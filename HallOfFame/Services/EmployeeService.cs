namespace HallOfFame.Services
{
    using HallOfFame.DataBase.Repositories;
    using HallOfFame.DTO;
    using HallOfFame.Models;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EmployeeService : IEmployeeService
    {
        #region Fields

        private readonly IEmployeeRepository _employeeRepository;

        #endregion Fields

        #region Constructor

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion Constructor

        #region Methods

        /// <inheritdoc />
        public async Task<ICollection<PersonDto>> GetAll()
        {
            var persons = await _employeeRepository.GetPersons();

            return persons.Select(person => GetExternalPerson(person))
                          .ToList();
        }

        /// <inheritdoc />
        public async Task<PersonDto> GetPerson(long id)
        {
            var (personExists, person) = await _employeeRepository.GetPerson(id);

            return personExists ? GetExternalPerson(person) : null;
        }

        /// <inheritdoc />
        public async Task<bool> TryCreatePerson(PersonDto person)
        {
            return await _employeeRepository.TryCreatePerson(GetInternalPerson(person));
        }

        /// <inheritdoc />
        public async Task<bool> TryUpdatePerson(long id, PersonDto person)
        {
            return await _employeeRepository.TryUpdatePerson(id, GetInternalPerson(person));
        }

        /// <inheritdoc />
        public async Task<bool> TryDeletePerson(long id)
        {
            return await _employeeRepository.TryDeletePerson(id);
        }

        private PersonDto GetExternalPerson(Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Name = person.Name,
                DisplayName = person.DisplayName,
                Skills = person.Skills.Select(skill => GetExternaSkill(skill)).ToList(),
            };
        }

        private Person GetInternalPerson(PersonDto personDto)
        {
            return new Person
            {
                Name = personDto.Name,
                DisplayName= personDto.DisplayName,
                Skills = personDto.Skills.Select(skill => GetInternalSkill(skill)).ToList(),
            };
        }

        private SkillDto GetExternaSkill(Skill skill)
        {
            return new SkillDto
            {
                Name = skill.Name,
                Level = skill.Level,
            };
        }

        private Skill GetInternalSkill(SkillDto skillDto)
        {
            return new Skill
            {
                Name = skillDto.Name,
                Level = skillDto.Level,
            };
        }

        #endregion Methods
    }
}
