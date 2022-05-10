namespace HallOfFame.DTO
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    public class PersonDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        [Required]
        public string DisplayName { get; set; }

        [JsonProperty("skills")]
        public ICollection<SkillDto> Skills { get; set; }
    }
}
