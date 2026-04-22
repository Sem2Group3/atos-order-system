using System.Collections.Generic;
using Moq;
using WebApplication_Atos.BLL.BLL;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;
using Xunit;

namespace WebApplication_Atos.Tests
{
    public class EmployeeBLLTests
    {
        private readonly Mock<IEmployeeDBManager> _mockDb;
        private readonly EmployeeBLL _service;

        public EmployeeBLLTests()
        {
            _mockDb = new Mock<IEmployeeDBManager>();
            _service = new EmployeeBLL(_mockDb.Object);
        }

        [Fact]
        public void GetEmployees_ReturnsAllEmployees()
        {
            var employees = new List<Employee>
            {
                new() { WerknemerId = 1 },
                new() { WerknemerId = 2 }
            };
            _mockDb.Setup(db => db.GetEmployees()).Returns(employees);

            var result = _service.GetEmployees();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetEmployeePassword_ReturnsCorrectPassword()
        {
            _mockDb.Setup(db => db.GetEmployeePassword("admin")).Returns("pass123");

            var result = _service.GetEmployeePassword("admin");

            Assert.Equal("pass123", result);
        }
    }
}
