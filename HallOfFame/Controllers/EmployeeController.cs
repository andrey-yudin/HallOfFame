namespace HallOfFame.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using HallOfFame.DTO;
    using HallOfFame.Services;

    [Produces("application/json")]
    [ApiController]
    [Route("")]
    public class EmployeeController : ControllerBase
    {
        #region Fields

        private readonly IEmployeeService _employeeService;

        #endregion Fields

        #region Constructor

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet("api/v1/persons")]
        public async Task<ActionResult<ICollection<PersonDto>>> GetPersons()
        {
            var employees = await _employeeService.GetAll();
            return employees.Count > 0 
                   ? Ok(employees) 
                   : NotFound();
        }

        [HttpGet("api/v1/person/{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(long id)
        {
            var person = await _employeeService.GetPerson(id);

            return person != null 
                   ? Ok(person) 
                   : NotFound();
        }

        [HttpPost("api/v1/person")]
        public async Task<ActionResult> CreatePerson(PersonDto person)
        {
            if (!await _employeeService.TryCreatePerson(person))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("api/v1/person/{id}")]
        public async Task<ActionResult> UpdatePerson(long id, PersonDto person)
        {
            if (!await _employeeService.TryUpdatePerson(id, person))
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("api/v1/person/{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            if (!await _employeeService.TryDeletePerson(id))
            {
                return NotFound();
            }

            return Ok();
        }

        #endregion Methods
    }
}
