using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customers.Domain.Excel;
using Customers.Domain.TableDef;

namespace Customers.WebUI.Models
{
    public class ExcelImportViewModel
    {
        public DataReadingRepository<DemandView> ReadingRepository { get; private set; }

        public IDemandRepository DemandRepository { get; set; }

        public IEnumerable<string> CityPermissions { get; set; }

        //private IProgressRepository progressRepository;

        public ExcelImportViewModel(/*IProgressRepository progressRepository*/)
        {
            ReadingRepository = new DataReadingRepository<DemandView>();
            //this.progressRepository = progressRepository;
        }

        public int Import(DataTable table)
        {
            IDataReader dataReader = table.CreateDataReader();
            ReadingRepository.Reading(dataReader);
            IEnumerable<DemandView> demandList =
                ReadingRepository.DataList.Where(x =>
                    CityPermissions.FirstOrDefault(y => y == x.City) != null);
            if (demandList.Count() > 0)
            {
                foreach (DemandView demand in demandList)
                {
                    int id = DemandRepository.SaveDemand(demand);
                    int weekNum
                        = (int)Math.Ceiling((DateTime.Now - new DateTime(DateTime.Now.Year, 1, 1)).Days / (float)7);
                    //progressRepository.SaveProgress(id, weekNum, demand.ProgressDescription);
                }
            }
            return demandList.Count();
        }
    }
}