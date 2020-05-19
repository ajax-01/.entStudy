using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Dtos;
using WebApplication4.Modles;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class Test2Controller : Controller
    {
        public TestDbContent DbContent { get; }
        public IMapper Mapper { get; }

        public Test2Controller(TestDbContent dbContent, IMapper mapper)
        {
            DbContent = dbContent;
            Mapper = mapper;
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
    }
}