﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Models
{

    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        // public DbSet<UserResumePage> UserResumePages { get; set; }
        public DbSet<UserInformation> UserInformation { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}