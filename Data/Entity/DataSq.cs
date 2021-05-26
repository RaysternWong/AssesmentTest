using System.ComponentModel.DataAnnotations;

namespace Data.Entity
{
    public class DataSq : Data
    {
        [Key]
        public int f_id { get; set; }

        public bool IsSame(DataSq dataSq)
        {
            if( this.id == dataSq.id 
                && this.uniqueName == dataSq.uniqueName  
                && this.latitude == dataSq.latitude 
                && this.updatedAt == dataSq.updatedAt
                && this.createdAt == dataSq.createdAt
                && this.longitude == dataSq.longitude)
            {
                return true;
            }
            return false;
        }
    }
}
