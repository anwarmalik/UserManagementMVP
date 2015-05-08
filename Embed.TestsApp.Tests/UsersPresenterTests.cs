using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Embed.TestApp.Services;
using System.Linq;
using Embed.TestApp.Model;
using System.Data.Entity;
using Embed.TestApp.Model.Entities;
using System.Collections.Generic;
using Embed.TestApp;

namespace Embed.TestsApp.Tests
{
    [TestClass]
    public class MainPresenterTests
    {

        IUserService _service;
        IQueryable<User> listUsers;
        Mock<IUsersView> view;
        Mock<IEmbedContext> _mockContext;
        Mock<DbSet<User>> _mockSet;
        Mock<IUnitOfWork> unitOfWork;
        UsersPresenter usersPresentor;
        
        [TestInitialize]
        public void Initialize()
        {
            listUsers = new List<User>() {
          new User{ Id = 1, UserName = "User1" },
          new User{ Id = 2, UserName = "User2" },
          new User{ Id = 3, UserName = "User3" }
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

            view = new Mock<IUsersView>();
            unitOfWork = new Mock<IUnitOfWork>();

        }

        [TestMethod]
        public void Load_Users_Test()
        {

            var usersPresentor = new UsersPresenter(view.Object, _service, unitOfWork.Object);

            view.Verify(m => m.LoadUsers(It.IsAny<IEnumerable<User>>()));
        }

        [TestMethod]
        public void New_User_Test()
        {

            usersPresentor = new UsersPresenter(view.Object, _service, unitOfWork.Object);
            view.Raise(m => m.AddUser += null, null, null);
            view.Verify(m => m.LoadUser(It.IsAny<User>()));
        }

    }
}
