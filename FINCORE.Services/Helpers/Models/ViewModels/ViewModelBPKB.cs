using FINCORE.Models.Models.Acquisition.BPKB;

namespace FINCORE.Services.Helpers.Models.ViewModels
{
    public class ViewModelBPKB
    {
        public BPKBModels? BPKB { get; set; }
        public BPKBPinjamModels? BPKBPinjam { get; set; }
        public string? CustomerName { get; set; }
        public string? QQName { get; set; }
        public string? sBPKB { get; set; }
        public string? ChasisNo { get; set; }
        public string? MachineNo { get; set; }
        public string? DealerCode { get; set; }
        public string? ItemMerk { get; set; }
        public string? ItemTypeName { get; set; }
        public string? ItemColor { get; set; }
        public string? StatusNPP { get; set; }
        public string? StatusLunasNPP { get; set; }
        public string? STATUSBPKB { get; set; }
        public string? ItemYear { get; set; }
        public string? CompanyId { get; set; }
        public string? UserName { get; set; }
        public string? BranchName { get; set; }
        public string? BranchId { get; set; }
        public int? BiayaTitip { get; set; }
        public decimal? biayaaging { get; set; }
        public int jmlhariaging { get; set; }
    }
}