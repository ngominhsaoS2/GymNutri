using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GymNutri.Business.Dapper.ViewModels;

namespace GymNutri.Business.Dapper.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<RevenueReportViewModel>> GetReportAsync(string fromDate, string toDate);
    }
}