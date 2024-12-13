using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PurpleBuzzPr.Abstractions;
using PurpleBuzzPr.DAL;
using PurpleBuzzPr.DTOs.UserDTOs;
using PurpleBuzzPr.Models;
using PurpleBuzzPr.Utilities;
using System.Runtime.InteropServices;

namespace PurpleBuzzPr.Controllers
{

    public class AccountController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        ILogger<AccountController> _logger;

        public AccountController(AppDbContext appDbContext, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager,
            IEmailService emailService, ILogger<AccountController> logger)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
            _logger = logger;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto createUserDto)
        {
            //if(createUserDto.Age < 18)
            //{
            //    ModelState.AddModelError("Age", "Age must be at least 18");
            //}
            if (!ModelState.IsValid)
            {
                return View(createUserDto);
            }
            AppUser user = new AppUser();
            user.FirstName = createUserDto.FirstName;
            user.LastName = createUserDto.LastName; 
            user.Email = createUserDto.Email;
            user.UserName = createUserDto.Username;

            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }
                return View(createUserDto);
            }
            await _userManager.AddToRoleAsync(user, RoleEnums.User.ToString());
            _emailService.SendWelcome(user.Email);

            string userToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string? url = Url.Action("ConfirmEmail", "Account", new {userId=user.Id, token= userToken },Request.Scheme );
            _emailService.SendConfirmEmail(user.Email, url);

            return RedirectToAction(nameof(Index), "Home");
        }
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            AppUser? user = await  _userManager.FindByIdAsync(userId);
            if (user==null)
            {
                return BadRequest("Problem occured");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Ok("Confirmed email");
            }
            return BadRequest("Problem occured");
        }
       

        public IActionResult Login()
        { 
            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto, string? ReturnUrl) 
        {
           
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser? user = await _userManager.FindByNameAsync(loginUserDto.EmailOrUsername); 
            if (user == null)
            {
                
                user = await _userManager.FindByEmailAsync(loginUserDto.EmailOrUsername);
                if (user == null)
                {

                    _logger.LogWarning("ehtiyatli ol");
                }
            }
            if (!user.EmailConfirmed)
            {
                return BadRequest("Please confirm your email");
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginUserDto.Password, loginUserDto.IsPersistant, true);
            //Response.Cookies.Append("UserName", user.UserName, new CookieOptions()
            //{
            //    Expires = DateTime.Now.AddSeconds(5),
            //});
            //HttpContext.Session.SetString("SessionUserName", user.UserName);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username or password is wrong");
                return View();
            }
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }



            return RedirectToAction(nameof(Index), "Home"); 
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index), "Home");
        }

        //public async Task CreateRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Manager" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //}

        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser user = new AppUser();
        //    user.UserName = "SuperAdmin";
        //    user.Email = "admin@purplebuzz.com";
        //    user.FirstName = "Nermin";
        //    user.LastName = "Shivakhan";
        //    var result = await _userManager.CreateAsync(user, "Admin123!");
        //    if (!result.Succeeded)
        //    {
        //        return Json(result);
        //    }
        //    await _userManager.AddToRoleAsync(user, RoleEnums.Admin.ToString());
        //    return Json("Success");
        //}




    }
}
