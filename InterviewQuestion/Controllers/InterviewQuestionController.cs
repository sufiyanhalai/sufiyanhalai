using DataAccessLayer;
using DataAccessLayer.Model;
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
    public class InterviewQuestionController : ControllerBase
    {
        private readonly IInterviewQuestionRepository _IInterviewQuestionRepository;


        public InterviewQuestionController(IInterviewQuestionRepository IInterviewQuestionRepository)
        {
            _IInterviewQuestionRepository = IInterviewQuestionRepository;
        }

        [Route("GetInterviewQuestion")]
        [HttpGet]
        public IEnumerable<InterviewQuestionAnswers> GetInterviewQuestion(int id)
        {
            var result = _IInterviewQuestionRepository.GetInterviewQuestion(id);
            return result;
        }   

        [Route("GetTechnologyDetails")]
        [HttpGet]
        public async Task<ActionResult> GetTechnologyDetails()
        {
            try
            {
                var technologydetails = await _IInterviewQuestionRepository.GetTechnology();
                return Ok(technologydetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
