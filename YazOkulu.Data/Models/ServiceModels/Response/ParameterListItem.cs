using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOkulu.Data.Models.ServiceModels.Response
{
    public class ParameterListItem
    {
        public int ParameterID { get; set; }
        public int? ParentParameterID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
