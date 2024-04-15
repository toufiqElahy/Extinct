using interwebz.Data;
using interwebz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace interwebz.Controllers
{
	[AllowAnonymous]
	public class CommunityController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly ApplicationDbContext _ctx;
		public CommunityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext ctx)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_ctx = ctx;
		}

		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Forums");
			}
				return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(string email, string password, string remember, string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;


			var user = _userManager.FindByEmailAsync(email).Result;
			if (user != null)
			{
				//if (!_userManager.IsEmailConfirmedAsync
				//	 (user).Result)
				//{
				//	var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				//	var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = code }, protocol: HttpContext.Request.Scheme);

				//	//var model = new ViewModels.emailVerification { UserName = user.FirstName + " " + user.LastName, Link = callbackUrl, Email = user.UserName };

				//	//string viewHtml = await this.RenderViewAsync("emailVerification", model);
				//	//await SendEmail.SendEmailAsync(user.UserName, viewHtml);
				//	////await SendEmail.SendEmailAsync(email, "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
				//	//TempData["msg"] = "Need Email Confirmation. A Confirmation email already sent." + Admin.EmailNotice;
				//	return View();
				//}
				var result = await _signInManager.PasswordSignInAsync(user,
			password, true, lockoutOnFailure: false);
				if (result.Succeeded)
				{
					//_logger.LogInformation(1, "User logged in.");
					return RedirectToAction(nameof(HomeController.Index), "Home");
					//return LocalRedirect(returnUrl);
				}
			}

			

			TempData["msg"] = "Invalid login attempt.";


			return RedirectToAction("Index");
		}

		public IActionResult Forums()
		{
			return View();
		}
		public IActionResult Announcement()
		{
			return View();
		}
		public IActionResult Single()
		{
			return View();
		}
		public IActionResult Knowledge_base()
		{
			return View();
		}
		public IActionResult Tickets()
		{
			return View();
		}
		public IActionResult Account()
		{
			return View();
		}
		public IActionResult Security()
		{
			return View();
		}
		public IActionResult Profile()
		{
			return View();
		}

		public IActionResult Error()
		{
			return View();
		}
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(string email, string password, string username)
		{

			var user = new ApplicationUser { UserName = username, Email = email };
			var result = await _userManager.CreateAsync(user, password);
			if (result.Succeeded)
			{
				// For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
				// Send an email with this link
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				var callbackUrl = Url.Action("ConfirmEmail", "Community", new { userId = user.Id, token = code, referral = Request.Query["referral"].ToString() }, protocol: HttpContext.Request.Scheme);
				return Redirect(callbackUrl);
				//var resendUrl = Url.Action("ReSendEmail", "Account", new { email }, protocol: HttpContext.Request.Scheme);

				//var model = new ViewModels.emailVerification { UserName = user.FirstName + " " + user.LastName, Link = callbackUrl, Email = user.UserName };

				//string viewHtml = await this.RenderViewAsync("emailVerification", model);
				//await SendEmail.SendEmailAsync(user.UserName, viewHtml);
				////await SendEmail.SendEmailAsync(email, "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
				//TempData["msg"] = "A Confirmation email already sent...Check your Email." + Admin.EmailNotice;
				return RedirectToAction(nameof(CommunityController.Index), "Community");
				//await _signInManager.SignInAsync(user, isPersistent: false);
				//_logger.LogInformation(3, "User created a new account with password.");
				//return RedirectToAction(nameof(HomeController.Index), "Home");

				//return Content("Please check email to confirm your account ");
			}
			else
			{
				TempData["msg"] = result.Errors.First().Description;//Newtonsoft.Json.JsonConvert.SerializeObject(result.Errors);
			}
			//AddErrors(result);


			return View();
		}
		public IActionResult ConfirmEmail(string userid, string token)
		{
			var user = _userManager.FindByIdAsync(userid).Result;
			if (_userManager.IsEmailConfirmedAsync
					 (user).Result)
			{
				TempData["msg"] = "Email Already Confirmed!";
				return RedirectToAction("Index");
			}
			IdentityResult result = _userManager.
					ConfirmEmailAsync(user, token).Result;
			if (result.Succeeded)
			{
				TempData["msg"] = "Email confirmed successfully!";
				return RedirectToAction("Index");
			}
			return RedirectToAction("Register");
		}
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			//_logger.LogInformation(4, "User logged out.");
			return RedirectToAction(nameof(HomeController.Index), "Home");
		}
		//[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		//      public IActionResult Error()
		//      {
		//          return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		//      }
	}
}
