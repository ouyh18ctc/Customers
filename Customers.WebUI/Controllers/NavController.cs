using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Customers.Domain.ViewHelper;
using Customers.Domain.TypeDef;
using Customers.Domain.TableDef;
using Customers.Domain.Excel;
using Customers.WebUI.Models;
using Customers.Domain.Security;
using LinqToCsv.Context;
using LinqToCsv.Description;
using System.Text;

namespace Customers.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IDemandRepository repository;
        private IUserInformationRepository userRepository;
        private IProgressRepository progressRepository;

        public NavController(IDemandRepository repo, IUserInformationRepository userRepository,
            IProgressRepository progressRepository)
        {
            repository = repo;
            this.userRepository = userRepository;
            ListProviders.SetListProvider(() => new TimeSelectorProvider());
            this.progressRepository = progressRepository;
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

        public PartialViewResult Menu(string city = null)
        {
            ViewBag.SelectedCity = city;

            List<Demand> demands = repository.Demands.ToList();
            IEnumerable<string> cities = demands.Select(
                x => x.City.GetCityName()).Distinct().OrderBy(x => x.GetCityIndex());
            IEnumerable<string> cityList = userRepository.GetPermissionCities(CurrentUser) ?? new List<string>();

            return PartialView(cities.Intersect(cityList));
        }

        [Authorize]
        public ActionResult Import()
        {
            IEnumerable<string> cityList = userRepository.GetPermissionCities(CurrentUser);
            if (cityList.Count() > 0)
            {
                ViewBag.Title = "导入Excel表";
                string tips = "当前用户可以导入的城市包括：";
                foreach (string city in cityList)
                { tips += " " + city; }
                TempData["info"] = tips;
                return View(new ExcelImportViewModel(/*progressRepository*/));
            }
            TempData["error"] = "当前用户没有对任何城市记录操作的权限！请联系管理员放开权限。";
            return this.Redirect("/");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Import(ExcelImportViewModel viewModel)
        {
            string fileName = Path.GetFileName(Request.Files["fileUpload"].FileName);
            if (string.IsNullOrEmpty(fileName) ||
                (Path.GetExtension(fileName).ToLower() != ".xls"
                && Path.GetExtension(fileName).ToLower() != ".xlsx"))
            {
                TempData["error"] = "未选择文件或选择的不是Excel文件，请重新选择！";
                return Import();
            }
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads/", fileName);
            Request.Files["fileUpload"].SaveAs(path);
            ViewBag.Title = "导入Excel表：" + fileName;
            ExcelImporter importer = new ExcelImporter(path, new string[1] { "市级客服支撑项目计划" });
            
            DataTable dataTable = importer["市级客服支撑项目计划"];
            if (dataTable == null)
            {
                TempData["error"] 
                    = "选择的文件格式不正确（如不正确的sheet名称，不是【市级客服支撑项目计划】），请重新选择！";
                return Import();
            }
            viewModel.DemandRepository = repository;
            viewModel.CityPermissions = userRepository.GetPermissionCities(CurrentUser);
            int importCount = viewModel.Import(dataTable);
            if (importCount > 0)
            {
                TempData["success"] = "导入数据成功！共导入：" + importCount + "条记录。";
            }
            else
            {
                TempData["warning"] = "由于权限等原因，没有导入任何记录！";
            }
            return View(viewModel);
        }

        public ActionResult StatByLevel()
        {
            ViewBag.Title = "需求等级统计";
            return View(new StatByLevelViewModel()
            {
                BeginTime = DateTime.Now.AddDays(-100),
                EndTime = DateTime.Now,
                TimeSpan = "自由选择"
            });
        }

        [HttpPost]
        public ActionResult StatByLevel(StatByLevelViewModel viewModel)
        {
            ViewBag.Title = "需求等级统计";
            IEnumerable<ByLevelDemandStat> results 
                = viewModel.FilterByTimeSpan(repository.Demands.ToList()).GetDemandStatByLevel();
            TempData["StatByLevel"] = results;
            return View(new StatByLevelViewModel()
            {
                BeginTime = viewModel.BeginTime,
                EndTime = viewModel.EndTime,
                TimeSpan = viewModel.TimeSpan,
                Results = results
            });
        }

        public ActionResult ExportStatByLevel(string fileName)
        {
            IEnumerable<ByLevelDemandStat> results = TempData["StatByLevel"] as IEnumerable<ByLevelDemandStat>;
            if (results == null) { return Redirect("StatByLevel"); }
            string absoluFilePath
                = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads/", fileName);
            CsvFileDescription fileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true,
                EnforceCsvColumnAttribute = true,
                TextEncoding = Encoding.GetEncoding("GB2312")
            };
            CsvContext.Write<ByLevelDemandStat>(results, absoluFilePath, fileDescription);
            return File(new FileStream(absoluFilePath, FileMode.Open),
                "application/octet-stream", Server.UrlEncode(fileName));
        }

        public ActionResult StatByDelay()
        {
            ViewBag.Title = "需求历时统计";
            return View(new StatByDelayViewModel()
            {
                BeginTime = DateTime.Now.AddDays(-100),
                EndTime = DateTime.Now,
                TimeSpan = "自由选择"
            });
        }

        [HttpPost]
        public ActionResult StatByDelay(StatByDelayViewModel viewModel)
        {
            ViewBag.Title = "需求历时统计";
            IEnumerable<ByDelayDemandStat> results = viewModel.FilterByTimeSpan(repository.Demands.ToList()).GetDemandStatByDelay();
            TempData["StatByDelay"] = results;
            return View(new StatByDelayViewModel()
            {
                BeginTime = viewModel.BeginTime,
                EndTime = viewModel.EndTime,
                TimeSpan = viewModel.TimeSpan,
                Results = results
            });
        }

        public ActionResult ExportStatByDelay(string fileName)
        {
            IEnumerable<ByDelayDemandStat> results = TempData["StatByDelay"] as IEnumerable<ByDelayDemandStat>;
            if (results == null) { return Redirect("StatByDelay"); }
            string absoluFilePath
                = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads/", fileName);
            CsvFileDescription fileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true,
                EnforceCsvColumnAttribute = true,
                TextEncoding = Encoding.GetEncoding("GB2312")
            };
            CsvContext.Write<ByDelayDemandStat>(results, absoluFilePath, fileDescription);
            return File(new FileStream(absoluFilePath, FileMode.Open),
                "application/octet-stream", Server.UrlEncode(fileName));
        }

        public ActionResult StatByType()
        {
            ViewBag.Title = "需求类型统计";
            return View(new StatByTypeViewModel()
            {
                BeginTime = DateTime.Now.AddDays(-100),
                EndTime = DateTime.Now,
                TimeSpan = "自由选择"
            });
        }

        [HttpPost]
        public ActionResult StatByType(StatByTypeViewModel viewModel)
        {
            ViewBag.Title = "需求类型统计";
            IEnumerable<ByTypeDemandStat> results = viewModel.FilterByTimeSpan(repository.Demands.ToList()).GetDemandStatByType();
            TempData["StatByType"] = results;
            return View(new StatByTypeViewModel()
            {
                BeginTime = viewModel.BeginTime,
                EndTime = viewModel.EndTime,
                TimeSpan = viewModel.TimeSpan,
                Results = results
            });
        }

        public ActionResult ExportStatByType(string fileName)
        {
            IEnumerable<ByTypeDemandStat> results = TempData["StatByType"] as IEnumerable<ByTypeDemandStat>;
            if (results == null) { return Redirect("StatByType"); }
            string absoluFilePath
                = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "uploads/", fileName);
            CsvFileDescription fileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true,
                EnforceCsvColumnAttribute = true,
                TextEncoding = Encoding.GetEncoding("GB2312")
            };
            CsvContext.Write<ByTypeDemandStat>(results, absoluFilePath, fileDescription);
            return File(new FileStream(absoluFilePath, FileMode.Open),
                "application/octet-stream", Server.UrlEncode(fileName));
        }
    }
}
