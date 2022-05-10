namespace HallOfFame.Services
{
    using DTO;

    public interface IEmployeeService
    {
        public Task<ICollection<PersonDto>> GetAll();

        public Task<ICollection<PersonDto>> GetPerson(long id);

        public Task<bool> TryCreatePerson(PersonDto person);

        public Task<bool> TryUpdatePerson(PersonDto person);

        public Task<bool> TryDeletePerson(long id);
    }
}
