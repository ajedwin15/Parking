using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using src.Parking.Api.DB.Models;

namespace DB.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? VehicleId { get; set; }        
        public string? PlateNumber { get; set; }           
        public int TotalMinutes { get; set; }        
        public bool? IsPaid { get; set; }        
        public ICollection<Resident>? Resident { get; set; } 
        public ICollection<OfficialVehicle>? OfficialVehicle { get; set; }
    }
}
