using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINCORE.Models.Models.Payment.CashierSessionVerify
{
    public class RequestParamReceipt
    {
        public int PageIndex { get; set; }
        public int MaxPage { get; set; }
        public string? SearchTerm { get; set; } = "";
        public string? BranchId { get; set; }
        public string? SessionId { get; set; }
        public string? UserId { get; set; }
    }
}
