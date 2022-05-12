namespace HallOfFame.DataBase.Repositories
{
    using HallOfFame.DataBase.DataAccess;
    using HallOfFame.Models;

    using Microsoft.EntityFrameworkCore;

    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NLog;

    public class EmployeeRepository : IEmployeeRepository
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly EmployeeContext _employeeContext;

        #endregion Fields

        #region Constructor

        public EmployeeRepository(EmployeeContext employeeContext)
        {
            _logger = LogManager.GetCurrentClassLogger();
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
                _logger.Trace(() => $"Try to get person with id {id}");

                var person = await _employeeContext.Persons.FindAsync(id);

                if (person == null)
                {
                    _logger.Warn($"Can`t find person with id {id}");

                    return (false, null);
                }

                _employeeContext.Entry(person)
                    .Collection(p => p.Skills)
                    .Load();

                _logger.Trace(() => $"Person {person} created successfully");

                return (true, person);
            }
            catch (Exception ex)
            {
                _logger.Error($"Have an error while getting person with id {id}, exception message: {ex}");
                return (false, null);
            }
        }

        /// <inheritdoc />
        public async Task<bool> TryCreatePerson(Person person)
        {
            try
            {
                _logger.Trace(() => $"Try to create person {person}");

                await _employeeContext.AddAsync(person);
                await _employeeContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Have an error while creating person {person}, exception message: {ex}");
                return false;
            }
        }

        /// <inheritdoc />
        public async Task<bool> TryUpdatePerson(long id, Person person)
        {
            try
            {
                _logger.Trace(() => $"Try to update person {person}, id {id}");

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

                    _logger.Trace(() => $"Person with id {id} upated");

                    return true;
                }

                _logger.Warn($"Can`t update person with id {id}");

                return false;
            }
            catch (Exception ex)
            {
                _logger.Error($"Have an error while updating person {person} with id {id}, exception message: {ex}");
                return false;
            }
        }

        /// <inheritdoc />
        public async Task<bool> TryDeletePerson(long id)
        {
            try
            {
                _logger.Trace(() => $"Try to delete person with id {id}");

                var person = await _employeeContext.Persons.FirstOrDefaultAsync(person => person.Id == id);

                if (person == null)
                {
                    _logger.Warn(() => $"No person with id {id}");
                    return false;
                }

                _employeeContext.Persons.Remove(person);
                await _employeeContext.SaveChangesAsync();

                _logger.Trace(() => $"Person with id {id} deleted successfully");

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"Have an error while deleting person with id {id}, exception message: {ex}");
                return false;
            }
        }

        #endregion Methods
    }
}
