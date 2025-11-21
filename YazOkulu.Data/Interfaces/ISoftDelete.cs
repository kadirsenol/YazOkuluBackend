using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOkulu.Data.Interfaces
{
    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
