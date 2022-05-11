namespace HallOfFame.DataBase.Repositories
{
    using Models;

    public interface IEmployeeRepository
    {
        /// <summary>
        /// Return all persons
        /// </summary>
        /// <returns>Persons collection</returns>
        Task<ICollection<Person>> GetPersons();

        /// <summary>
        /// Get specified person
        /// </summary>
        /// <param name="id">Person id</param>
        /// <returns>True, Person item when success; false, null - otherwise</returns>
        Task<(bool, Person)> GetPerson(long id);

        /// <summary>
        /// Create new person item in collection
        /// </summary>
        /// <param name="person">New Person item</param>
        /// <returns>True when success, false - otherwise</returns>
        Task<bool> TryCreatePerson(Person person);

        /// <summary>
        /// Update exist person item in collection
        /// </summary>
        /// <param name="id">Person id</param>
        /// <param name="person">Edited Person item</param>
        /// <returns>True when success, false - otherwise</returns>
        Task<bool> TryUpdatePerson(long id, Person person);

        /// <summary>
        /// Delete specified person item
        /// </summary>
        /// <param name="id">Person id</param>
        /// <returns>True when success, false - otherwise</returns>
        Task<bool> TryDeletePerson(long id);
    }
}
