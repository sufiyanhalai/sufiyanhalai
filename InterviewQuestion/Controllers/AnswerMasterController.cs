using DataAccessLayer;
using DataAccessLayer.Model;
using InterviewQuestion.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewQuestion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerMasterController : Controller
    {
        private readonly ApplicationDbContext _DbContext;
        public AnswerMasterController(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        [HttpGet]
        [Route("Answer")]
        public async Task<ActionResult<IEnumerable<Answers>>> Answers()
        {
            var answer = (from tm in _DbContext.Answers
                          join qa in _DbContext.Questions on tm.QuestionsID equals qa.QuestionsID
                          select new Answers
                              {
                                  AnswersId = tm.AnswersId,
                                  AnswersName = tm.AnswersName,
                                  QuestionsID = tm.QuestionsID,
                                  Questions = tm.Questions

                              }).ToListAsync();

            return await answer;
        }
        [HttpGet]
        [Route("getAnswer")]
        public async Task<ActionResult<Answers>> getAnswer(int id)
        {
            var answer = await _DbContext.Answers.FindAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }
        [Route("AddAnswer")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> AddAnswer(string AnswerName,int QuestionID)
        {
            ReturnResponse ReturnResponse = new ReturnResponse();
            try
            {
                Answers ans = new Answers();
                ans.AnswersName = AnswerName;
                ans.QuestionsID = QuestionID;

                _DbContext.Answers.Add(ans);
                await _DbContext.SaveChangesAsync();


                ReturnResponse.Status = true;
                ReturnResponse.Message = "Answer Saved Successfull.";
                return ReturnResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ReturnResponse.Status = false;
                ReturnResponse.Message = "Answer Save Failed.";
                return ReturnResponse;
            }
        }
        [Route("UpdateAnswer")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> UpdateAnswer(int id, string AnswerName)
        {
            ReturnResponse ReturnResponse = new ReturnResponse();
            try
            {
                var AnswerMaster = _DbContext.Answers.Where(x => x.AnswersId == id).FirstOrDefault();
                AnswerMaster.AnswersName = AnswerName;
                await _DbContext.SaveChangesAsync();
                ReturnResponse.Status = true;
                ReturnResponse.Message = "Answer Update Successfull.";
                return ReturnResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ReturnResponse.Status = false;
                ReturnResponse.Message = "Answer Update Failed.";
                return ReturnResponse;
            }
        }
    }
}
