using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;

namespace API.Models.DTOs
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        
        public string Email { get; set; }
        
        public string Name { get; set; }

        public List<String> ImageUrl { get; set; }
    }
}