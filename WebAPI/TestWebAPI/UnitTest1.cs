
//using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WebAPI;
using WebAPI.Controllers;
using WebAPI.DTO;
using WebAPI.Repository;
using WebAPI.Service;

namespace TestWebAPI
{
    [TestFixture]
    public class Tests
    {
        string name = string.Empty;

        // Using MOQ
        //private Mock<ITodoItemRepository> externalServiceClientMock;
        //public ITodoItemService myService;

        private DependencyResolverHelper _serviceProvider;


        public Tests()
        {

            var webHost = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder()
                    .UseStartup<Startup>()
                    .Build();
            _serviceProvider = new DependencyResolverHelper(webHost);

        }

        [SetUp]
        public void Setup()
        {
            name = "3rd";

            // TodoItemRepository = new TodoItemService(TodoItemRepository);

            // Using MOQ
            //externalServiceClientMock = new Mock<ITodoItemRepository>();
            //myService = new TodoItemService(externalServiceClientMock.Object);

        }

        [Test]
        public void GetItemTest()
        {

            //Using MOQ

            // mocking and test
            //var expected = new TodoItemDTO { Id = 11, TodoName = "ee", ProgressPercentage = 44 };
            //externalServiceClientMock.Setup(esc => esc.GetItem(name)).Returns(expected);

            // When
            //var actual = myService.GetItem(name);

            // Then
            //externalServiceClientMock.Verify(esc => esc.GetItem(name));
            //Assert.AreEqual(expected, actual);

            // ENd MOQ

            var SUTRepo = _serviceProvider.GetService<ITodoItemRepository>();
            TodoItemDTO todoItemDTO = SUTRepo.GetItem(name);

            if (todoItemDTO != null && !string.IsNullOrEmpty(todoItemDTO.TodoName))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }
    }
}