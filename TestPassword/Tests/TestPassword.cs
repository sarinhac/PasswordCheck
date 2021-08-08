
using NUnit.Framework;
using PasswordCheck.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestPassword.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test_ValidPassword()
        {
            var controller = new UsersController();

            string userValidPassword = "{\"Username\": \"UserTesteNew\",\"Password\": \"senhaTesteValid#\"}";

            var result = (ObjectResult) controller.CreateUser(userValidPassword).Result;

            int status = (int) result.StatusCode;

            Assert.AreEqual(StatusCodes.Status200OK, status);
        }

        [Test]
        public void Test_InvalidPassword_SmallerSize()
        {
            var controller = new UsersController();

            string userSmallerSize = "{\"Username\": \"UserTesteSmallerSize\",\"Password\": \"senhaMenor#\"}";

            var result = (ObjectResult)controller.CreateUser(userSmallerSize).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status400BadRequest, status);
        }

        [Test]
        public void Test_InvalidPassword_WithOutUpperCase()
        {
            var controller = new UsersController();

            string userUpperCase = "{\"Username\": \"UserTesteUpperCase\",\"Password\": \"senhasemuppercase@\"}";

            var result = (ObjectResult)controller.CreateUser(userUpperCase).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status400BadRequest, status);
        }

        [Test]
        public void Test_InvalidPassword_WithOutLowCase()
        {
            var controller = new UsersController();

            string userLowCase = "{\"Username\": \"UserTesteLowCase\",\"Password\": \"SENHASEMLOWCASE#\"}";

            var result = (ObjectResult)controller.CreateUser(userLowCase).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status400BadRequest, status);
        }

        [Test]
        public void Test_InvalidPassword_WithOutSimbols()
        {
            var controller = new UsersController();

            string userSimbols = "{\"Username\": \"UserTesteSimbols\",\"Password\": \"senhaTesteSemSimbolo\"}";

            var result = (ObjectResult)controller.CreateUser(userSimbols).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status400BadRequest, status);
        }

        [Test]
        public void Test_InvalidPassword_RepetCaracter()
        {
            var controller = new UsersController();

            string userRepet = "{\"Username\": \"UserTesteRepeat\",\"Password\": \"senhaRepeticao#1111\"}";

            var result = (ObjectResult)controller.CreateUser(userRepet).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status400BadRequest, status);
        }

        [Test]
        public void Test_InvalidPassword_AcceptNumber()
        {
            //Testa se aceita numeros -> resultado deve aceitar
            var controller = new UsersController();

            string userNumber = "{\"Username\": \"UserTesteNumero\",\"Password\": \"#senhaNumero32#\"}";

            var result = (ObjectResult)controller.CreateUser(userNumber).Result;

            int status = (int)result.StatusCode;

            Assert.AreEqual(StatusCodes.Status200OK, status);
        }

    }
}