using DB;
using DB.Models;
using Microsoft.EntityFrameworkCore;
using src.Parking.Api.DB.Models;
using src.Parking.Api.DB.Models.Enum;
using src.Parking.Api.Parking.Api.Core;
using src.Parking.Api.Parking.Api.Core.Dtos;
using src.Parking.Api.Parking.Api.Repository.Interface;

namespace src.Parking.Api.Parking.Api.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ParkingContex _context;

        public VehicleRepository(ParkingContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
           return await _context.Vehicles
            .Include(x => x.OfficialVehicle)
            .Include(x => x.Resident)
            .ToListAsync();

        }

        public async Task<OfficialVehicle?> GetOfficialVehicle(string PlateNumber)
        {
            return await _context.OfficialVehicles.Include(x => x.Instances)
            .SingleOrDefaultAsync(x => x.PlateNumber == PlateNumber);
        }


        public async Task<Vehicle> PostVehicle(CheckIn checkIn)
        {
            var updateVehicle = await UpdateVehicle(checkIn);

            if (updateVehicle != null) return updateVehicle;

            checkIn.PlateNumber = checkIn.PlateNumber == null ? "XXX-000000" : checkIn.PlateNumber;

            var updateOfficial = await UpdateVehicleOfficial(null, checkIn);

            var updateResident = await UpdateVehicleResident(null, checkIn);

            Vehicle vehicles = new Vehicle()
            {
                PlateNumber = checkIn.PlateNumber,            
                OfficialVehicle = new List<OfficialVehicle>(),
                Resident = new List<Resident>()
            };

            if (updateOfficial != null)
            {
                vehicles.OfficialVehicle.Add(updateOfficial);
            }

            if (updateResident != null)
            {
                vehicles.Resident.Add(updateResident);
            }


            await _context.Vehicles.AddAsync(vehicles);
            await _context.SaveChangesAsync();
            return vehicles;
        }

        public async Task<Vehicle> UpdateVehicle(CheckIn checkIn)
        {
            var vehicleExists = await _context.Vehicles
                                           .Include(ov => ov.Resident)
                                           .Include(ov => ov.OfficialVehicle)
                                           .FirstOrDefaultAsync(x => x.PlateNumber == checkIn.PlateNumber);

            if (vehicleExists == null) return null;

            var updateOfficial = await UpdateVehicleOfficial(null, checkIn);

            var updateResident = await UpdateVehicleResident(null, checkIn);

            Vehicle vehicles = new Vehicle()
            {
                PlateNumber = checkIn.PlateNumber,             
                OfficialVehicle = new List<OfficialVehicle>(),
                Resident = new List<Resident>()
            };

            if (updateOfficial != null)
            {
                vehicles.OfficialVehicle.Add(updateOfficial);
            }

            if (updateResident != null)
            {
                vehicles.Resident.Add(updateResident);
            }
            vehicleExists = vehicles;


            _context.Vehicles.Update(vehicleExists);
            await _context.SaveChangesAsync();
            return vehicles;
        }

        public async Task<OfficialVehicle> RegisterVehicleOfficial(RegisterTypeVehicle registerTypeVehicle)
        {

            OfficialVehicle officialVehicle = new OfficialVehicle()
            {
                PlateNumber = registerTypeVehicle.PlateNumber,
                VehicleType = TypeVehicleEnum.Official
            };

            _context.OfficialVehicles.Add(officialVehicle);

            await _context.SaveChangesAsync();
            return officialVehicle;
        }

        public async Task<Resident> RegisterVehicleResident(RegisterTypeVehicle registerTypeVehicle)
        {
            Resident residentVehicle = new Resident()
            {
                PlateNumber = registerTypeVehicle.PlateNumber,
                VehicleType = TypeVehicleEnum.Resident
            };

            _context.Residents.Add(residentVehicle);
            await _context.SaveChangesAsync();
            return residentVehicle;
        }

        public async Task<OfficialVehicle> UpdateVehicleOfficial(RegisterTypeVehicle registerTypeVehicle, CheckIn checkIn)
        {

            var officialVehicleUpdate = await _context.OfficialVehicles
                                           .Include(ov => ov.Instances)
                                           .FirstOrDefaultAsync(x => x.PlateNumber == checkIn.PlateNumber);

            if (officialVehicleUpdate == null) return null;

            Instance instance = new Instance()
            {
                Entrytime = checkIn.Entrytime
            };
            officialVehicleUpdate.Instances.Add(instance);

            _context.OfficialVehicles.Update(officialVehicleUpdate);

            await _context.SaveChangesAsync();

            return officialVehicleUpdate;

        }

        public async Task<Resident> UpdateVehicleResident(RegisterTypeVehicle registerTypeVehicle, CheckIn checkIn)
        {

            var residentVehicleUpdate = await _context.Residents
                                           .Include(ov => ov.Instances)
                                           .FirstOrDefaultAsync(x => x.PlateNumber == checkIn.PlateNumber);

            if (residentVehicleUpdate == null) return null;

            Instance instance = new Instance()
            {
                Entrytime = checkIn.Entrytime
            };
            residentVehicleUpdate.Instances.Add(instance);

            _context.Residents.Update(residentVehicleUpdate);

            await _context.SaveChangesAsync();

            return residentVehicleUpdate;

        }


        public async Task<Vehicle> PatchVehicle(CheckOut checkOut)
        {
            var officialVehicleUpdate = await _context.Vehicles
                                           .Include(x => x.OfficialVehicle)
                                           .FirstOrDefaultAsync(x => x.PlateNumber == checkOut.PlateNumber);

            var nose = await UpdateOfficialCheckOut(checkOut);                        

            Vehicle vehicles = new Vehicle()
            {
                VehicleId = officialVehicleUpdate.VehicleId,
                PlateNumber = checkOut.PlateNumber,               
                OfficialVehicle = new List<OfficialVehicle>(),
                Resident = new List<Resident>()
            };
            vehicles.OfficialVehicle.Add(nose);



            return vehicles;
        }

        public async Task<OfficialVehicle> UpdateOfficialCheckOut(CheckOut checkOut)
        {

            var officialVehicleUpdate = await _context.OfficialVehicles
                                           .Include(ov => ov.Instances)
                                           .FirstOrDefaultAsync(x => x.PlateNumber == checkOut.PlateNumber);

            if (officialVehicleUpdate == null) return null;

            var instance = officialVehicleUpdate.Instances.SingleOrDefault(x => x.InstanceId.Equals(checkOut.InstanceId));

            instance.DepartureTime = checkOut.DepartureTime;

            _context.OfficialVehicles.Update(officialVehicleUpdate);

            await _context.SaveChangesAsync();

            return officialVehicleUpdate;

        }
    }
}