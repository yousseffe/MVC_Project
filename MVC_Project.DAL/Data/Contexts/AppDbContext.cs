﻿using Microsoft.EntityFrameworkCore;
using MVC_3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_3.DAL.Data.Contexts
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{

		//}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
		}

		public DbSet<Department> Departments { get; set; }

	}
}
