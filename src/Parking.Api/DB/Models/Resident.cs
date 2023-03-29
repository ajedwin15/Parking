using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DB.Models;
using src.Parking.Api.DB.Models.Enum;

namespace src.Parking.Api.DB.Models
{

    [ComplexType]
    public class Resident
    {
        [Key]      
        public string? PlateNumber { get; set; }
        public int? TotalMinutes { get; set; }        
        public TypeVehicleEnum? VehicleType { get; set; } 
        public ICollection<Instance>? Instances { get; set; }
    }
}