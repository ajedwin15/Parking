using DB.Models;
using Microsoft.AspNetCore.Mvc;
using src.Parking.Api.DB.Models;
using src.Parking.Api.Parking.Api.Core;
using src.Parking.Api.Parking.Api.Core.Dtos;
using src.Parking.Api.Parking.Api.Repository.Interface;

namespace src.Parking.Api.Parking.Api.Controllers
{
    [Route("api/v1/[Controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehiclesController(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var result = await _vehicleRepository.GetAll();
            return Ok(result);
        }

        // GET: api/Vehiculos/
        [HttpGet("{PlateNumber}")]
        public async Task<OfficialVehicle?> GetOfficialVehicle(string PlateNumber)
        {
            var vehiculo = await _vehicleRepository.GetOfficialVehicle(PlateNumber);
            return vehiculo;
        }

        [HttpPost("CheckIn")]
        public async Task<ActionResult<Vehicle>> PostVehicle([FromBody] CheckIn vehicle)
        {
            var result = await _vehicleRepository.PostVehicle(vehicle);
            return Ok(result);

        }

        [HttpPatch("checkOut")]
        public async Task<ActionResult<Vehicle>> PatchVehicle([FromBody] CheckOut vehicle)
        {
            var result = await _vehicleRepository.PatchVehicle(vehicle);
            return Ok(result);

        }

        [HttpPost("RegisterVehicleOfficial")]
        public async Task<ActionResult<OfficialVehicle>> PostRegisterVehicleOfficial([FromBody] RegisterTypeVehicle registerTypeVehicle)
        {
            var result = await _vehicleRepository.RegisterVehicleOfficial(registerTypeVehicle);
            return Ok(result);

        }

        [HttpPost("RegisterVehicleResident")]
        public async Task<Resident> PostRegisterVehicleResident(RegisterTypeVehicle registerTypeVehicle)
        {
            var result = await _vehicleRepository.RegisterVehicleResident(registerTypeVehicle);
            return result;
        }


    }
}