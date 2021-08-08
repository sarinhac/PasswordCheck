using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PasswordCheck.Models;
using PasswordCheck.Models.Errors;
using PasswordCheck.Services;

namespace PasswordCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        List<User> users = new List<User>
        {
            new User
            {
                Username = "UserTeste",
                Password = "$2a$11$Zk3V1FQnnu947HJehfOCyuSJE3ScqiZktd1O4ktj02QsT3CONwMVO" //senhaTesteValid@
            }
        };

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public ActionResult<HttpResponse> CreateUser([FromBody] object username)
        {
            User user = new User();
            try
            {
                user = JsonSerializer.Deserialize<User>(username.ToString());
            }
            catch
            {
                return BadRequest(new { message = "Body is not in the correct format" });
            }

            if (!String.IsNullOrEmpty(user.Username) && !String.IsNullOrEmpty(user.Password))
            {
                User userExists = users.Where(x => x.Username.Equals(user.Username)).FirstOrDefault();

                if (userExists == null)
                {
                    if (PasswordService.isValid(user.Password))
                    {
                        user.Password = PasswordService.CreatePassword(user.Password);

                        users.Add(user);

                        return Ok(new { message = "New User Created" });

                    }
                    else
                    {
                        return BadRequest(new { message = "This Password isn't valid" });
                    }
                }

                return UnprocessableEntity(new { message = "User already exists" });

            }

            return BadRequest(new { message = "Body is not in the correct format" });

        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult<dynamic> Authenticate([FromBody] object username)
        {
            User user = new User();
            try
            {
                user = JsonSerializer.Deserialize<User>(username.ToString());
            }
            catch
            {
                return BadRequest(new { message = "Body is not in the correct format" });
            }

            if (!String.IsNullOrEmpty(user.Username) && !String.IsNullOrEmpty(user.Password))
            {
                if (PasswordService.isValid(user.Password))
                {
                    User userExists = users.Where(x => x.Username.Equals(user.Username)).FirstOrDefault();

                    if (userExists != null && PasswordService.ValidatePassword(user.Password, userExists.Password))
                    {
                        var token = TokenService.GenerateToken(user);

                        return new UserModelApi
                        {
                            User = user.Username,
                            Token = token,
                            TokenExpirationDateTime = DateTime.Now.AddMinutes(5),
                            Authenticated = true
                        };
                    }
                    else
                    {
                        return Unauthorized(new { message = "username or password incorrect" });
                    }

                }
                else
                {
                    return BadRequest(new { message = "This Password isn't valid" });
                }

            }

            return BadRequest(new { message = "Body is not in the correct format" });

        }

    }
}

