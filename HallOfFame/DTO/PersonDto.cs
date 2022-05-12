namespace HallOfFame.DTO
{
    using Newtonsoft.Json;

    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Person data transfer object
    /// </summary>
    public class PersonDto
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("id")]
        public long? Id { get; set; }

        /// <summary>
        /// Person name
        /// </summary>
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Person display name
        /// </summary>
        [JsonProperty("display_name")]
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        /// Person skills
        /// </summary>
        [JsonProperty("skills")]
        public ICollection<SkillDto> Skills { get; set; }
    }
}
