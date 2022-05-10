namespace HallOfFame.DataBase.Repositories
{
    using Models;

    public interface IEmployeeRepository
    {
        Task<ICollection<Person>> GetPersons();

        Task<Person> GetPerson(long id);

        Task<bool> TryCreatePerson(Person person);

        Task<bool> TryUpdatePerson(Person person);

        Task<bool> TryDeletePerson(long id);
    }
}
