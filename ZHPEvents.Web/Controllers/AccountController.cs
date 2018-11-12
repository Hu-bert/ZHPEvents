//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using ZHPEvents.Core;
//using ZHPEvents.Core.Identity;

//namespace ZHPEvents
//{
//    [Authorize(Roles = "Administrator")]
//    public class Account : Controller
//    {
//        private readonly Context _context;
//        private readonly UserManager<AppUser> _userManager;

//        public Account(Context context, UserManager<AppUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }


//        public async Task<IActionResult> ConfirmEmail(string userId, string code)
//        {
//            if (userId == null || code == null)
//            {
//                return RedirectToPage("/Index");
//            }

//            var user = await _userManager.FindByIdAsync(userId);
//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{userId}'.");
//            }

//            var result = await _userManager.ConfirmEmailAsync(user, code);
//            if (!result.Succeeded)
//            {
//                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
//            }

//            return View();
//        }
//    }
//}