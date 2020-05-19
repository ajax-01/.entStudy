using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace WebApplication1
{
    public class HomeController : AbpController
    {
        public IActionResult Index()
        {
            return Content("hello world!");
        }
    }
}