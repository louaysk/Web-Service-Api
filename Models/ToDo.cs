using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoRest.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public DateTime URD { get; set; }
        //public ToDo()
        //{
        //    URD = DateTime.Now;
        //}
        public DateTime Due_Date { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsVisible { get; set; }
    }

}
