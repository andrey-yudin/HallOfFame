namespace HallOfFame.DTO
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class SkillDto
    {
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("level")]
        [Required]
        [Range(1, 10)]
        public byte Level { get; set; }
    }
}
