using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class WellSq
    {
        [Key]
        public int f_id { get; set; }

        public string platformId { get; set; }

        public virtual DataSq data { get; set; }
    }
}
