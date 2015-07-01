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
    public class FinishDemandView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string City { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "请输入市场支撑放号用户数")]
        [Display(Name = "市场支撑放号用户数")]
        [Column(Name = "市场支撑放号用户数")]
        public int ExpectedSubscriber { get; set; }

        [Required(ErrorMessage = "请输入市场支撑产出价值")]
        [Display(Name = "市场支撑产出价值")]
        [Column(Name = "市场支撑产出价值")]
        public double ExpectedProfit { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ProjectState { get; set; }

        [DropdownList("Satisfactory")]
        [DisplayName("满意度")]
        [Column(Name = "满意度")]
        public string Satisfactory { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "项目进展")]
        [Column(Name = "项目进展")]
        public string ProgressDescription { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "备注")]
        [Column(Name = "备注")]
        public string Comments { get; set; }

        public static FinishDemandView Parse(Demand demand)
        {
            return new FinishDemandView()
            {
                Id = demand.Id,
                City = demand.City.GetCityName(),
                ProjectName = demand.ProjectName,
                ExpectedSubscriber = demand.ExpectedSubscriber,
                ExpectedProfit = demand.ExpectedProfit,
                ProjectState = ProjectStateDef.Complete.GetProjectStateDescription(),
                Satisfactory = demand.Satisfactory.GetSatisfactoryDescription(),
                ProgressDescription = demand.ProgressDescription,
                Comments = demand.Comments
            };
        }

        public void FinishDemand(Demand demand)
        {
            demand.ExpectedSubscriber = ExpectedSubscriber;
            demand.ExpectedProfit = ExpectedProfit;
            demand.ActualCompleteDate = DateTime.Now;
            demand.ProjectState = ProjectState.GetProjectStateIndex();
            demand.Satisfactory = Satisfactory.GetSatisfactoryIndex();
            demand.ProgressDescription = ProgressDescription;
            demand.Comments = Comments;
        }
    }
}
