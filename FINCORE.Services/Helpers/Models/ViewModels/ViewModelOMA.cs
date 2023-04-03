using FINCORE.Models.Models.Acquisition.OMA;
using Microsoft.AspNetCore.Http;

namespace FINCORE.Services.Helpers.Models.ViewModels
{
    public class ViewModelOMA
    {
        public OMAModel? OMAModel { get; set; }
        public string? SelectCompany { get; set; }
        public string? SelectPemohon { get; set; }
        public string? SelectSLIK { get; set; }
        public IFormFile? slikpemohon { get; set; }
        public IFormFile? slikpasangan { get; set; }
        public IFormFile? appifile { get; set; }
    }
}