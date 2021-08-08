using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PasswordCheck.Controllers;

namespace TestPassword.Tests
{
    [TestFixture]
    class TestCreat
    {
        [Test]
        public void Test_Valid()
        {
            var controller = new UsersController();

            string userValid = "{\"Username\": \"UserTesteNew\",\"Password\": \"TesteValid#senh@\"}";

            var result = (ObjectResult)controller.CreateUser(userValid).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status200OK, status);
        }

        [Test]
        public void Test_AlreadyExists()
        {
            var controller = new UsersController();

            string userAlreadyExists = "{\"Username\": \"UserTeste\",\"Password\": \"senhaTesteValid#\"}";

            var result = (ObjectResult)controller.CreateUser(userAlreadyExists).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status422UnprocessableEntity, status);
        }

        [Test]
        public void Test_PasswordNull()
        {
            var controller = new UsersController();

            string userPasswordNull = "{\"Username\": \"UserTesteNew\"}";

            var result = (ObjectResult)controller.CreateUser(userPasswordNull).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status400BadRequest, status);
        }

        [Test]
        public void Test_UserNameNull()
        {
            var controller = new UsersController();

            string userNameNull = "{\"Password\": \"senhaTesteValid#\"}";

            var result = (ObjectResult)controller.CreateUser(userNameNull).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status400BadRequest, status);
        }

        [Test]
        public void Test_WithoutBody()
        {
            var controller = new UsersController();

            string userWithoutBody = null;

            var result = (ObjectResult)controller.CreateUser(userWithoutBody).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status400BadRequest, status);
        }
    }
}
