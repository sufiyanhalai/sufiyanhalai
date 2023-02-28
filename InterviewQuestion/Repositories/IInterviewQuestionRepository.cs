using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewQuestion.Repositories
{
    public interface IInterviewQuestionRepository
    {
        IEnumerable<InterviewQuestionAnswers> GetInterviewQuestion(int id);

        Task<IEnumerable<TechnologyMaster>> GetTechnology();
    }
}
