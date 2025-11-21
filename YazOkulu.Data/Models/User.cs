using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Base;

namespace YazOkulu.Data.Models
{
    public class User : BaseEntity
    {
        public int UserID { get; set; }
        public string Gsm { get; set; } = string.Empty;
        [Description("Admin ve Öğrenci Kullanıcı Adı")]
        public string? UserName { get; set; } = string.Empty;
        public int? FacultyTypeID { get; set; }
        public int? DepartmentTypeID { get; set; }
        [Description("Admin Şifre")]
        public string? Password { get; set; }
        public int RoleTypeID { get; set; }
    }
}
