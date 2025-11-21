using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Models.ServiceModels.Base;

namespace YazOkulu.Data.Models.ServiceModels.Request
{
    public class ParameterSearch : SearchRequest
    {
        public int? ParentParameterID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
