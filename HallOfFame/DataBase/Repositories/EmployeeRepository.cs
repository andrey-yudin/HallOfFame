namespace HallOfFame.DataBase.Repositories
{
    using HallOfFame.DataBase.DataAccess;
    using HallOfFame.Models;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public class EmployeeRepository : IEmployeeRepository
    {
        #region Fields

        private readonly EmployeeContext _employeeContext;

        #endregion Fields

        #region Constructor

        public EmployeeRepository(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        #endregion Constructor

        #region Methods

        public async Task<ICollection<Person>> GetPersons()
        {
            return await _employeeContext.Persons.Include(person => person.Skills)
                                                 .ToListAsync();
        }

        public async Task<Person> GetPerson(long id)
        {
            return await _employeeContext.Persons.Where(person => person.Id == id)
                                                 .Include(person => person.Skills)
                                                 .FirstOrDefaultAsync();
        }

        public async Task<bool> TryCreatePerson(Person person)
        {
            await _employeeContext.AddAsync(person);

            try
            {
                await _employeeContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> TryUpdatePerson(Person person)
        {
            _employeeContext.Persons.Update(person);

            try
            {
                await _employeeContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> TryDeletePerson(long id)
        {
            var person = await _employeeContext.Persons.FirstOrDefaultAsync(person => person.Id == id);

            if (person == null)
            {
                return false;
            }

            _employeeContext.Persons.Remove(person);
            await _employeeContext.SaveChangesAsync();

            return true;
        }

        #endregion Methods
    }
}
