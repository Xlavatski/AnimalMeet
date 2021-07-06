using AnimalMeetAPI.Data;
using AnimalMeetAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace AnimalMeetAPI.Repository.IRepository
{
    public class AuthUserRepository : IAuthUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSetting _appSetting;

        public AuthUserRepository(ApplicationDbContext db, IOptions<AppSetting> appSetting)
        {
            _db = db;
            _appSetting = appSetting.Value;
        }

        public ApplicationUser Authenticate(string username, string password)
        {
            var user = _db.ApplicationUsers.SingleOrDefault(x => x.Username == username);

            if (user == null || !BC.Verify(password, user.Password)) 
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSetting.Secret);
            var tokekDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokekDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.ApplicationUsers.SingleOrDefault(x => x.Username == username);

            if (user == null) 
            {
                return true;
            }
            return false;
        }

        public ApplicationUser Register(string username, string password, string name, string surname, int? cityId)
        {
            ApplicationUser userObj = new ApplicationUser()
            {
                Username = username,
                Password = password,
                Name = name,
                Surname = surname,
                CityId = cityId,
                Role = "User"
            };

            userObj.Password = BC.HashPassword(password);

            _db.ApplicationUsers.Add(userObj);
            _db.SaveChanges();
            userObj.Password = "";
            return userObj;
        }
    }
}
