//using E_Commerce__Mini_Proj_.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;

//namespace E_Commerce__Mini_Proj_.Controllers
//{

//    public class HomeController : Controller
//    {
//        private Entities db = new Entities();
//        // GET: Home
//        public ActionResult Index()
//        {
//            return View();
//        }
//        public ActionResult Login()
//        {
//            return View();
//        }
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Login(User model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
//                if (user != null)
//                {

//                    Session["LoggedInUser"] = user.UserID;
//                    // Set session variables
//                    Session["isLogin"] = true;
//                    Session["UserEmail"] = user.Email;

//                    // Optionally set other user details in session if needed
//                    // Session["UserName"] = user.UserName;

//                    // Redirect to the home page
//                    return RedirectToAction("Index", "Home");
//                }
//                ModelState.AddModelError("", "Invalid login attempt.");
//            }
//            return View("Index"); // Assuming you still have an Index view for the home page
//        }

//        public ActionResult Logout()
//        {
//            // Clear session on logout
//            Session.Clear();

//            // Redirect to the home page
//            return RedirectToAction("Index", "Home");
//        }


//        // Register POST Action
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Register(string Name, string Email, string Password)
//        {
//            if (ModelState.IsValid)
//            {
//                // Check if user already exists
//                var existingUser = db.Users.FirstOrDefault(u => u.Email == Email);
//                if (existingUser == null)
//                {
//                    // Create new user
//                    var newUser = new User { UserName = Name, Email = Email, Password = Password };

//                    // Add user to the database
//                    db.Users.Add(newUser);
//                    db.SaveChanges();

//                    // Log the user in
//                    FormsAuthentication.SetAuthCookie(newUser.Email, false);

//                    // Redirect to Home after registration
//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Email is already registered.");
//                }
//            }

//            // Return error if registration fails
//            return View("Index");
//        }

//        // Logout Action
//        //public ActionResult Logout()
//        //{
//        //    // Sign out the user
//        //    FormsAuthentication.SignOut();

//        //    // Redirect to Home after logout
//        //    return RedirectToAction("Index", "Home");
//        //}

//        [Authorize]
//        public ActionResult Profile()
//        {
//            var userId = Session["LoggedInUser"];
//            var user = db.Users.Find(userId);

//            //var email = User.Identity.Name;
//            //var user = db.Users.FirstOrDefault(u => u.Email == email);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }
//            return View(user);
//        }

//        // GET: EditProfile
//        [Authorize]
//        [HttpGet]
//        public ActionResult EditProfile()
//        {
//            var email = User.Identity.Name;
//            var user = db.Users.FirstOrDefault(u => u.Email == email);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }
//            return View(user);
//        }

//        // POST: EditProfile
//        [Authorize]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult EditProfile(User model)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = db.Users.Find(model.UserID);

//                if (user != null)
//                {
//                    user.UserName = model.UserName;
//                    user.Email = model.Email;
//                    user.Password = model.Password;
//                    user.Address = model.Address;

//                    db.SaveChanges();
//                    return RedirectToAction("Profile");
//                }
//            }

//            return View(model);
//        }

//        // POST: DeleteProfile
//        [Authorize]
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteProfile()
//        {
//            var email = User.Identity.Name;
//            var user = db.Users.FirstOrDefault(u => u.Email == email);

//            if (user != null)
//            {
//                db.Users.Remove(user);
//                db.SaveChanges();

//                // Log the user out after account deletion
//                FormsAuthentication.SignOut();
//                return RedirectToAction("Index", "Home");
//            }

//            return RedirectToAction("Profile");
//        }

//    public ActionResult Shop()
//        {
//            return View();
//        }

//        public ActionResult PD()
//        {
//            return View();
//        }
//        public ActionResult Cart()
//        {
//            return View();
//        }
//        public ActionResult Checkout()
//        {
//            return View();
//        }

//    }
//}

using E_Commerce__Mini_Proj_.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace E_Commerce__Mini_Proj_.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    // Set session variables
                    Session["LoggedInUser"] = user.UserID;
                    Session["isLogin"] = true;
                    Session["UserEmail"] = user.Email;

                    // Redirect to the profile page
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return View(model); // Return to login view with model errors
        }

        // GET: Logout
        public ActionResult Logout()
        {
            // Clear session on logout
            Session.Clear();
            FormsAuthentication.SignOut();

            // Redirect to the home page
            return RedirectToAction("Index");
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string Name, string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                // Check if user already exists
                var existingUser = db.Users.FirstOrDefault(u => u.Email == Email);
                if (existingUser == null)
                {
                    // Create new user
                    var newUser = new User { UserName = Name, Email = Email, Password = Password };

                    // Add user to the database
                    db.Users.Add(newUser);
                    db.SaveChanges();

                    // Log the user in
                    FormsAuthentication.SetAuthCookie(newUser.Email, false);

                    // Set session variables
                    Session["LoggedInUser"] = newUser.UserID;
                    Session["isLogin"] = true;
                    Session["UserEmail"] = newUser.Email;

                    // Redirect to profile page after registration
                    return RedirectToAction("Profile");
                }
                else
                {
                    ModelState.AddModelError("", "Email is already registered.");
                }
            }

            // Return error if registration fails
            return View("Index"); // Assuming you have an Index view for registration errors
        }


        // GET: Profile
        //[Authorize]
        //public ActionResult Profile(int id)
        //{
        //    var userId = Session["LoggedInUser"] as int?;
        //    if (userId == null)
        //    {
        //        return RedirectToAction("Index","Home");
        //    }
        //    var user = db.Users.FirstOrDefault(u=>u.UserID == userId);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}
        public ActionResult Profile()
        {
            var userId = Session["LoggedInUser"] as int?;
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = db.Users.FirstOrDefault(u => u.UserID == userId);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        //[Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            var userId = Session["LoggedInUser"] as int?;
            if (userId == null)
            {
                return HttpNotFound();
            }
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: EditProfile
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(User model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Find(model.UserID);

                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.Address = model.Address;

                    db.SaveChanges();
                    return RedirectToAction("Profile");
                }
            }

            return View(model);
        }

        // POST: DeleteProfile
        //[Authorize]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteProfile()
        //{
        //    var userId = Session["LoggedInUser"] as int?;
        //    if (userId == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var user = db.Users.Find(userId);

        //    if (user != null)
        //    {
        //        db.Users.Remove(user);
        //        db.SaveChanges();

        //        // Log the user out after account deletion
        //        FormsAuthentication.SignOut();
        //        Session.Clear();
        //        return RedirectToAction("Index");
        //    }

        //    return RedirectToAction("Profile");
        //}

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult PD()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateCart(int cartItemId, int newQuantity)
        {
            var cartItem = db.CartItems.Find(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = newQuantity;
                cartItem.TtlPrice = cartItem.Quantity * cartItem.Product.Price; // Assuming Product has Price
                db.SaveChanges();
            }

            // Recalculate the total price of the cart
            var cart = cartItem.Cart;
            cart.TotalPrice = cart.CartItems.Sum(ci => ci.TtlPrice);
            db.SaveChanges();

            return RedirectToAction("Cart");
        }


        public ActionResult Checkout()
        {
            return View();
        }
        
    }
}
