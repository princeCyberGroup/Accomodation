using System.ComponentModel.DataAnnotations;

namespace API.Entity
{
    public class AppUserRegister
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [MaxLength(12)]
        public string AadharCardNumber { get; set; }

        [Required]
        public string EmailId { get; set; }

        [MaxLength(14), MinLength(10)]
        public string ContactNumber { get; set; }

        [MinLength(4)]
        public string University_CollegeName { get; set; }

        //  Deafult Password pattern =firstname.4(middle) digit of aadharcard
        [Required]
        public byte[] Password { get; set; }
        [Required]
        public byte[] SaltedPassword { get; set; }

        //  dotnet ef migrations remove
        //  dotnet ef migrations add <migration msg> --output-dir Data/Migrations/RegisterMig
        //  dotnet ef migrations remove
        //  dotnet ef migrations list
        //  dotnet ef database update

    }
}