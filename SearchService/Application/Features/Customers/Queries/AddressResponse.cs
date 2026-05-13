namespace Application.Features.Customers.Queries
{
    public class AddressResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string DistrictName { get; set; }
        public string CityName { get; set; }
        public string Street { get; set; }
        public string HouseName { get; set; }
        public string Description { get; set; }
    }
}
