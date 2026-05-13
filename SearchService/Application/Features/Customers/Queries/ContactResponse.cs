namespace Application.Features.Customers.Queries
{
    public class ContactResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public bool IsPrimary { get; set; }
    }
}
