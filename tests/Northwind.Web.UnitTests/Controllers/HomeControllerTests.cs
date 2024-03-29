using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Northwind.Web.Controllers;
using Northwind.Web.ViewModels.Home;
using NUnit.Framework;
using System;

namespace Northwind.Web.UnitTests.Controllers
{
    public class HomeControllerTests
    {

        [Test]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController(null);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Error_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController(Mock.Of<ILogger<ErrorViewModel>>())
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            // Act
            var result = controller.Error();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Error_LogsError()
        {
            // Arrange
            var logger = new Mock<ILogger<ErrorViewModel>>();
            var controller = new HomeController(logger.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            controller.Request.HttpContext.Features.Set<IExceptionHandlerFeature>(
                new ExceptionHandlerFeature
                {
                    Error = new Exception()
                });

            // Act
            controller.Error();

            // Assert
            logger.Verify(e =>
                e.Log
                    (LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.IsAny<It.IsAnyType>(),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }
    }
}
