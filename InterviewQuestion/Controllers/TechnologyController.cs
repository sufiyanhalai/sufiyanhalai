using DataAccessLayer;
using DataAccessLayer.Model;
using InterviewQuestion.DataContext;
using Microsoft.AspNetCore.Http;
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
    public class TechnologyController : ControllerBase
    {
        private readonly ApplicationDbContext _DbContext;
        public TechnologyController(ApplicationDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        [Route("Technologys")]
        public async Task<ActionResult<IEnumerable<TechnologyMaster>>> Technologys()
        {
            var Technology = (from tm in _DbContext.TechnologyMaster
                              select new TechnologyMaster
                              {
                                  TechnologyId = tm.TechnologyId,
                                  Name = tm.Name
                              }).ToListAsync();

            return await Technology;
        }
        [HttpGet]
        [Route("getTechnology")]
        public async Task<ActionResult<TechnologyMaster>> getTechnology(int id)
        {
            var Technology = await _DbContext.TechnologyMaster.FindAsync(id);

            if (Technology == null)
            {
                return NotFound();
            }

            return Technology;
        }

        [Route("AddTechnology")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> AddTechnology(string Name) {
            ReturnResponse ReturnResponse = new ReturnResponse();
            try
            {
                TechnologyMaster TechnologyMaster = new TechnologyMaster();
                TechnologyMaster.Name = Name;
                _DbContext.TechnologyMaster.Add(TechnologyMaster);
                await _DbContext.SaveChangesAsync();

                
                ReturnResponse.Status = true;
                ReturnResponse.Message = "Technology Saved Successfull.";
                return ReturnResponse;
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
                ReturnResponse.Status = false;
                ReturnResponse.Message = "Technology Save Failed.";
                return ReturnResponse;
            }
        }
        [HttpPost]
        [Route("UpdateTechnology")]
       
        public async Task<ActionResult<ReturnResponse>> UpdateTechnology(int id,string Name)
        {
            ReturnResponse ReturnResponse = new ReturnResponse();
            try
            {
                var TechnologyMaster = _DbContext.TechnologyMaster.Where(x=>x.TechnologyId == id).FirstOrDefault();
                TechnologyMaster.Name = Name;
                await _DbContext.SaveChangesAsync();
                ReturnResponse.Status = true;
                ReturnResponse.Message = "Technology Update Successfull.";
                return ReturnResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ReturnResponse.Status = false;
                ReturnResponse.Message = "Technology Update Failed.";
                return ReturnResponse;
            }
        }
    }
}
