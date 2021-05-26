using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class PlatformwelldataSq
    {
        [Key]
        public int f_id { get; set; }

        public virtual DataSq data { get; set; }

        public virtual ICollection<WellSq> wellList { get; set; }
    }
}
