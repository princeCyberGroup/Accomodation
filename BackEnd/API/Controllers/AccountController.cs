using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Entity;
using API.Interface;
using BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenServices _tokenServices;
        public AccountController(DataContext context,ITokenServices tokenServices)
        {
            _tokenServices = tokenServices;
            _context = context;
        }

        /*
         *   Registration for New intern account
         */
        private string ExtractMiddleFourDigits(string aadharCardNumber)
        {
            aadharCardNumber = aadharCardNumber.Replace("-", "");
            string middleDigits = aadharCardNumber.Substring(4, 4);

            return middleDigits;
        }
        private async Task<bool> userExist(string EmailId)
        {
            return await _context.Users.AnyAsync(x => x.EmailId == EmailId.ToLower());
        }

        [HttpPost("intern-registration")]
        public async Task<ActionResult<UserDTOs>> InternRegister([FromBody] InternRegisterDTOs register)
        {
            try
            {
                if (await userExist(register.emailId)) return BadRequest("User is already registered");
                using var hmacSalt = new HMACSHA512();
                string middleAadharDigits = ExtractMiddleFourDigits(register.aadharCardNumber);
                var passwordString = $"{register.firstName.ToLower()}.{middleAadharDigits}";

                byte[] passwrodByte = Encoding.UTF8.GetBytes(passwordString);

                var user = new AppUserRegister
                {
                    FirstName = register.firstName,
                    LastName = register.lastName,
                    AadharCardNumber = register.aadharCardNumber,
                    EmailId = register.emailId,
                    ContactNumber = register.contactNumber,
                    University_CollegeName = register.university_collegeName,
                    SaltedPassword = hmacSalt.Key,
                    // Password = passwrodByte,
                    Password = hmacSalt.ComputeHash(Encoding.UTF8.GetBytes(passwordString)),
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new UserDTOs
                {
                    EmailId = user.EmailId,
                    Token = _tokenServices.CreateToken(user)
                };
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }



        /*
        *   Login for account
        */
        [HttpPost("login")]
        public async Task<ActionResult<UserDTOs>> Login(LoginDTOs loginDetails)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.EmailId == loginDetails.EmailId);
                if (user == null)
                {
                    return Unauthorized("Invalid User Email: " + loginDetails.EmailId);
                }

                //decrypting the password
                using var hmac = new HMACSHA512(user.SaltedPassword);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDetails.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.Password[i]) return Unauthorized("Invalid Password");
                }
                return new UserDTOs
                {
                    EmailId = user.EmailId,
                    Token = _tokenServices.CreateToken(user)
                };
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
    }
}