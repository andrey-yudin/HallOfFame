namespace HallOfFame.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Skill
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 10)]
        public byte Level { get; set; }

        public Person Person { get; set; }

        public long PersonId { get; set; }
    }
}
