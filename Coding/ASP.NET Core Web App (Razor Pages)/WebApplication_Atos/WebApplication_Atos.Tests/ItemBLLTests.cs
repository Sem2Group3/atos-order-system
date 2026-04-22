using System.Collections.Generic;
using Moq;
using WebApplication_Atos.BLL.BLL;
using WebApplication_Atos.Interfaces;
using WebApplication_Atos.Models;
using Xunit;

namespace WebApplication_Atos.Tests
{
    public class ItemBLLTests
    {
        private readonly Mock<IItemDBManager> _mockDb;
        private readonly ItemBLL _service;

        public ItemBLLTests()
        {
            _mockDb = new Mock<IItemDBManager>();
            _service = new ItemBLL(_mockDb.Object);
        }

        [Fact]
        public void GetItems_ReturnsListOfItems()
        {
            var items = new List<Item> { new(), new() };
            _mockDb.Setup(db => db.GetItems()).Returns(items);

            var result = _service.GetItems();

            Assert.Equal(2, result.Count);
        }
    }
}
