namespace HallOfFame.Services
{
    using HallOfFame.DataBase.Repositories;
    using HallOfFame.DTO;

    using System.Collections.Generic;
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

        public Task<ICollection<PersonDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<PersonDto>> GetPerson(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryCreatePerson(PersonDto person)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryUpdatePerson(PersonDto person)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeletePerson(long id)
        {
            throw new NotImplementedException();
        }

        #endregion Methods
    }
}
