using System;
using System.Collections.Generic;

namespace ContosoUniveristy.Core.Entities
{
    public class Student : Person
    {
        public DateTime EnrollmentDate { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}