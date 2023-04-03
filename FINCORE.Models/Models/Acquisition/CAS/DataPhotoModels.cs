namespace FINCORE.Models.Models.Acquisition.CAS
{
    public class DataPhotoModels
    {
        public int id { get; set; }
        public string credit_id { get; set; }
        public string filename { get; set; }
        public string filePath { get; set; }
        public string photo_type_id { get; set; }
        public string photo_id { get; set; }
        public string photo_desc { get; set; }
        public int? is_new_zoom { get; set; }
    }
}