namespace HallOfFame.DataBase.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using HallOfFame.Models;

    public class EmployeeContext : DbContext
    {
        #region Properies

        /// <summary>
        /// Persons table
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Skills table
        /// </summary>
        public DbSet<Skill> Skills { get; set; }

        #endregion Properies

        #region Constructor

        public EmployeeContext(DbContextOptions options): base(options)
        {
        }

        #endregion Constructor

        #region Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
        }

        #endregion Methods
    }
}