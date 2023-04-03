using FINCORE.Models.Models.Dukcapil;
using FINCORE.Services.Helpers.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINCORE.Services.IServices.IThirdParty
{
    public interface IDukcapilService
    {
        Task<APIResponse> GetDataDukcapil(DukcapilModels dukcapilModels);
    }
}
