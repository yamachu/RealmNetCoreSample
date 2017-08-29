using System;
using Realms;

namespace RealmNetCoreSample.Models
{
    public class Announcement : RealmObject
    {
        [PrimaryKey]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

        public DateTimeOffset ModifiedAt { get; set; }
    }
}
