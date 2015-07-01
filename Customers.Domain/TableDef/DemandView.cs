using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.TypeDef;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Customers.Domain.ViewHelper;
using Customers.Domain.Excel;

namespace Customers.Domain.TableDef
{
    public class DemandView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool AllowFinish { get; set; }

        [DropdownList("City")]
        [DisplayName("地市")]
        [CsvColumn(Name = "地市")]
        public string City { get; set; }

        [Required(ErrorMessage = "请输入需求提出人")]
        [Display(Name = "需求提出人")]
        [CsvColumn(Name = "需求提出人")]
        public string Recept { get; set; }

        [Required(ErrorMessage = "请输入项目名称")]
        [Display(Name = "项目名称")]
        [CsvColumn(Name = "项目名称")]
        public string ProjectName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        [CsvColumn(Name = "备注")]
        public string Comments { get; set; }

        [Required(ErrorMessage = "请输入服务部门")]
        [Display(Name = "服务部门")]
        [CsvColumn(Name = "服务部门")]
        public string Department { get; set; }

        [Required(ErrorMessage = "请输入受理时间")]
        [Display(Name = "受理时间")]
        [CsvColumn(Name = "受理时间")]
        public DateTime ReceiveDate { get; set; }

        [Required(ErrorMessage = "请输入责任人")]
        [Display(Name = "责任人")]
        [CsvColumn(Name = "责任人")]
        public string Supporter { get; set; }

        [Required(ErrorMessage = "请输入市场支撑放号用户数")]
        [Display(Name = "市场支撑放号用户数")]
        [CsvColumn(Name = "市场支撑放号用户数")]
        public int ExpectedSubscriber { get; set; }

        [Required(ErrorMessage = "请输入市场支撑产出价值")]
        [Display(Name = "市场支撑产出价值")]
        [CsvColumn(Name = "市场支撑产出价值")]
        public double ExpectedProfit { get; set; }

        [Required(ErrorMessage = "请输入预计完成/完成时间")]
        [Display(Name = "预计完成时间")]
        [CsvColumn(Name = "预计完成/完成时间")]
        public DateTime ExpectedCompleteDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "实际完成时间")]
        public DateTime ActualCompleteDate { get; set; }

        [DropdownList("AcceptPath")]
        [DisplayName("受理途径")]
        [CsvColumn(Name = "受理途径")]
        public string AcceptPath { get; set; }

        [DropdownList("CustomerLevel")]
        [DisplayName("客户等级")]
        [CsvColumn(Name = "客户等级")]
        public string CustomerLevel { get; set; }

        [DropdownList("DemandType")]
        [DisplayName("需求类型")]
        [CsvColumn(Name = "需求类型")]
        public string DemandType { get; set; }

        [DropdownList("DemandSource")]
        [DisplayName("需求归属")]
        [CsvColumn(Name = "需求归属")]
        public string DemandSource { get; set; }

        [DropdownList("DemandLevel")]
        [DisplayName("需求等级")]
        [CsvColumn(Name = "需求等级")]
        public string DemandLevel { get; set; }

        [DropdownList("MarketingTheme")]
        [DisplayName("营销主题")]
        [CsvColumn(Name = "营销主题")]
        public string MarketingTheme { get; set; }

        [RadioButtonList("ProjectState")]
        [DisplayName("项目状态")]
        [CsvColumn(Name = "项目状态")]
        public string ProjectState { get; set; }

        [DropdownList("Satisfactory")]
        [DisplayName("满意度")]
        [CsvColumn(Name = "满意度")]
        public string Satisfactory { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "项目进展")]
        [CsvColumn(Name = "项目进展")]
        public string ProgressDescription { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool IsFinished
        {
            get
            {
                return ProjectState == ProjectStateDef.Complete.GetProjectStateDescription()
                    && ActualCompleteDate > new DateTime(2000, 1, 1);
            }
        }

        public DemandView()
        {
            ExpectedSubscriber = 0;
            ExpectedProfit = 0;
            AcceptPath = AcceptPathDef.Direct.GetAcceptPathDescription();
            CustomerLevel = CustomerLevelDef.Four.GetCustomerLevelValue().ToString();
            DemandLevel = DemandLevelDef.C.GetDemandLevelDescription();
            DemandSource = DemandSourceDef.Branch.GetDemandSourceDescription();
            DemandType = DemandTypeDef.Intra.GetDemandTypeDescription();
            MarketingTheme = MarketingThemeDef.Market.GetMarketingThemeDescription();
            Satisfactory = SatisfactoryDef.Unknown.GetSatisfactoryDescription();
            ProjectState = ProjectStateDef.InProgress.GetProjectStateDescription();
            AllowFinish = false;
        }

        public static DemandView Parse(Demand demand)
        {
            return new DemandView()
            {
                AcceptPath = demand.AcceptPath.GetAcceptPathDescription(),
                ActualCompleteDate = demand.ActualCompleteDate,
                City = demand.City.GetCityName(),
                Comments = demand.Comments,
                CustomerLevel = demand.CustomerLevel.GetCustomerLevelValue().ToString(),
                DemandLevel = demand.DemandLevel.GetDemandLevelDescription(),
                DemandSource = demand.DemandSource.GetDemandSourceDescription(),
                DemandType = demand.DemandType.GetDemandTypeDescription(),
                Department = demand.Department,
                ExpectedCompleteDate = demand.ExpectedCompleteDate,
                ExpectedProfit = demand.ExpectedProfit,
                ExpectedSubscriber = demand.ExpectedSubscriber,
                Id = demand.Id,
                MarketingTheme = demand.MarketingTheme.GetMarketingThemeDescription(),
                ProgressDescription = demand.ProgressDescription,
                ProjectName = demand.ProjectName,
                ProjectState = demand.ProjectState.GetProjectStateDescription(),
                ReceiveDate = demand.ReceiveDate,
                Recept = demand.Recept,
                Satisfactory = demand.Satisfactory.GetSatisfactoryDescription(),
                Supporter = demand.Supporter,
                AllowFinish = false
            };
        }
    }
}
