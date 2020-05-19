using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Modles
{
    public class TestDbContent : IdentityDbContext
    {
        public TestDbContent(DbContextOptions<TestDbContent> options) : base(options)
        {
        }

        public DbSet<Persons> Persons { get; set; }

        // public DbSet<TestUser> TestUsers { get; set; }
    }

    public class Persons
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Dogs> Dogs { get; set; }
    }

    public class Dogs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DogTypes Type { get; set; }
        public Persons Person { get; set; }
    }

    public enum DogTypes
    {
        Jm,
        Td,
    }

    // public class TestUser : IdentityUser<Guid>
    // {
    // }

    //
    // public class TestRole : IdentityRole<int>
    // {
    // }
}