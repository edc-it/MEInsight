using System;
using System.Linq;
using System.Threading.Tasks;
using MEL.Data;
using MEL.Entities.Identity;
//using MEL.Web.Areas.Identity;
using MEL.Web.Areas.Settings.Models.ViewModels;
using MEL.Web.Controllers;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

namespace MEL.Web.Areas.Settings.Controllers
{
    [Area("Settings")]
    //[Authorize(Policy = "RequireAdministratorRole")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UsersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager
            )
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();

            var usersList = from user in users
                            join organization in _context.Organizations
                                on user.OrganizationId equals organization.OrganizationId into userorganization
                            from u in userorganization.DefaultIfEmpty()
                            select new UsersListViewModel
                            {
                                Id = user.Id,
                                Email = user.Email,
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                OrganizationId = user?.OrganizationId ?? null,
                                Organization = u?.OrganizationName ?? null,
                                Role = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Name
                            };

            return View(usersList.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return base.View(model: new UsersListViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Organization = user.OrganizationId == null ? null : _context.Organizations.Single(o => o.OrganizationId == user.OrganizationId).OrganizationName,
                Role = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Name ?? null,
                ModifiedBy = user.ModifiedBy,
                ModifiedDate = user.ModifiedDate,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                AccessFailedCount = user.AccessFailedCount,
                LockoutEnabled = user.LockoutEnabled,
                LockoutEnd = user.LockoutEnd

            });
        }

        // GET: ApplicationUsers/Create
        public async Task<IActionResult> Create()
        {
            ViewData["RoleId"] = new SelectList(_roleManager.Roles
                .Where(x => x.Name != "Administrator")
                .Select(x => new
                {
                    x.Id,
                    Name = x.Name + " - " + x.Description
                }), "Id", "Name");

            ViewData["OrganizationId"] = new SelectList(_context.Organizations
                .Where(x => x.IsOrganizationUnit == true), "OrganizationId", "OrganizationName");

            // *** For JSTree ***
            //Logged User OrganizationId
            Guid? userOrganizacionId = (await _userManager.GetUserAsync(HttpContext.User))?.OrganizationId;

            if (userOrganizacionId == null)
            {
                return NotFound();
            }

            //Get Logged User Organization
            var organization = await _context.Organizations
                    .SingleOrDefaultAsync(o => o.OrganizationId == userOrganizacionId);

            if (organization == null)
            {
                return NotFound();
            }

            //Returns the top hiearchy organization for JSTree
            ViewData["ParentOrganizationId"] = organization.OrganizationId;

            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsersRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    OrganizationId = model.OrganizationId,
                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now
                };

                var createUser = await _userManager.CreateAsync(user, model.Password);

                if (createUser.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    ApplicationRole role = await _roleManager.FindByIdAsync(model.RoleId.ToString());

                    if (role != null)
                    {
                        var addRoles = await _userManager.AddToRoleAsync(user, role.Name);

                        if (!addRoles.Succeeded)
                        {

                            AddErrors(addRoles);

                            ViewData["RoleId"] = new SelectList(_roleManager.Roles
                                //.Where(x => x.Name != "Administrator")
                                .Select(x => new
                                {
                                    x.Id,
                                    Name = x.Name + " - " + x.Description
                                }), "Id", "Name");

                            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationName");

                            return RedirectToAction(nameof(Index));
                        }
                    }

                    TempData["messageType"] = "success";
                    TempData["messageTitle"] = "RECORD CREATED";
                    TempData["message"] = "New record created successfully";

                    return RedirectToAction(nameof(Index));
                }

                AddErrors(createUser);
            }

            ViewData["RoleId"] = new SelectList(_roleManager.Roles
                .Where(x => x.Name != "Administrador")
                .Select(x => new
                {
                    x.Id,
                    Name = x.Name + " - " + x.Description
                }), "Id", "Name");

            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationName");

            return View(model);
        }

        // GET: ApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UsersEditViewModel model = new UsersEditViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                OrganizationId = user.OrganizationId,
                RoleId = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Id
            };

            ViewData["RoleId"] = new SelectList(_roleManager.Roles
                //.Where(x => x.Name != "Administrator")
                .Select(x => new
                {
                    x.Id,
                    Name = x.Name + " - " + x.Description
                }), "Id", "Name", model.RoleId);

            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationName");

            return View(model);

        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UsersEditViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.Email;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.OrganizationId = model.OrganizationId;
                user.ModifiedBy = User.Identity.Name;
                user.ModifiedDate = DateTime.Now;

                string existingRole = _userManager.GetRolesAsync(user).Result.Single();
                Guid existingRoleId = _roleManager.Roles.Single(r => r.Name == existingRole).Id;
                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    if (existingRoleId != model.RoleId)
                    {
                        IdentityResult roleResult = await _userManager.RemoveFromRoleAsync(user, existingRole);
                        if (roleResult.Succeeded)
                        {
                            //ToString
                            ApplicationRole applicationRole = await _roleManager.FindByIdAsync(model.RoleId.ToString());
                            if (applicationRole != null)
                            {
                                IdentityResult newRoleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);
                                if (newRoleResult.Succeeded)
                                {
                                    TempData["messageType"] = "success";
                                    TempData["messageTitle"] = "RECORDS UPDATED";
                                    TempData["message"] = "Records updated successfully";
                                }
                            }
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["RoleId"] = new SelectList(_roleManager.Roles
                //.Where(x => x.Name != "Administrator")
                .Select(x => new
                {
                    x.Id,
                    Name = x.Name + " - " + x.Description
                }), "Id", "Name", model.RoleId);

            ViewData["OrganizationId"] = new SelectList(_context.Organizations, "OrganizationId", "OrganizationName");

            return View(model);
        }

        // GET: Admin/Users/Reset/5
        public async Task<IActionResult> Reset(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            UsersResetPasswordViewModel model = new UsersResetPasswordViewModel
            {
                Email = user.Email
            };

            return View(model);

        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reset(string id, UsersResetPasswordViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

                if (result.Succeeded)
                {
                    TempData["messageType"] = "success";
                    TempData["messageTitle"] = "PASSWORD RESET";
                    TempData["message"] = "Password reset was successful";
                    return RedirectToAction(nameof(Index));
                }
                AddErrors(result);
            }
            return View(model);
        }


        public async Task<IActionResult> Unlock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            if (user.LockoutEnd == null)
            {
                TempData["messageType"] = "warning";
                TempData["messageTitle"] = "USER IS NOT LOCKED!";
                TempData["message"] = "No updates where made to the user";
                return RedirectToAction(nameof(Details), new { id });
            }

            UsersListViewModel model = new UsersListViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessFailedCount = user.AccessFailedCount,
                LockoutEnd = user.LockoutEnd
            };

            return View(model);

        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unlock(string id, UsersListViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound();
                }

                if (user.LockoutEnd == null)
                {
                    TempData["messageType"] = "warning";
                    TempData["messageTitle"] = "USER IS NOT LOCKED!";
                    TempData["message"] = "No updates where made to the user";
                    return RedirectToAction(nameof(Details), new { id });
                }

                var result = await _userManager.SetLockoutEndDateAsync(user, null); ;

                if (result.Succeeded)
                {
                    TempData["messageType"] = "success";
                    TempData["messageTitle"] = "USER ACCOUNT UNLOCKED";
                    TempData["message"] = "User account unlocked successfully";
                    return RedirectToAction(nameof(Index));
                }
                AddErrors(result);
            }
            return View(model);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userOrganization = await _context.Users
                .Where(m => m.OrganizationId == user.OrganizationId)
                .CountAsync();

            int relatedCount = 0;
            relatedCount += userOrganization;

            if (relatedCount > 0)
            {
                ViewData["hasRelated"] = true;
            }
            else
            {
                ViewData["hasRelated"] = false;
            }

            ViewData["RelatedCount"] = relatedCount;

            return base.View(model: new UsersListViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Organization = user.OrganizationId == null ? null : _context.Organizations.Single(o => o.OrganizationId == user.OrganizationId).OrganizationName,
                Role = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Name ?? null,
                ModifiedBy = user.ModifiedBy,
                ModifiedDate = user.ModifiedDate,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate
            });

        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            IdentityResult result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["messageType"] = "danger";
                TempData["messageTitle"] = "RECORD DELETED";
                TempData["message"] = "Record deleted successfully";

                return RedirectToAction(nameof(Index));
            }

            return base.View(model: new UsersListViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Organization = user.OrganizationId == null ? null : _context.Organizations.Single(o => o.OrganizationId == user.OrganizationId).OrganizationName,
                Role = _roleManager.Roles.Single(r => r.Name == _userManager.GetRolesAsync(user).Result.Single()).Name ?? null,
                ModifiedBy = user.ModifiedBy,
                ModifiedDate = user.ModifiedDate,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate
            });
        }

        private bool ApplicationUserExists(Guid id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion

    }
}