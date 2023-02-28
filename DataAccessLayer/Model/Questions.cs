using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLayer.Model
{
    public class Questions
    {
        [Key]
        public int QuestionsID { get; set; }
        public string QuestionsName { get; set; }
        public int QuestionsOrder { get; set; }

        [Display(Name = "TechnologyMaster")]
        public virtual int TechnologyId { get; set; }

        [ForeignKey("TechnologyId")]
        public virtual TechnologyMaster TechnologyMaster { get; set; }

        [NotMapped]
        public string TechnologyName { get; set; }

    }
}