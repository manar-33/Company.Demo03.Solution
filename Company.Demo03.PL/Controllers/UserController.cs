using Company.Demo03.DAL.Models;
using Company.Demo03.PL.Helper;
using Company.Demo03.PL.ViewModels;
using Company.Demo03.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Demo03.PL.Controllers
{
    [Authorize (Roles ="Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IActionResult> Index(string InputSearch)
		{
			var users = Enumerable.Empty<UserViewModel>();
			if (string.IsNullOrEmpty(InputSearch))
			{
				users = await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					FirstName = U.FirstName,
					LastName = U.LastName,
					Email = U.Email,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync();
			}
			else
			{
				users= await _userManager.Users.Where(U => U.Email
								  .ToLower()
								  .Contains(InputSearch.ToLower()))
								  .Select(U => new UserViewModel()
								  {
									  Id = U.Id,
									  FirstName = U.FirstName,
									  LastName = U.LastName,
									  Email = U.Email,
									  Roles = _userManager.GetRolesAsync(U).Result
								  }).ToListAsync();
			}
			return View(users);

		}
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null)
				return BadRequest();//400

		var userFromDb	=await _userManager.FindByIdAsync(id);

			if (userFromDb is null)
                return NotFound();//404

			var user = new UserViewModel()
			{
				Id= userFromDb.Id,
				FirstName= userFromDb.FirstName,
				LastName= userFromDb.LastName,
				Email = userFromDb.Email,
                Roles = _userManager.GetRolesAsync(userFromDb).Result

            };

            return View(viewName, user);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
          return await Details(id,"Edit");
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {

            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var userFromDb = await _userManager.FindByIdAsync(id);

                    if (userFromDb is null)
                        return NotFound();//404

                    userFromDb.FirstName = model.FirstName;
                    userFromDb.LastName = model.LastName;
                    userFromDb.Email = model.Email;

                  await  _userManager.UpdateAsync(userFromDb);
                        return RedirectToAction(nameof(Index));

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);

        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        //{
        //    try
        //    {
        //        if (id != model.Id) return BadRequest(); // Check for ID mismatch

        //        if (ModelState.IsValid)
        //        {
        //            var userFromDb = await _userManager.FindByIdAsync(id);
        //            if (userFromDb is null) return NotFound(); // Handle missing user

        //            userFromDb.FirstName = model.FirstName;
        //            userFromDb.LastName = model.LastName;
        //            userFromDb.Email = model.Email;

        //            var result = await _userManager.UpdateAsync(userFromDb);
        //            if (!result.Succeeded)
        //            {
        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError(string.Empty, error.Description);
        //                }
        //                return View(model);
        //            }
        //            return RedirectToAction(nameof(Index));
        //        }
        //        else
        //        {
        //            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        //            {
        //                Console.WriteLine(error.ErrorMessage);
        //            }
                
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.Message); // Add generic error
        //    }

        //    return View(model);
       // }
        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            
        
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {

            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByIdAsync(id);

                if (userFromDb is null)
                    return NotFound();//404

                await _userManager.DeleteAsync(userFromDb);

                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }
            

}
    }

