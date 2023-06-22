using DellortoApi.Modals;
using DellortoApi.Services.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DellortoApi.Controllers
{
    internal class AllPdfReportsLogic : IAllPdfReportsLogic
    {
        public AllPdfReportsLogic(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public Task<JsonResult> GetGRNGoodBadReport(List<GrnGoodBadReportModel> model)
        {
            throw new System.NotImplementedException();
        }
    }
}