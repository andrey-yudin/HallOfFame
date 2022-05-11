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

        /// <inheritdoc />
        public async Task<ICollection<Person>> GetPersons()
        {
            return await _employeeContext.Persons.Include(person => person.Skills)
                                                 .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<(bool, Person)> GetPerson(long id)
        {
            try
            {
                var person = await _employeeContext.Persons.FindAsync(id);

                if (person == null)
                {
                    return (false, null);
                }

                _employeeContext.Entry(person)
                    .Collection(p => p.Skills)
                    .Load();

                return (true, person);
            }
            catch
            {
                return (false, null);
            }
        }

        /// <inheritdoc />
        public async Task<bool> TryCreatePerson(Person person)
        {
            try
            {
                await _employeeContext.AddAsync(person);
                await _employeeContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc />
        public async Task<bool> TryUpdatePerson(long id, Person person)
        {
            try
            {
                var personExists = await _employeeContext.Persons.AnyAsync(p => p.Id == id);

                if (personExists)
                {
                    var skills = _employeeContext.Skills.ToList()
                                    .Where(s => s.PersonId == person.Id)
                                    .Except(person.Skills);

                    person.Id = id;

                    _employeeContext.Skills.RemoveRange(skills);
                    _employeeContext.Update(person);

                    await _employeeContext.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch 
            {
                return false;
            }
        }

        /// <inheritdoc />
        public async Task<bool> TryDeletePerson(long id)
        {
            try
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
            catch
            {
                return false;
            }
        }

        #endregion Methods
    }
}
