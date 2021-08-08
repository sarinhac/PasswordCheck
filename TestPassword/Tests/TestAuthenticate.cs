using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using PasswordCheck.Controllers;
using PasswordCheck.Models;
using System.Text.Json;

namespace TestPassword.Tests
{
    [TestFixture]
    class TestAuthenticate
    {
        [Test]
        public void Test_Valid()
        {
            var controller = new UsersController();

            string userValid = "{\"Username\": \"UserTeste\",\"Password\": \"senhaTesteValid@\"}";

            var result = (UserModelApi) controller.Authenticate(userValid).Value;

            Assert.IsTrue(result.Authenticated);
            
        }

        [Test]
        public void Test_IncorrectPassword()
        {
            var controller = new UsersController();

            string userIncorrect = "{\"Username\": \"UserTeste\",\"Password\": \"senhaTesteValid#\"}";

            var result = (ObjectResult)controller.Authenticate(userIncorrect).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status401Unauthorized, status);
        }

        [Test]
        public void Test_NonExistentUser()
        {
            var controller = new UsersController();

            string userNonExistentUser = "{\"Username\": \"UserTeste1\",\"Password\": \"senhaTesteValid#\"}";

            var result = (ObjectResult)controller.Authenticate(userNonExistentUser).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status401Unauthorized, status);
        }
    }
}
