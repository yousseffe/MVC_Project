﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_3.DAL.Data.Configurations
{
	internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			// Fluent Apis
			builder.Property(D => D.Id).UseIdentityColumn(10 , 10);
			builder.HasMany(x => x.Employees)
				   .WithOne(E=>E.Department)
				   .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
