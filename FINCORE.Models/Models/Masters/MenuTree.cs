namespace FINCORE.Models.Models.Masters
{
    public class MenuTree
    {
        public int module_id { get; set; }
        public string module_name { get; set; }
        public int parent_module_id { get; set; }
        public string menu_title { get; set; }
        public string menu_action { get; set; }
        public string menu_controller { get; set; }
        public int module_sort { get; set; }
        public int menu_sort { get; set; }
    }

    public class moduleMenu
    {
        public int modul_id { get; set; }
        public string modul_name { get; set; }
        public List<child> childs { get; set; }
    }

    public class child
    {
        public string menu_title { get; set; }
        public string menu_action { get; set; }
        public string menu_controller { get; set; }
    }
}