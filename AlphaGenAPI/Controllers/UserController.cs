using AlphaGenAPI.Data;
using AlphaGenAPI.Models;
using AlphaGenAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlphaGenAPI.Controllers
{
    public class UserController : Controller
    {
        private readonly SqlContext _context;
        public IConfiguration _configuration;
        public UserController(SqlContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        public IActionResult Index()
        {
            List<User> users = _context.Users.ToList();
            return View(users);
        }
        [Authorize]
        public IActionResult Success()
        { return View(); }

        [HttpGet]
        public IActionResult Create()
        {
            User usser = new User();
            return View(usser);
        }

        [HttpPost]
        public IActionResult Create(User _userData)
        {
            var checkNick = _context.Users.FirstOrDefault(u => u.Nick == _userData.Nick);
            if (checkNick != null)
            {
                return Ok("Kullanıcı Adı Zaten Kayıtlı");

            }
            else
            {

                _userData.Password = CryptographyService.Encrypt(_userData.Password); // password bilgisi encrypted gitti.
                _context.Add(_userData); // Dependency Injection olan sqlcontextin referansı ile (EF) user modeli DB'YE add edildi.
                _context.SaveChanges(); // SQLdeki değişiklikleri kaydet.
                
                return RedirectToAction("Success"); // işlem başarılı ise Create/Succese'e yönlendir.
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User _userData)
        {
            if (_userData != null && _userData.Nick != null && _userData.Password != null)
            {
                var user = GetUser(_userData.Nick, _userData.Password);

                if (user != null)
                {
                    //User bilgileriyle eşsiz bir JWT üretmek için claimler oluşturulur.
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
                    };
                    //Claimler ve configdeki bilgiler ile token basılır ve dönülür.
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(40),
                    signingCredentials: signIn);
                    var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
                    HttpContext.Session.SetString("JWToken",accessToken);
                    HttpContext.Session.SetString("Nick", _userData.Nick);
                    HttpContext.Session.SetString("Id", user.UserId.ToString());
                    return RedirectToAction("Index","Home");

                }
                else
                {    //Email ve Password eşleşmedi

                    return BadRequest("Kullanıcı adı veya şifre hatalı!");
                }
            }
            else
            {   // Bir hata oldu ve BadRequest response üretildi.
                return BadRequest();
            }
        }
        private  User GetUser(string nick, string password)
        {  // Email ve şifre eşleşmesi için Dbden sorgu yapan ef kodu

           string encryptPass = CryptographyService.Encrypt(password);
            return _context.Users.Where(u => u.Nick == nick && u.Password == encryptPass).Select(x=>x).FirstOrDefault();

        }
        
       
    }
}
