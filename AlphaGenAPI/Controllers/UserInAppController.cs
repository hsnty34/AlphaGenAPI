using AlphaGenAPI.Data;
using AlphaGenAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlphaGenAPI.Controllers
{
    public class UserInAppController : Controller
    {
        private readonly SqlContext _context;
        public IConfiguration _configuration;
        UserInApp users;
        public UserInAppController(SqlContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }
        public IActionResult Index()
        {
            int userId = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            if (_context.UserInApps.Where(u => u.UserId == userId).FirstOrDefault() == null)
            { return RedirectToAction("Add"); }
            else
            {
                users = _context.UserInApps.Where(u => u.UserId == userId).Select(x => x).FirstOrDefault();
                return View(users);
            }

        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(UserInApp _userData)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            _userData.UserId = id;
            _context.UserInApps.Add(_userData); // Dependency Injection olan sqlcontextin referansı ile (EF) user modeli DB'YE add edildi.
            _context.SaveChanges(); // SQLdeki değişiklikleri kaydet.
            return RedirectToAction("Index"); // işlem başarılı ise listeye yönlendir.


        }
        public UserInApp GetUserInAppsById()
        {
            int id = Convert.ToInt32(Request.Cookies["Id"]);
            return _context.UserInApps.SingleOrDefault(e => e.Id == id);
        }

        public IActionResult Edit()
        { return View(); }


        // public IActionResult Edit(UserInApp _userData)

        // {
        //   int userId = Convert.ToInt32(HttpContext.Session.GetString("Id"));

        // _context.UserInApps.Where(x => x.Id == userId);
        //   _context.SaveChanges();
        // return RedirectToAction("Index");
        //    int id = Convert.ToInt32(HttpContext.Session.GetString("Id"));
        // _userData.UserId = id;
        //    _context.UserInApps.Update(_userData); // Dependency Injection olan sqlcontextin referansı ile (EF) user modeli DB'YE add edildi.
        //    _context.SaveChanges(); // SQLdeki değişiklikleri kaydet.
        //    return RedirectToAction("Index"); // işlem başarılı ise listeye yönlendir.
        //int userId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
        //_userData.UserId = userId;
        //users = _context.UserInApps.Where(x => x.Id == userId).SingleOrDefault();
        //_context.Entry(users).State = EntityState.Modified;
        //_context.UserInApps.Update(_userData);
        //_context.SaveChanges();
        //return RedirectToAction("Index");

        // }
        [HttpPost]
        public  IActionResult Edit(UserInApp _userInApp)
        {
            int id = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            _userInApp.Id = id;
            _userInApp.UserId = id;
            _context.UserInApps.Update(_userInApp);
            _context.SaveChanges();
            return RedirectToAction("Index");
            // var user = new UserInApp() { Id = id, Salon = _userInApp.Salon, Adres = _userInApp.Adres };
            // if (_userInApp.Adres == null)
            // {

            //     _context.Entry(user).Property(x => x.Salon).IsModified = true;
            // }
            //if (_userInApp.Salon == null )
            // {
            //     _context.UserInApps.Attach(user);
            //     _context.Entry(user).Property(x => x.Adres).IsModified = true;

            // }
            //if  (_userInApp.Salon !=null && _userInApp.Adres !=null)
            // {
            //     _context.Entry(user).Property(x => x.Salon).IsModified = true;
            //     _context.Entry(user).Property(x => x.Adres).IsModified = true;
            // }
            // _context.SaveChanges();
            //   return RedirectToAction("Index");
            //     //Where(x => x.Id == id)

        }
    }
}
