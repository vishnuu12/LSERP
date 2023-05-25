using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JqDatatablesWebForm.Models
{
    public class DataTables
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<Dictionary<string, object>> data { get; set; }

        public List<Dictionary<string, object>> Ldata { get; set; }

        public List<DocumentType> ddocum { get; set; }
        public string TotalInwardCost { get; set; }

        public string ItemName { get; set; }
    }
}