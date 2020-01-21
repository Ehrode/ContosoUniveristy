using NUnit.Framework;
using ContosoUniveristy.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoUniveristy.Core.Entities;
using ContosoUniveristy.Core.Interfaces;
using ContosoUniveristy.CoreTests.TestClasses;
using ContosoUniversity.Infrastructure.Data;
using ContosoUniversity.Infrastructure.Data.Repositories;
using Moq;

namespace ContosoUniveristy.Core.Specifications.Tests
{
    [TestFixture]
    public class OrSpecificationTests
    {
        [Test]
        public void ToExpressionTest()
        {
            var data = new List<Instructor>
            {
                new Instructor { Id = 1, LastName = "ABCDE", FirstMidName = "ZER", HireDate = DateTime.MinValue},
                new Instructor { Id = 2, LastName = "ABFGE", FirstMidName = "wsd", HireDate = DateTime.MaxValue},
                new Instructor { Id = 3, LastName = "TREZ", FirstMidName = "tre", HireDate = DateTime.MinValue}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Instructor>>();
            mockSet.As<IDbAsyncEnumerable<Instructor>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Instructor>(data.GetEnumerator()));

            mockSet.As<IQueryable<Instructor>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Instructor>(data.Provider));

            mockSet.As<IQueryable<Instructor>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Instructor>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Instructor>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<SchoolContext>();
            mockContext.Setup(c => c.Set<Instructor>()).Returns(mockSet.Object);

            var lastNameSpec = new LastNameStartWithSpecification("AB");
            var firstNameSpec = new FirstMidNameStartWithSpecification("w");
            var hiredSpec = new HiredAfterSpecifications(DateTime.Now);

            IGenericRepository<Instructor> repo = new GenericRepository<Instructor>(mockContext.Object);

            var instructors = repo.Find(hiredSpec.And(firstNameSpec.And(lastNameSpec))).Result.ToList();

            Assert.AreEqual(instructors.Count, 1);
            Assert.AreEqual(instructors.First().Id, 2);
        }
    }
}