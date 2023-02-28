using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Model
{
    public class Answers
    {
        [Key]
        public int AnswersId { get; set; }
        public string AnswersName { get; set; }

        [Display(Name = "Questions")]
        public virtual int QuestionsID { get; set; }
        [ForeignKey("QuestionsID")]
        public virtual Questions Questions { get; set; }

    }
}
