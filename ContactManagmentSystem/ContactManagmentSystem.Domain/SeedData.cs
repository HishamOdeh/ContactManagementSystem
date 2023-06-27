
using ContactManagementSystem.Domain.Models;

namespace ContactManagementSystem.Domain
{    
    public static class SeedData
    {
        public static Guid HeshoId = Guid.Parse("c9895b85-9304-42b7-a5d9-08db6cf9923a");

        public static Contact Hesho => new Contact()
        {
            Id = HeshoId,
            LastName = "Odeh",
            FirstName = "Hesho",
            PhoneNumber = "123-456-7890",
            Email = "HeshoOdeh@hesho.com",
            BirthDate = DateTime.Parse("11/09/2001"),
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now,
        };

    }
}