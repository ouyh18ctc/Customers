using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Customers.Domain.TypeDef;
using Customers.Domain.TableDef;
using Customers.WebUI.Models;
using Customers.WebUI.Properties;
using Customers.Domain.ViewHelper;
using Customers.Domain.Security;
using LinqToCsv.Context;
using LinqToCsv.Description;

namespace Customers.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IDemandRepository repository;
        private IUserInformationRepository userRepository;
        private IProgressRepository progressRepository;

        public int PageSize { get; set; }

        public HomeController(IDemandRepository repository, IUserInformationRepository userRepository,
            IProgressRepository progressRepository)
        {
            this.repository = repository;
            this.userRepository = userRepository;
            this.progressRepository = progressRepository;
            PageSize = Settings.Default.PageSize;
            ListProviders.SetListProvider(() => new DemandListProvider());
        }

        private string currentUser = "";
        public string CurrentUser
        {
            get
            {
                return (User != null && User.Identity != null) ? User.Identity.Name : currentUser;
            }
            set//Only for tests.
            {
                if (User == null || User.Identity == null) { currentUser = value; }
            }
        }

        public ViewResult Index(string city, int page = 1)
        {
            IEnumerable<string> cityList = userRepository.GetPermissionCities(CurrentUser) ?? new List<string>();
            List<Demand> demands = repository.Demands.ToList();
            demands = demands.Where(x => cityList.FirstOrDefault(c => x.City.GetCityName() == c) != null).ToList();
            IEnumerable<DemandView> demandViews
                = (demands.Count() == 0) ? new List<DemandView>()
                : demands.Select(x => DemandView.Parse(x)).ToList();
            DemandListViewModel viewModel = new DemandListViewModel
            {
                Demands = demandViews
                    .Where(p => city == null || p.City == city)
                    .OrderBy(p => p.Id)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize).ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = city == null ?
                        demandViews.Count() :
                        demandViews.Where(e => e.City == city).Count()
                },
                CurrentCity = city
            };
            foreach (DemandView demand in viewModel.Demands)
            {
                demand.AllowFinish = userRepository.UserCanEditCity(CurrentUser, demand.City);
            }

            ViewBag.Title = "客户需求列表";
            return View(viewModel);
        }

        public ViewResult Query(string city)
        {
            ListProviders.SetListProvider(() => new DemandListProvider());
            QueryDemandViewModel viewModel = new QueryDemandViewModel
            {
                CurrentCities = string.IsNullOrEmpty(city) ? new List<string>() : new List<string> { city },
                BeginDate = DateTime.Now.AddDays(-180),
                EndDate = DateTime.Now
            };
            ViewBag.Title = "客户需求查询";
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Query(QueryDemandViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                List<Demand> demands = repository.Demands.ToList();
                List<DemandView> demandViews
                    = demands.Select(x => DemandView.Parse(x))
                    .Where(p => 
                        (viewModel.CurrentCities == null ||
                        viewModel.CurrentCities.FirstOrDefault(x => x == p.City) != null)
                        && p.ReceiveDate >= viewModel.BeginDate && p.ReceiveDate <= viewModel.EndDate
                        && (p.Satisfactory == viewModel.Satisfactory || viewModel.Satisfactory == null)).ToList();
                ViewBag.Title = "需求列表查询结果与导出";
                foreach (DemandView demand in demandViews)
                {
                    demand.AllowFinish = userRepository.UserCanEditCity(CurrentUser, demand.City);
                }
                TempData["Demands"] = demandViews;
                return View("QueryResult", demandViews); 
            }
            else
            {
                TempData["warning"] = "输入有误。请重新输入";
                return View(); 
            }
        }

        [Authorize]
        public FileStreamResult Export(string filePath, string fileName)
        {
            string absoluFilePath
                = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads/", filePath);
            CsvFileDescription fileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true,
                EnforceCsvColumnAttribute = true,
                TextEncoding = Encoding.GetEncoding("GB2312")
            };
            CsvContext.Write<DemandView>(TempData["Demands"] as IEnumerable<DemandView>, absoluFilePath, fileDescription);
            return File(new FileStream(absoluFilePath, FileMode.Open), 
                "application/octet-stream", Server.UrlEncode(fileName));
        }

        [Authorize]
        public ActionResult Create(string city = null)
        {
            ViewBag.Title = "新建需求";
            List<Demand> demands = repository.Demands.ToList();
            Demand demand = demands.FirstOrDefault(x => x.City.GetCityName() == city);
            if (!userRepository.SetPermissionCities(CurrentUser))
            {
                TempData["error"] = "当前用户没有对任何城市记录操作的权限！请联系管理员放开权限。";
                return this.Redirect("/");
            }
            return View("Edit", new DemandView()
            {
                City = demand == null ? "深圳" : city,
                ReceiveDate = DateTime.Now,
                ExpectedCompleteDate = DateTime.Now.AddDays(100),
                Recept=DateTime.Now.ToString("yyyyMMddHHmm")
            });
        }

        [Authorize(Users="admin")]
        public ViewResult Edit(int demandId)
        {
            Demand demand = repository.Demands.FirstOrDefault(x => x.Id == demandId);
            ViewBag.Title = (demand == null) ? "新建需求" : "编辑需求：" + demand.ProjectName;
            return View(DemandView.Parse(demand ?? new Demand()));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(DemandView demand)
        {
            if (ModelState.IsValid)
            {
                int id = repository.SaveDemand(demand);
                int weekNum
                    = (int)Math.Ceiling((DateTime.Now - new DateTime(DateTime.Now.Year, 1, 1)).Days / (float)7);
                progressRepository.SaveProgress(id, weekNum, demand.ProgressDescription);
                TempData["success"] = string.Format("{0}需求已成功保存！", demand.ProjectName);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["warning"] = "部分字段不合要求，请重新填写！";
                return View(demand);
            }
        }

        [Authorize(Users = "admin")]
        public ActionResult Remove(int demandId, string remove = null)
        {
            Demand demand = repository.Demands.FirstOrDefault(x => x.Id == demandId);
            if (demand == null)
            {
                TempData["error"] = "请求的需求不存在！";
                return this.Redirect("/");
            }

            if (String.Equals(remove, "yes", StringComparison.OrdinalIgnoreCase))
            {
                repository.DeleteDemand(demandId);
                TempData["info"] = demand.ProjectName + "已经从数据库中删除！";
                demand = null;
            }
            else if (String.Equals(remove, "no", StringComparison.OrdinalIgnoreCase))
            {
                TempData["info"] = demand.ProjectName + "已经放弃删除。";
            }
            else
            {
                TempData["warning"] = "您是否确认删除需求：" + demand.ProjectName + "？一旦删除将无法恢复！";
            }
            ViewBag.Title = "删除需求";

            return View(demand);
        }

        [Authorize]
        public ActionResult Finish(int demandId)
        {
            Demand demand = repository.Demands.FirstOrDefault(x => x.Id == demandId);
            if (demand == null)
            {
                TempData["error"] = "请求的需求不存在！";
                return this.Redirect("/");
            }
            return View(FinishDemandView.Parse(demand));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Finish(FinishDemandView viewModel)
        {
            if (repository.FinishDemand(viewModel))
            {
                TempData["success"] = viewModel.ProjectName + "结单成功！";
            }
            else
            {
                TempData["error"] = viewModel.ProjectName + "结单失败！";
            }
            return this.Redirect("/");
        }
    }
}
