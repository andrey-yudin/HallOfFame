namespace HallOfFame.Controllers
{
    using HallOfFame.DTO;
    using HallOfFame.Services;

    using NLog;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [ApiController]
    [Route("")]
    public class EmployeeController : ControllerBase
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        #endregion Fields

        #region Constructor

        public EmployeeController(IEmployeeService employeeService)
        {
            _logger = LogManager.GetCurrentClassLogger();
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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("api/v1/person/{id}")]
        public async Task<ActionResult<PersonDto>> GetPerson(long id)
        {
            var person = await _employeeService.GetPerson(id);

            return person != null 
                   ? Ok(person) 
                   : NotFound();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("api/v1/person")]
        public async Task<ActionResult> CreatePerson(PersonDto person)
        {
            if (!ModelState.IsValid)
            {
                _logger.Warn($"Bad request: {Request}");
                return BadRequest();
            }

            if (!await _employeeService.TryCreatePerson(person))
            {
                return BadRequest();
            }

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("api/v1/person/{id}")]
        public async Task<ActionResult> UpdatePerson(long id, PersonDto person)
        {
            if (!ModelState.IsValid)
            {
                _logger.Warn($"Bad request: {Request}");
                return BadRequest();
            }

            if (!await _employeeService.TryUpdatePerson(id, person))
            {
                return BadRequest();
            }

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
