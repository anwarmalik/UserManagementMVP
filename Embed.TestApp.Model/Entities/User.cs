using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Embed.TestApp.Model.Entities
{
    public class User : IAuditEntity
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]{3,15}$$", ErrorMessage = "Username cannot contain special characters. Total letters allowed are (3-15)")]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
    }

    public interface IAuditEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime LastModifiedAt { get; set; }
    }
}
