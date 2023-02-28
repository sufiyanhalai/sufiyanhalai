using DataAccessLayer;
using InterviewQuestion.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewQuestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepository _ILoginRepository;

        public LoginController(ILoginRepository ILoginRepository)
        {
            _ILoginRepository = ILoginRepository;
        }

        [Route("UserLogin")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> UserLogin(Login login)
        {
            ReturnResponse returnResponse = new ReturnResponse();

            var checklogin = await _ILoginRepository.Login(login);

            if (checklogin == null)
            {
                returnResponse.Message = "Please enter valid usrename and password";
            }
            else
            {
                returnResponse.Status = true;
                returnResponse.Message = "Login Successfully";
            }
            return returnResponse;
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> SignUp(User user)
        {
            try
            {
                ReturnResponse returnResponse = new ReturnResponse();

                if (user == null)
                {
                    return BadRequest();
                }
                else
                {
                    var signup = await _ILoginRepository.Userregistration(user);
                    returnResponse.Status = true;
                    returnResponse.Message = "User Signup Successfully";

                    return returnResponse;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
