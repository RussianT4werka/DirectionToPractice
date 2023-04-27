using DirectionToPractice.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectionToPractice.Interfaces
{
    public interface Students
    {
        public List<Student> GetStudent()
        {
            return new List<Student>();
        }
    }
}
