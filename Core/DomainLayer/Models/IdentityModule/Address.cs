namespace DomainLayer.Models.IdentityModule
{
    public class Address
    {
        // Id, FirstName, LastName, Street, City, Country, User, UserId
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public string UserId { get; set; } = default!; //fk uniqe 

    }
}