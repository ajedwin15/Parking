using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using src.Parking.Api.DB.Models.Enum;

namespace src.Parking.Api.DB.Models
{
    public class OfficialVehicle
    {
        [Key]       
        public string? PlateNumber { get; set; }
        public TypeVehicleEnum? VehicleType { get; set; }

        public ICollection<Instance>? Instances { get; set; }
    }
}


 