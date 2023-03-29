using DB.Models;
using src.Parking.Api.DB.Models;
using src.Parking.Api.DB.Models.Enum;
using src.Parking.Api.Parking.Api.Core;
using src.Parking.Api.Parking.Api.Core.Dtos;

namespace src.Parking.Api.Parking.Api.Repository.Interface
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> PostVehicle(CheckIn vehicle);
        Task<OfficialVehicle?> GetOfficialVehicle(string PlateNumber);
        Task<OfficialVehicle> RegisterVehicleOfficial(RegisterTypeVehicle registerTypeVehicle);
        Task<Resident> RegisterVehicleResident(RegisterTypeVehicle registerTypeVehicle);
        Task<Vehicle> PatchVehicle(CheckOut vehicle);


    }
}