using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using netcore_ecommerce.DTO;
using netcore_ecommerce.Models;

namespace netcore_ecommerce.Controllers {
    [Authorize]
    public class AccountController: Controller {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager) {
            _userManager = userManager;
        }

        // GET: AccountController
        public async Task<ActionResult> Index() {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            AppUserEdit appUserEdit = new AppUserEdit();
            appUserEdit.FirstName = values.FirstName;
            appUserEdit.LastName = values.LastName;
            appUserEdit.PhoneNumber = values.PhoneNumber;
            appUserEdit.Email = values.Email;
            appUserEdit.City = values.City;
            return View(appUserEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Index(AppUserEdit appUserEdit) {
            if(appUserEdit.Password == appUserEdit.ConfirmPassword) {
                var user = await _userManager.FindByEmailAsync(appUserEdit.Email);
                user.FirstName = appUserEdit.FirstName;
                user.LastName = appUserEdit.LastName;
                user.City = appUserEdit.City;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEdit.Password);
                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded) {
                    return RedirectToAction("Index", "Account");
                }
            }

            return View();
        }
    }
}