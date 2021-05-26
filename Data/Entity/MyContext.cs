using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data.Entity
{
    public class MyContext : DbContext
    {
        public virtual DbSet<DataSq> Data { get; set; }

        public virtual DbSet<WellSq> Well { get; set; }

        public virtual DbSet<PlatformwelldataSq> PlatformWellActual { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public void AddOrUpdate(PlatformwelldataSq platformdata)
        {
            var newData = platformdata.data;
            if (Data.Any((o => o.id == newData.id && o.uniqueName == newData.uniqueName)))
            {
                AddOrUpdate(platformdata.data);
                AddOrUpdate(platformdata.wellList);
            }
            else
            {
                PlatformWellActual.Add(platformdata);
                SaveChanges();
            }
        }

        public void AddOrUpdate(ICollection<WellSq> wells)
        {
            foreach(var well in wells)
            {
                AddOrUpdate(well);
            }
        }

        public void AddOrUpdate(WellSq well)
        {
            var newData = well.data;
            var existData = Data.Where(b => b.id == newData.id && b.uniqueName == newData.uniqueName).FirstOrDefault();

            if (existData == null)
            {
                Well.Add(well);
            }
            else
            {
                newData.f_id = existData.f_id;
                Entry(existData).CurrentValues.SetValues(newData);
            }
            SaveChanges();
        }

        public void AddOrUpdate(DataSq newData)
        {
            var existData = Data.Where(b => b.id == newData.id && b.uniqueName == newData.uniqueName).FirstOrDefault();

            if (existData == null)
            {
                Data.Add(newData);
            }
            else
            {
                newData.f_id = existData.f_id;
                Entry(existData).CurrentValues.SetValues(newData);
            }
            SaveChanges();
        }
    }
}
