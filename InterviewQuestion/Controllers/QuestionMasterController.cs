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
    public class QuestionMasterController : Controller
    {
        private readonly ApplicationDbContext _DbContext;
        public QuestionMasterController(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        [HttpGet]
        [Route("Question")]
        public async Task<ActionResult<IEnumerable<Questions>>> Questions()
        {
            var Technology = (from qm in _DbContext.Questions
                              join tm in _DbContext.TechnologyMaster on qm.TechnologyId equals tm.TechnologyId
                              select new Questions
                              {
                                  QuestionsID = qm.QuestionsID,
                                  QuestionsName = qm.QuestionsName,
                                  QuestionsOrder = qm.QuestionsOrder,
                                  TechnologyId = qm.TechnologyId,
                                  TechnologyName = tm.Name

                              }).ToListAsync();

            return await Technology;
        }

        [Route("getQuestion")]
        public async Task<ActionResult<Questions>> getQuestion(int id)
        {
            var Question = (from qm in _DbContext.Questions
                            join tm in _DbContext.TechnologyMaster on qm.TechnologyId equals tm.TechnologyId
                            select new Questions
                            {
                                QuestionsID = qm.QuestionsID,
                                QuestionsName = qm.QuestionsName,
                                QuestionsOrder = qm.QuestionsOrder,
                                TechnologyId = qm.TechnologyId,
                                TechnologyName = tm.Name

                            }).Where(x => x.QuestionsID == id).FirstOrDefaultAsync();
            if (Question == null)
            {
                return NotFound();
            }

            return await Question;
        }

        [Route("AddQuestions")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> AddQuestions(string QuestionName, int TechnologyID)
        {
            int QuestionsOrder = 0;
            ReturnResponse ReturnResponse = new ReturnResponse();
            try
            {
                var allQuestion = _DbContext.Questions.OrderByDescending(x => x.QuestionsOrder).FirstOrDefault();
                if (allQuestion != null)
                {
                    QuestionsOrder = allQuestion.QuestionsOrder + 1;
                }
                else
                {
                    QuestionsOrder = 1;
                }
                Questions que = new Questions();
                que.QuestionsName = QuestionName;
                que.QuestionsOrder = QuestionsOrder;
                que.TechnologyId = TechnologyID;
                _DbContext.Questions.Add(que);
                await _DbContext.SaveChangesAsync();
                ReturnResponse.Status = true;
                ReturnResponse.Message = "Question Saved Successfull.";
                return ReturnResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ReturnResponse.Status = false;
                ReturnResponse.Message = "Question Save Failed.";
                return ReturnResponse;
            }
        }
        [Route("UpdateQuestions")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> UpdateQuestions(int id, string QuestionName, int techID)
        {
            ReturnResponse ReturnResponse = new ReturnResponse();
            try
            {
                var QuestionMaster = _DbContext.Questions.Where(x => x.QuestionsID == id).FirstOrDefault();
                QuestionMaster.QuestionsName = QuestionName;
                QuestionMaster.TechnologyId = techID;
                await _DbContext.SaveChangesAsync();
                ReturnResponse.Status = true;
                ReturnResponse.Message = "Question Update Successfull.";
                return ReturnResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ReturnResponse.Status = false;
                ReturnResponse.Message = "Question Update Failed.";
                return ReturnResponse;
            }
        }
    }
}