using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using MVCPresentationLayer.Models;

namespace MVCPresentationLayer.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

        // GET: Admin
        public ActionResult Index()
        {
            //return View(db.ApplicationUsers.ToList());
            _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return View(_userManager.Users.OrderBy(n => n.UserName).ToList());
        }

        // GET: Admin/Details/5
        
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = _userManager.FindById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            var usrMgr = new LogicLayer.UserManager();
            var allRoles = usrMgr.RetrieveEmployeeRoles();
            var roles = _userManager.GetRoles(id);
            var noRoles = allRoles.Except(roles);

            ViewBag.Roles = roles;
            ViewBag.NoRoles = noRoles;

            return View(applicationUser);
        }

        public ActionResult RemoveRole(string id, string role)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = userManager.Users.First(u => u.Id == id);

            if (role == "Admin")
            {
                var allUsers = userManager.Users.ToList();
                var adminUsers = allUsers
                    .Where(u => userManager.IsInRole(u.Id, "Admin"))
                    .ToList().Count();
                if(adminUsers < 2)
                {
                    ViewBag.Error = "Cannot remove last administrator.";
                    return RedirectToAction("Details", "Admin", new { id = user.Id });
                }
                
            }
            userManager.RemoveFromRole(id, role);
            if(user.UserID != null)
            {
                try
                {
                    var usrMgr = new LogicLayer.UserManager();
                    usrMgr.RemoveUserRole((int)user.UserID, role);
                }
                catch (Exception)
                {
                    //nothing
                }
            }

            return RedirectToAction("Details", "Admin", new { id = user.Id});

            //var usrMgr = new LogicLayer.UserManager();
            //var allRoles = usrMgr.RetrieveEmployeeRoles();

            //var roles = userManager.GetRoles(id);
            //var noRoles = allRoles.Except(roles);

            //ViewBag.Roles = roles;
            //ViewBag.NoRoles = noRoles;

            //return View("Details", user);
        }

        public ActionResult AddRole(string id, string role)
        {
            _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = _userManager.Users.First(u => u.Id == id);

            _userManager.AddToRole(user.Id, role);

            if (user.UserID != null)
            {
                try
                {
                    var usrMgr = new LogicLayer.UserManager();
                    usrMgr.AddUserRole((int)user.UserID, role);
                }
                catch (Exception)
                {
                    //nothing
                }
            }

            return RedirectToAction("Details", "Admin", new { id = user.Id });

            //var usrMgr = new LogicLayer.UserManager();
            //var allRoles = usrMgr.RetrieveEmployeeRoles();

            //var roles = _userManager.GetRoles(id);
            //var noRoles = allRoles.Except(roles);

            //ViewBag.Roles = roles;
            //ViewBag.NoRoles = noRoles;

            //return View("Details", user);
        }

    }
}
