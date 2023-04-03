using Microsoft.AspNetCore.Http;

namespace FINCORE.Models.Models.Acquisition.CM
{
    public class PhotoDetailResultModels
    {
        public string? credit_id { get; set; }
        public string? photo_type_id { get; set; }
        public IFormFile? fileUpload { get; set; }
        public string? photo_id { get; set; }
        public string? flagAction { get; set; }
    }
}