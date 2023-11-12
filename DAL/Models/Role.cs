using DAL.Enum;
using Portfolio_Backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [EnumDataType(typeof(UserRoleEnum), ErrorMessage = "Invalid Input")]
        public string Name { get; set; }
    }
}
