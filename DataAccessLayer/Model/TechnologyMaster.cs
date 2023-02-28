using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.Model
{
    public class TechnologyMaster
    {
        [Key]
        public int TechnologyId { get; set; }
        public string Name { get; set; }
    }
}
