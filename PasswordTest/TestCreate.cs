
using Moq;
using PasswordCheck.Controllers;
using PasswordCheck.Models;
using Xunit;


namespace PasswordTest
{
    public class TestCreate
    {
        [Fact]
        public void TestPasswordValid()
        {
            var repository = new Mock<User>();

            var controller = new UsersController();

            object userValid = "{\"Username\": \"UserTeste\",\"Password\": \"senhaTesteValid#\"}";
            
            var result = controller.CreateUser(userValid);

            Assert.
        }
    }
}
