using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GeoCoder.Models
{
    [PrimaryKey("Latitude", "Longitude")]
    public class GeoLocation
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }

    }
}
