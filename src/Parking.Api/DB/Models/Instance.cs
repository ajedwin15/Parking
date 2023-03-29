using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace src.Parking.Api.DB.Models
{
    public class Instance
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InstanceId { get; set; }
        public DateTime? Entrytime { get; set; }
        public DateTime? DepartureTime { get; set; }
    }
}