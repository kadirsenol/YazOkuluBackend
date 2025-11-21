using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Models.ServiceModels.Base;

namespace YazOkulu.Data.Models.ServiceModels.Request
{
    public class ApplicationSearch : SearchRequest
    {
        public int? UserID { get; set; }
        public int? CourseID { get; set; }
    }
}
