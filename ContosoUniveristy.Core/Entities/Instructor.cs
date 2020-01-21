using System;
using System.Collections.Generic;

namespace ContosoUniveristy.Core.Entities
{
    public class Instructor : Person
    {
        public DateTime HireDate { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        public virtual OfficeAssignment OfficeAssignment { get; set; }
    }
}