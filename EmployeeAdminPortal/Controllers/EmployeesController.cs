using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        #region private Fields
        private readonly ApplicationDbContext context; 
        #endregion

        #region Ctor
        public EmployeesController(ApplicationDbContext dbContext)
        {
            context = dbContext;
        } 
        #endregion

        #region GetAll
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = context.Employees.ToList();
            if (allEmployees == null || !allEmployees.Any())
            {
                return NotFound("No employees found.");
            }
            return Ok(allEmployees);
        } 
        #endregion

        #region Get

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = context.Employees.Find(id);

            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        } 
        #endregion

        #region Add
        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)

        {
            var employeeEntity = new Employee()
            {
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary

            };
            context.Employees.Add(employeeEntity);
            context.SaveChanges();

            return Ok(employeeEntity);
        } 
        #endregion

        #region Update

        [HttpPut]
        [Route("{id:guid}")]

        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employeeEntity = context.Employees.Find(id);
            if (employeeEntity is null)
            {
                return NotFound();
            }
            employeeEntity.Name = updateEmployeeDto.Name;
            employeeEntity.Email = updateEmployeeDto.Email;
            employeeEntity.Phone = updateEmployeeDto.Phone;
            employeeEntity.Salary = updateEmployeeDto.Salary;
            context.SaveChanges();
            return Ok(employeeEntity);
        } 

        #endregion

        #region Delete

        [HttpDelete]
        [Route("{id:guid}")]

        public IActionResult DeleteEmployee(Guid id)
        {
            var employeeEntity = context.Employees.Find(id);
            if (employeeEntity is null)
            {
                return NotFound();
            }
            context.Employees.Remove(employeeEntity);
            context.SaveChanges();
            return Ok("Employee deleted successfully.");
        } 

        #endregion
    }
}
