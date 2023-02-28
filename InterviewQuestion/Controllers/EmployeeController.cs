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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Route("GetEmployees")]
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                var employeedetails = await _employeeRepository.GetEmployees();
                return Ok(employeedetails);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("GetEmployee")]
        [HttpGet]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployee(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [Route("CreateEmployee")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> CreateEmployee(Employee employee)
        {
            try
            {
                ReturnResponse returnResponse = new ReturnResponse();
                if (employee == null)
                {
                    return BadRequest();
                }

                var emp = await _employeeRepository.AddEmployee(employee);
                returnResponse.Status = true;
                returnResponse.Message = "Employee Add Successfully";
                return returnResponse;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("UpdateEmployee")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                ReturnResponse returnResponse = new ReturnResponse();

                if (id != employee.Id)
                {
                    return BadRequest();
                }
                var update = await _employeeRepository.GetEmployee(id);
                if (update == null)
                {
                    return NotFound();
                }

                var Updateemp = await _employeeRepository.UpdateEmployee(employee);
                returnResponse.Status = true;
                returnResponse.Message = "Employee Update SuccessFully";
                return returnResponse;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [Route("DeleteEmployee")]
        [HttpPost]
        public async Task<ActionResult<ReturnResponse>> DeleteEmployee(int Id)
        {
            try
            {

                ReturnResponse returnResponse = new ReturnResponse();

                var employee = await _employeeRepository.GetEmployee(Id);

                if (employee == null)
                {
                    return NotFound();
                }

                var deleteemployee = await _employeeRepository.DeleteEmployee(Id);
                returnResponse.Status = true;
                returnResponse.Message = "Employee Delete SuccessFully";
                return returnResponse;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


        [Route("SearchEmployee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> SearchEmployee(string name)
        {
            try
            {
                var result = await _employeeRepository.SearchEmployee(name);

                if (name.Any())
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
