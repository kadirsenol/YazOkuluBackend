using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Base;

namespace YazOkulu.Data.Models
{
    public class Parameter : BaseEntity
    {
        public int ParameterID { get; set; }
        public int? ParentParameterID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
