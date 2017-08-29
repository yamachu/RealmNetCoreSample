using System;
using Newtonsoft.Json;
using Realms;

namespace RealmNetCoreSample.Models
{
    public class User : RealmObject
    {
        [PrimaryKey]
        [JsonIgnore]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [JsonIgnore]
        public string AccessToken { get; set; }

        [JsonIgnore]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
