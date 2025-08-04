using Microsoft.AspNetCore.Mvc;
using TShip.Data;

namespace TShip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public AuthController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var Accounts = dbContext.Accounts.ToList();
            return Ok(Accounts);
        }

        //[HttpPost]
        //public IActionResult AddEmployee(AddEmployeeDto addEmployeeDto)
        //{
        //    var employeeEntity = new Employee()
        //    {
        //        Name = addEmployeeDto.Name,
        //        Email = addEmployeeDto.Email,
        //        Phone = addEmployeeDto.Phone,
        //        Salary = addEmployeeDto.Salary,

        //    };
        //    dbContext.Employees.Add(employeeEntity);
        //    dbContext.SaveChanges();
        //    return Ok(employeeEntity);

        //}

        //[HttpGet]
        //[Route("{id:guid}")]
        //public IActionResult GetEmployeeById(Guid id)
        //{
        //    var employee = dbContext.Employees.Find(id);
        //    if (employee is null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(employee);
        //}

        ////[HttpPut]
        ////[Route("{id:guid}")]
        //[HttpPut("{id:guid}")]
        //public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDto dto)
        //{
        //    var employee = dbContext.Employees.Find(id);
        //    if (employee is null)
        //        return NotFound();

        //    // Chỉ cập nhật những field không null
        //    if (!string.IsNullOrWhiteSpace(dto.Name))
        //        employee.Name = dto.Name;

        //    if (!string.IsNullOrWhiteSpace(dto.Email))
        //        employee.Email = dto.Email;

        //    if (!string.IsNullOrWhiteSpace(dto.Phone))
        //        employee.Phone = dto.Phone;

        //    if (dto.Salary.HasValue)
        //        employee.Salary = dto.Salary.Value;

        //    dbContext.SaveChanges();
        //    return Ok(employee);
        //}

        //[HttpDelete]
        //public IActionResult DeleteEmployee(Guid id)
        //{
        //    var employee = dbContext.Employees.Find(id);
        //    if (employee is null)
        //    {
        //        return NotFound();
        //    }
        //    dbContext.Employees.Remove(employee);
        //    dbContext.SaveChanges();

        //    return Ok();
        //}

        //[HttpPost("{id:guid}")]
        //public IActionResult GetSalaryById(Guid id)
        //{
        //    var result = dbContext.Employees
        //.FromSqlRaw("SELECT * FROM Employees WHERE Id = {0}", id)
        //.FirstOrDefault();

        //    if (result == null)
        //        return NotFound();

        //    return Ok(result.Salary);
        //}

        //[HttpGet("GetSalary")]
        ////public IActionResult<decimal> GetSalaryOnlyById([FromQuery] Guid id)
        //public ActionResult<decimal> GetSalaryOnlyById([FromQuery] Guid id)
        //{
        //    var salary = dbContext.Employees
        //        .Where(e => e.Id == id)
        //        .Select(e => e.Salary)
        //        .FirstOrDefault();
        //    if (salary == 0)
        //        return NotFound();
        //    return Ok(salary);
        //    //var result = dbContext.Employees
        //    //    .FromSqlRaw("SELECT * FROM Employees WHERE Id = {0}", id)
        //    //    .FirstOrDefault();

        //    //if (result == null)
        //    //    return NotFound();

        //    //return Ok(result.Salary);
        //}

        //[HttpPost("GetSalary/{id:guid}")]
        //public ActionResult<EmployeeSalaryDto> GetNameAndSalaryById(/*[FromQuery] Guid id*/ Guid id)
        //{
        //    var employee = dbContext.Employees.Where(e => e.Id == id).Select(e => new EmployeeSalaryDto
        //    {
        //        Name = e.Name,
        //        Salary = e.Salary,
        //    }).FirstOrDefault();
        //    if (employee == null)
        //        return NotFound();

        //    return Ok(employee);
        //}

        ////[HttpPost("EmployeeObj/{id:guid}")]
        ////public IActionResult GetEmployeeByIdAndName([FromQuery] String name, Guid id)
        ////{
        ////    if (string.IsNullOrWhiteSpace(name))
        ////    {
        ////        return BadRequest(new { message = "Thông tin gửi lên không hợp lệ." });
        ////    }
        ////    var employee = dbContext.Employees.Where(e => e.Id == id && e.Name == name).Select(e => new EmployeeSalaryDto
        ////    {
        ////        Name = e.Name,
        ////        Salary = e.Salary,
        ////    }).FirstOrDefault();
        ////    if (employee == null)
        ////        return Forbid();

        ////    return Ok(employee);
        ////}
        //[HttpPost("EmployeeObj/{id:guid}")]
        //public IActionResult GetEmployeeByIdAndName([FromBody] EmployeeQueryDto body, [FromRoute] Guid id)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);


        //    var employee = dbContext.Employees
        //        .Where(e => e.Id == id && e.Name == body.Name && e.Email == body.Email)
        //        .Select(e => new EmployeeSalaryDto
        //        {
        //            Name = e.Name,
        //            Salary = e.Salary,
        //        })
        //        .FirstOrDefault();
        //    //var employee = dbContext.Employees
        //    //    .Where(e => e.Id == id && e.Name == body.Name && (string.IsNullOrEmpty(body.Email) || e.Email == body.Email))
        //    //    .Select(e => new EmployeeSalaryDto
        //    //    {
        //    //        Name = e.Name,
        //    //        Salary = e.Salary,
        //    //    })
        //    //    .FirstOrDefault();

        //    if (employee == null)
        //        return NotFound();

        //    return Ok(employee);
        //}
    }
}
