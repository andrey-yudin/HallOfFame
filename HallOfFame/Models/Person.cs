namespace HallOfFame.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Person
    {
        [Key]
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public ICollection<Skill> Skills { get; set; }
    }
}
