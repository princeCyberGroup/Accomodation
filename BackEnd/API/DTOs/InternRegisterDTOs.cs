
using System.ComponentModel.DataAnnotations;


namespace API.DTOs
{
    public class InternRegisterDTOs
    {
        // {
        // "firstName": "Prince",
        // "lastName": "Kumar",
        // "aadharCardNumber": "1234-5678-90123",
        // "EmailId": "prince.11901775@lpu.in",
        // "contactNumber": "7254962644",
        // "university_collegeName": "LPU"
        // }
        [Required]
        public string firstName { get; set; }
        public string lastName { get; set; }
        [StringLength(12)]
        [Required]
        public string aadharCardNumber { get; set; }
        [Required]
        public string emailId { get; set; }
        [MinLength(10)]
        public string contactNumber { get; set; }
        [Required]
        [MinLength(4)]
        public string university_collegeName { get; set; }
    }
}