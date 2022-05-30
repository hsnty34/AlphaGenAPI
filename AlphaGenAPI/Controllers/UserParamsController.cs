using AlphaGenAPI.Data;
using AlphaGenAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlphaGenAPI.Controllers
{
    public class UserParamsController : Controller
    {
        private readonly SqlContext _context;
        public IConfiguration _configuration;
        UserParams users;
        public UserParamsController(SqlContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }
        public IActionResult Index()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            if (_context.UserParams.Where(u => u.UserId == userId).FirstOrDefault() == null)
            { return RedirectToAction("Add"); }
            else
            {
                users = _context.UserParams.Where(u => u.UserId == userId).Select(x => x).FirstOrDefault();
                return View(users);
            }
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(UserParams _userData)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            _userData.UserId = id;
            _context.UserParams.Add(_userData); // Dependency Injection olan sqlcontextin referansı ile (EF) user modeli DB'YE add edildi.
            _context.SaveChanges(); // SQLdeki değişiklikleri kaydet.
            return RedirectToAction("Index"); // işlem başarılı ise listeye yönlendir.
        }

        [HttpPost]
        public IActionResult Edit(UserParams _userInApp)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            _userInApp.Id = id;
            _userInApp.UserId = id;
            _context.UserParams.Update(_userInApp);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
