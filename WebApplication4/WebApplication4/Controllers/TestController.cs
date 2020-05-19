using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Dtos;
using WebApplication4.Modles;

namespace WebApplication4.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        public TestDbContent DbContent { get; set; }
        public IMapper Mapper { get; set; }
        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public RoleManager<IdentityRole> RoleManager { get; }

        public TestController(TestDbContent dbContent, IMapper mapper, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            DbContent = dbContent;
            Mapper = mapper;
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        // [Route("TestRoleA")]
        [Authorize(Roles = RoleConstant.Hr)]
        [HttpGet]
        public int A()
        {
            return 1;
        }

        // [Route("TestRoleB")]
        [HttpGet]
        [Authorize(Roles = RoleConstant.Admin)]
        public int B()
        {
            return 1;
        }

        [HttpGet("{id}")]
        public PersonsDto Get(Guid id)
        {
            var result = DbContent.Persons.Find(id);
            if (result != null)
            {
                NotFound();
            }

            return Mapper.Map<Persons, PersonsDto>(result);
        }

        [HttpPost]
        public PersonsDto Post(PersonsDto dto)
        {
            var person = Mapper.Map<PersonsDto, Persons>(dto);
            DbContent.Persons.Add(person);
            DbContent.SaveChanges();

            return Mapper.Map<Persons, PersonsDto>(person);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<int> Test()
        {
            var roles = new[]
            {
                "Admin", "User", "Hr"
            };
            foreach (var role in roles)
            {
                var result = await RoleManager.RoleExistsAsync(role);
                if (!result)
                {
                    var roleResult = await RoleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var user1 = await UserManager.FindByEmailAsync("Admin@gmail.com");
            if (user1 == null)
            {
                user1 = new IdentityUser()
                {
                    UserName = "Admin@gmail.com",
                    Email = "Admin@gmail.com",
                };
                await UserManager.CreateAsync(user1, "Admin@123");
            }

            await UserManager.AddToRoleAsync(user1, "Admin");

            var user2 = await UserManager.FindByEmailAsync("User@gmail.com");
            if (user2 == null)
            {
                user2 = new IdentityUser()
                {
                    UserName = "User@gmail.com",
                    Email = "User@gmail.com",
                };
                await UserManager.CreateAsync(user2, "User@123");
            }

            await UserManager.AddToRoleAsync(user2, "User");

            var user3 = await UserManager.FindByEmailAsync("Hr@gmail.com");
            if (user3 == null)
            {
                user3 = new IdentityUser()
                {
                    UserName = "Hr@gmail.com",
                    Email = "Hr@gmail.com",
                };
                await UserManager.CreateAsync(user3, "Hr@123");
            }

            await UserManager.AddToRoleAsync(user3, "Hr");

            return 1;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="createUserDot"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<string> Register(CreateUserDot createUserDot)
        {
            var user = new IdentityUser()
            {
                Email = createUserDot.Email,
                UserName = createUserDot.Email,
            };
            var result = await UserManager.CreateAsync(user, createUserDot.Password);
            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent: false);
                return "成功";
            }

            return "失败";
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginDto"></param>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<string> Login(LoginDto loginDto)
        {
            var result = await SignInManager.PasswordSignInAsync(loginDto.Email,
                loginDto.Password, loginDto.RememberMe, lockoutOnFailure: true);
            return result.Succeeded ? "成功" : "失败";
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet("signOut")]
        [AllowAnonymous]
        public async Task SignOut()
        {
            await SignInManager.SignOutAsync();
        }
    }

    public class RoleConstant
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Hr = "hr";
    }
}