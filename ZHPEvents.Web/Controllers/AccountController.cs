using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ZHPEvents.Core;
using ZHPEvents.Core.Identity;
using ZHPEvents.ViewModels.Account;

namespace ZHPEvents
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        #region Register

        public IActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// Register a new user with given data. If a certain parameter is null get it from the invitation.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Email,Password,ConfirmPassword,PrivacyAccept")] RegisterViewModel registerModel)
        {
            if (registerModel.PrivacyAccept != true)
            {
                return View();
            }

            /// Check if provided passwords match!
            if (registerModel.Password != registerModel.ConfirmPassword)
            {
                return View();
            }

            var user = new AppUser { UserName = registerModel.Email, Email = registerModel.Email };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action(
                    "ConfirmEmail",
                    "Account",
                    values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(registerModel.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                //await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("RegisterConfirmation", "Account", new { id = registerModel.Email });
            }
            return View();
        }

        [HttpGet("Account/RegisterConfirmation/{id}")]
        public ActionResult RegisterConfirmation(string id)
        {
            TempData["email"] = id;
            return View();
        }

        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password,Confirm password,PrivacyAccept")] LoginViewModel loginView)
        {
            if (string.IsNullOrEmpty(loginView.Email))
            {
                ModelState.AddModelError(string.Empty, "Błędna próba logowania");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(loginView.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Błędna próba logowania");
                return View();
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Adres email jest niepotwierdzony");
                return View();
            }

            var passwordSignInResult = await _signInManager.PasswordSignInAsync(user, loginView.Password, loginView.RememberMe, lockoutOnFailure: false);
            if (!passwordSignInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Błędna próba logowania");
                return View();
            }

            return Redirect("~/");
        }
        #endregion

        #region Logout

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        #endregion

        #region ConfirmEmail

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return View();
        }

        #endregion

        #region ForgotPassword

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword([Bind("Email")] ForgotPasswordViewModel forgotPasswordView)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordView.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View();
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = Url.Action(
                   "ResetPassword",
                   "Account",
                   values: new { code },
                   protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(
                    forgotPasswordView.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            return View();
        }

        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public IActionResult ResetPassword(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                ResetPasswordViewModel resetPasswordView = new ResetPasswordViewModel
                {
                    Code = code
                };
                return View(resetPasswordView);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([Bind("Email,Password,ConfirmPassword,Code")] ResetPasswordViewModel resetPasswordView)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordView.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordView.Code, resetPasswordView.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        #endregion

        #region Profile
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            bool isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            string Username = userName;

            ProfileViewModel profileView = new ProfileViewModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                IsEmailConfirmed = isEmailConfirmed
            };

            return View(profileView);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile([Bind("Email, PhoneNumber")] ProfileViewModel profileView)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var email = await _userManager.GetEmailAsync(user);
            if (profileView.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, profileView.Email);
                if (!setEmailResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (profileView.PhoneNumber != phoneNumber || phoneNumber == null)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, profileView.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);


            return RedirectToAction("ProfileConfirmation");
        }

        public async Task<ActionResult> ProfileConfirmation()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            bool isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            string Username = userName;

            ProfileViewModel profileView = new ProfileViewModel
            {
                Email = email,
                PhoneNumber = phoneNumber,
                IsEmailConfirmed = isEmailConfirmed
            };

            return View(profileView);
        }

        #endregion

        #region ChangePassword

        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([Bind("OldPassword, NewPassword, ConfirmPassword")] ChangePasswordViewModel changePasswordView)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordView.OldPassword, changePasswordView.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("ChangePasswordConfirmation");

        }

        [Authorize]
        public async Task<ActionResult> ChangePasswordConfirmation()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }
            return View();
        }

        #endregion
        
        //#region AboutMe

        //[Authorize]
        //public async Task<IActionResult> AboutMe()
        //{

        //}

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> ChangeData(string pass, string firstName, string surname)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var passwordVerificationResult = await _signInManager.CheckPasswordSignInAsync(user, pass, false);

        //    if (passwordVerificationResult.Succeeded)
        //    {
        //        if (user.FirstName != firstName)
        //        {
        //            logger.Info($"User changed its FirstName: {user.FirstName} -> {firstName}");
        //            user.FirstName = firstName;
        //        }
        //        if (user.Surname != surname)
        //        {
        //            logger.Info($"User changed its Surname: {user.Surname} -> {surname}");
        //            user.Surname = surname;
        //        }

        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, _localizer["badPassword"]);
        //    }

        //    var viewModel = user.GetViewModel();
        //    viewModel.Company = _context.Companies.Where(c => c.Id == user.CompanyId).FirstOrDefault();

        //    return View("AboutMe", viewModel);
        //}

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> ChangePass(string oldPass, string pass, string pass2)
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var passwordVerificationResult = await _signInManager.CheckPasswordSignInAsync(user, oldPass, false);

        //    if (passwordVerificationResult.Succeeded)
        //    {
        //        if (pass == pass2)
        //        {
        //            await _userManager.ChangePasswordAsync(user, oldPass, pass);
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, _localizer["differentPass"]);
        //        }
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, _localizer["badPassword"]);
        //    }

        //    var viewModel = user.GetViewModel();
        //    viewModel.Company = _context.Companies.Where(c => c.Id == user.CompanyId).FirstOrDefault();

        //    return View("AboutMe", viewModel);
        //}

        //[Authorize]
        //[HttpPost]
        //public async Task<IActionResult> RemoveAccount(string pass)
        //{
        //    var user = await _userManager.GetUserAsync(User);

        //    logger.Info($"User with email {user.Email} removed itself");

        //    _context.Users.Remove(user);
        //    _context.SaveChanges();

        //    try
        //    {
        //        await _signInManager.SignOutAsync();
        //    }
        //    catch { }

        //    return Redirect("~/");
        //}


        //#endregion


    }
}