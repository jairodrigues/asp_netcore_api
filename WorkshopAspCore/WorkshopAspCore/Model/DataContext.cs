﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopAspCore.Model
{
    public class DataContext : DbContext
    {     

        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
