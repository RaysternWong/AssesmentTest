using Data.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Data
{
    public class PlatformWellActual : Data
    {
        [JsonProperty("well")]
        public virtual List<Well> wells { get; set; }

        public PlatformwelldataSq ToPlatformwelldataSq()
        {
            List<WellSq> wellSqList = new List<WellSq>();

            foreach (var well in wells)
            {
                var wellSq = new WellSq { 
                    data = well.ToDataSq(),
                    platformId = well.platformId 
                };

                wellSqList.Add(wellSq) ;
            }

            return new PlatformwelldataSq
            {
                data = this.ToDataSq(),
                wellList = wellSqList             
            };
        }
    }

    public class Well : Data
    {
        [JsonProperty("platformId")]
        public string platformId { get; set; }
    }

    public class Data
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("uniqueName")]
        public string uniqueName { get; set; }

        [JsonProperty("latitude")]
        public string latitude { get; set; }

        [JsonProperty("longitude")]
        public string longitude { get; set; }

        [JsonProperty("createdAt")]
        public DateTime createdAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime updatedAt { get; set; }

        public DataSq ToDataSq()
        {
            return new DataSq
            {
                id = this.id,
                uniqueName = this.uniqueName,
                createdAt = this.createdAt,
                updatedAt = this.updatedAt,
                longitude = this.longitude,
                latitude = this.latitude
            };
        }
    } 
}

