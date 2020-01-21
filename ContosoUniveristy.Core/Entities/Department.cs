using System;
using System.Collections.Generic;

namespace ContosoUniveristy.Core.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }

        public int? InstructorId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Instructor Administrator { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}