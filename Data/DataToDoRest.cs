using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoRest.Models;

    public class DataToDoRest : DbContext
    {
        public DataToDoRest (DbContextOptions<DataToDoRest> options)
            : base(options)
        {
        }

        public DbSet<ToDoRest.Models.ToDo> ToDo { get; set; }
    }
