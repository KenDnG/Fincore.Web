using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINCORE.Models.Models.Acquisition.NPP.Pagination
{
    public class PaginationLookupNppProcessCredit
    {
        public long? RowNumber { get; set; }
        public string CreditId { get; set; }
        public string CreditDate { get; set; }
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string PONumber { get; set; }
        public string CustomerName { get; set; }
        public string InstallmentDate { get; set; }
        public string DealerCode { get; set; }
        public string DealerName { get; set; }
        public string DealerAddress { get; set; }
        public string StatusApproval { get; set; }
    }
}
