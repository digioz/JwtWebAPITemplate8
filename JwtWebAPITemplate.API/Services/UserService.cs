using JwtWebAPITemplate.API.Models;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtWebAPITemplate.API.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        //private readonly JwtWebAPITemplateDbContext db;

        public UserService(IOptions<AppSettings> appSettings) //, JwtWebAPITemplateDbContext _db)
        {
            _appSettings = appSettings.Value;
            //db = _db;
        }

        public Task<List<User>> GetAll()
        {
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test", isActive = true }
            };

            return Task.FromResult(users.Where(x => x.isActive).ToList());
        }

        public async Task<User?> GetById(int id)
        {
            var user = new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test", isActive = true };
            return user;
        }

        //public async Task<User?> AddAndUpdateUser(User userObj)
        //{
        //    bool isSuccess = false;
        //    if (userObj.Id > 0)
        //    {
        //        var obj = await db.Users.FirstOrDefaultAsync(c => c.Id == userObj.Id);
        //        if (obj != null)
        //        {
        //            // obj.Address = userObj.Address;
        //            obj.FirstName = userObj.FirstName;
        //            obj.LastName = userObj.LastName;
        //            db.Users.Update(obj);
        //            isSuccess = await db.SaveChangesAsync() > 0;
        //        }
        //    }
        //    else
        //    {
        //        await db.Users.AddAsync(userObj);
        //        isSuccess = await db.SaveChangesAsync() > 0;
        //    }

        //    return isSuccess ? userObj : null;

        //}

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
        {
            var user = new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test", isActive = true };

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = await generateJwtToken(user);

            return new AuthenticateResponse(user, token);

        }

        private async Task<string> generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = await Task.Run(() =>
            {

                var key = Encoding.ASCII.GetBytes(_appSettings.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _appSettings.Issuer,
                    Audience = _appSettings.Audience,
                    Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                return tokenHandler.CreateToken(tokenDescriptor);
            });

            return tokenHandler.WriteToken(token);

        }

        Task<IEnumerable<User>> IUserService.GetAll()
        {
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test", isActive = true }
            };

            return Task.FromResult(users.Where(x => x.isActive));
        }
    }
}
