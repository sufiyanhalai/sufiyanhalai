using DataAccessLayer.Model;
using InterviewQuestion.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewQuestion.Repositories
{
    public class InterviewQuestionRepository : IInterviewQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public InterviewQuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<InterviewQuestionAnswers> GetInterviewQuestion(int id)
        {
            List <Answers> answers =  _context.Answers.ToList();
            List <Questions> questions =  _context.Questions.ToList();
            List <TechnologyMaster> technologyMaster = _context.TechnologyMaster.ToList();

            var InterviewQuestion = (from an in answers
                                     join qa in questions on an.QuestionsID equals qa.QuestionsID
                                     join tm in technologyMaster on qa.TechnologyId equals tm.TechnologyId
                                     where tm.TechnologyId == id
                                     select new InterviewQuestionAnswers()
                                     {
                                         QuestionsName = qa.QuestionsName,
                                         AnswersName = an.AnswersName,
                                         QuestionOrder = qa.QuestionsOrder

                                     }
            ).OrderByDescending(x=>x.QuestionOrder).ToList();

            return InterviewQuestion;
        }

        public async Task<IEnumerable<TechnologyMaster>> GetTechnology()
        {
            return await _context.TechnologyMaster.ToListAsync();
        }

    }
}
