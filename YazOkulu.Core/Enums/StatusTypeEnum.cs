using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOkulu.Core.Enums
{
    public enum StatusTypeEnum
    {
        PendingApproval = 18,   // Onay Bekliyor
        Approved = 19,           // Onaylandı
        Rejected = 20,           // Reddedildi
        QuotaFull = 22          //Kontenjan Dolu
    }
}
