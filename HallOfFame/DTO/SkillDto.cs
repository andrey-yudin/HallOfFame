namespace HallOfFame.DTO
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    /// <summary>
    /// Skill data transfer object
    /// </summary>
    public class SkillDto
    {
        /// <summary>
        /// Skill name
        /// </summary>
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Skill level
        /// </summary>
        [JsonProperty("level")]
        [Required]
        [Range(1, 10)]
        public byte Level { get; set; }
    }
}
