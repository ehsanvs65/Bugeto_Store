using Bugeto_Store.Application.Services.Users.Commands.RegisterUser;
using Bugeto_Store.Application.Services.Users.Commands.UserLogin;
using Bugeto_Store.Application.Services.Users.Queries.GetRoles;
using Bugeto_Store.Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {

        private readonly IGetUsersService _getUserService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IUserLoginService _userLoginService;

        public UsersController(IGetUsersService getUsersService
            ,IGetRolesService getRolesService
            ,IRegisterUserService registerUserService
            ,IUserLoginService userLoginService
            )
        {
            _getUserService = getUsersService;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
            _userLoginService = userLoginService;
        }    
        public IActionResult Index(string searchkey,int page=1)
        {
            return View(_getUserService.Execute(new RequestGetUserDto
            {
                Page = page,
                SearchKey=searchkey,
            }));
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult Create(string Email, string FullName, long RoleId, string Password, string RePassword)
        {
            var result = _registerUserService.Execute(new RequestRegisterUserDto
            {
                Email = Email,
                FullName = FullName,
                roles = new List<RolesInRegisterUserDto>()
                   {
                        new RolesInRegisterUserDto
                        {
                             Id= RoleId
                        }
                   },
                Password = Password,
                RePasword = RePassword,
            });
            return Json(result);
        }
    }
}
