using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using YazOkulu.Data.Base;

namespace YazOkulu.Data.Models
{
    public class Application : BaseEntity
    {
        public int ApplicationID { get; set; }
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public int StatusID { get; set; }

        [ForeignKey(nameof(UserID))]
        public virtual User User { get; set; }
        [ForeignKey(nameof(CourseID))]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(StatusID))]
        public virtual Parameter StatusType { get; set; }
    }
}
