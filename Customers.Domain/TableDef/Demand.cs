using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Domain.TypeDef;

namespace Customers.Domain.TableDef
{
    public class Demand
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        public string Department { get; set; }

        public string Recept { get; set; }

        public DateTime ReceiveDate { get; set; }

        public string Supporter { get; set; }

        public int ExpectedSubscriber { get; set; }

        public double ExpectedProfit { get; set; }

        public DateTime ExpectedCompleteDate { get; set; }

        public DateTime ActualCompleteDate { get; set; }

        public string ProgressDescription { get; set; }

        public string Comments { get; set; }

        public int CityValue { get; set; }

        public CityDef City 
        {
            get { return (CityDef)CityValue; }
            set { CityValue = (int)value; }
        }

        public int AcceptPathValue { get; set; }

        public AcceptPathDef AcceptPath 
        {
            get { return (AcceptPathDef)AcceptPathValue; }
            set { AcceptPathValue = (int)value; } 
        }

        public int CustomerLevelValue { get; set; }

        public CustomerLevelDef CustomerLevel 
        {
            get { return (CustomerLevelDef)CustomerLevelValue; }
            set { CustomerLevelValue = (int)value; } 
        }

        public int DemandTypeValue { get; set; }

        public DemandTypeDef DemandType 
        {
            get { return (DemandTypeDef)DemandTypeValue; }
            set { DemandTypeValue = (int)value; } 
        }

        public int DemandSourceValue { get; set; }

        public DemandSourceDef DemandSource 
        {
            get { return (DemandSourceDef)DemandSourceValue; }
            set { DemandSourceValue = (int)value; } 
        }

        public int DemandLevelValue { get; set; }

        public DemandLevelDef DemandLevel 
        {
            get { return (DemandLevelDef)DemandLevelValue; }
            set { DemandLevelValue = (int)value; } 
        }

        public int MarketingThemeValue { get; set; }

        public MarketingThemeDef MarketingTheme 
        {
            get { return (MarketingThemeDef)MarketingThemeValue; }
            set { MarketingThemeValue = (int)value; } 
        }

        public int SatisfactoryValue { get; set; }

        public SatisfactoryDef Satisfactory 
        {
            get { return (SatisfactoryDef)SatisfactoryValue; }
            set { SatisfactoryValue = (int)value; } 
        }

        public int ProjectStateValue { get; set; }

        public ProjectStateDef ProjectState 
        {
            get { return (ProjectStateDef)ProjectStateValue; }
            set { ProjectStateValue = (int)value; } 
        }

        public TimeSpan Delay
        {
            get
            { return ((ProjectState == ProjectStateDef.Complete) ? ActualCompleteDate : DateTime.Now) - ReceiveDate; }
        }

        public Demand()
        {
            ExpectedSubscriber = 0;
            ExpectedProfit = 0;
            AcceptPath = AcceptPathDef.Direct;
            CustomerLevel = CustomerLevelDef.Four;
            DemandLevel = DemandLevelDef.C;
            DemandSource = DemandSourceDef.Branch;
            DemandType = DemandTypeDef.Intra;
            MarketingTheme = MarketingThemeDef.Market;
            Satisfactory = SatisfactoryDef.Unknown;
            ProjectState = ProjectStateDef.InProgress;
        }

        public static Demand Parse(DemandView demandView)
        {
            return new Demand()
            {
                AcceptPath = demandView.AcceptPath.GetAcceptPathIndex(),
                ActualCompleteDate = demandView.ActualCompleteDate<DateTime.Today.AddDays(-3) ?
                    DateTime.Today.AddDays(-3) : demandView.ActualCompleteDate,
                City = demandView.City.GetCityIndex(),
                Comments = demandView.Comments,
                CustomerLevel = int.Parse(demandView.CustomerLevel).GetCustomerLevelIndex(),
                DemandLevel = demandView.DemandLevel.GetDemandLevelIndex(),
                DemandSource = demandView.DemandSource.GetDemandSourceIndex(),
                DemandType = demandView.DemandType.GetDemandTypeIndex(),
                Department = demandView.Department,
                ExpectedCompleteDate = demandView.ExpectedCompleteDate,
                ExpectedProfit = demandView.ExpectedProfit,
                ExpectedSubscriber = demandView.ExpectedSubscriber,
                Id = demandView.Id,
                MarketingTheme = demandView.MarketingTheme.GetMarketingThemeIndex(),
                ProgressDescription = demandView.ProgressDescription,
                ProjectName = demandView.ProjectName,
                ProjectState = demandView.ProjectState.GetProjectStateIndex(),
                ReceiveDate = demandView.ReceiveDate,
                Recept = demandView.Recept,
                Satisfactory = demandView.Satisfactory.GetSatisfactoryIndex(),
                Supporter = demandView.Supporter
            };
        }
    }
}
