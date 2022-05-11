namespace HallOfFame.Services
{
    using DTO;

    public interface IEmployeeService
    {
        /// <summary>
        /// Return all persons
        /// </summary>
        /// <returns>PersonDtos collection</returns>
        public Task<ICollection<PersonDto>> GetAll();

        /// <summary>
        /// Return specified person
        /// </summary>
        /// <param name="id">Person id</param>
        /// <returns>PersonDto</returns>
        public Task<PersonDto> GetPerson(long id);

        /// <summary>
        /// Create new person item in collection
        /// </summary>
        /// <param name="person">New PersonDto item</param>
        /// <returns>True when success, false - otherwise</returns>
        public Task<bool> TryCreatePerson(PersonDto person);

        /// <summary>
        /// Update exist person item in collection
        /// </summary>
        /// <param name="id">Person id</param>
        /// <param name="person">Edited PersonDto item</param>
        /// <returns>True when success, false - otherwise</returns>
        public Task<bool> TryUpdatePerson(long id, PersonDto person);

        /// <summary>
        /// Delete specified person item
        /// </summary>
        /// <param name="id">Person id</param>
        /// <returns>True when success, false - otherwise</returns>
        public Task<bool> TryDeletePerson(long id);
    }
}
