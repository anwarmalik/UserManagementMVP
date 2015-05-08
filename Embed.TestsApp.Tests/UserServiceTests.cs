using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Embed.TestApp.Services;
using System.Linq;
using Embed.TestApp.Model;
using System.Data.Entity;
using Embed.TestApp.Model.Entities;
using System.Collections.Generic;

namespace Embed.TestsApp.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private IUserService _service;
        Mock<IEmbedContext> _mockContext;
        Mock<DbSet<User>> _mockSet;
        IQueryable<User> listUsers;

        [TestInitialize]
        public void Initialize()
        {
            listUsers = new List<User>() {
          new User{ Id = 1, UserName = "User 1" },
          new User{ Id = 2, UserName = "User 2" },
          new User{ Id = 3, UserName = "User 3" }
         }.AsQueryable();

            _mockSet = new Mock<DbSet<User>>();
            _mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(listUsers.Provider);
            _mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(listUsers.Expression);
            _mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(listUsers.ElementType);
            _mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(listUsers.GetEnumerator());

            _mockContext = new Mock<IEmbedContext>();
            _mockContext.Setup(c => c.Set<User>()).Returns(_mockSet.Object);
            _mockContext.Setup(c => c.Users).Returns(_mockSet.Object);

            _service = new UserService(_mockContext.Object);

        }

        [TestMethod]
        public void Users_Get_All()
        {


            List<User> results = _service.QuerySet.ToList() as List<User>;

            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void Can_Add_User()
        {
            int Id = 1;
            User user = new User() { UserName = "User 4" };

            _mockSet.Setup(m => m.Add(user)).Returns((User e) =>
            {
                e.Id = Id;
                return e;
            });


            _service.Add(user);

            //Assert
            _mockContext.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
        }
    }
}
