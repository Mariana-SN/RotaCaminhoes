using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TruckRouteAPI.Models
{
    [Table(name: "Route")]
    public class Route
    {
        [Key]

        public int Id { get; set; }
        public string RouteName { get; set; }
        public string Origin { get; set; }
        public string Destiny { get; set; }
        public int Distance { get; set; }
    }
}
