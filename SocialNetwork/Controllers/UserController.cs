using Microsoft.AspNetCore.Mvc;

namespace SocialNetwork.Controllers;

public class UserController : Controller
{
	[Route("User/Profile/{id}")]
	[HttpPost]
	public IActionResult Profile(string id)
	{
		var user = await _userService.GetByIdAsync(id); // получаем User по id
		if (user == null)
			return NotFound();

		var model = new UserViewModel(user);
		return View("MyPage", model);
	}
}