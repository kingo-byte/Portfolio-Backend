using Portfolio_Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } 
        public Guid Token { get; set; } = new Guid();

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? ExpiresAt {  get; set; }

        public User? User { get; set; }
    }
}
