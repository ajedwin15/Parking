namespace src.Parking.Api.Parking.Api.Core.Dtos
{
    public class CheckOut
    {
        public int InstanceId { get; set; }
        public string? PlateNumber { get; set; }
        public DateTime? DepartureTime { get; set; }
    }
}