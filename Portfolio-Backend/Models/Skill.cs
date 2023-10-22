﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio_Backend.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
