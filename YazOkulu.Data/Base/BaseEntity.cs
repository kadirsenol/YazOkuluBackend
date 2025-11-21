using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Interfaces;

namespace YazOkulu.Data.Base
{
    public abstract class BaseEntity : ISoftDelete
    {
        public int? CreateUserID { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public int? ModifyUserID { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? DeleteUserID { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
        public Guid Guid { get; set; } = Guid.NewGuid();
    }
}
