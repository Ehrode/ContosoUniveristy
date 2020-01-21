using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContosoUniveristy.Core.Entities;

namespace ContosoUniveristy.Core.Specifications
{
    public class HiredAfterSpecifications : Specification<Instructor>
    {
        private readonly DateTime _dateDelimiter;

        public HiredAfterSpecifications(DateTime dateDelimiter)
        {
            _dateDelimiter = dateDelimiter;
        }

        public override Expression<Func<Instructor, bool>> ToExpression()
        {
            return instructor => 
                DateTime.Compare(instructor.HireDate, _dateDelimiter) > 0;
        }

    }

    public class LastNameStartWithSpecification : Specification<Instructor>
    {
        private readonly string _startingStringMatch;

        public LastNameStartWithSpecification(string startingStringMatch)
        {
            _startingStringMatch = startingStringMatch;
        }

        public override Expression<Func<Instructor, bool>> ToExpression()
        {
            return person => person.LastName.StartsWith(_startingStringMatch);
        }
    }

    public class FirstMidNameStartWithSpecification : Specification<Instructor>
    {
        private readonly string _startingStringMatch;

        public FirstMidNameStartWithSpecification(string startingStringMatch)
        {
            _startingStringMatch = startingStringMatch;
        }

        public override Expression<Func<Instructor, bool>> ToExpression()
        {
            return person => person.FirstMidName.StartsWith(_startingStringMatch);
        }
    }
}
