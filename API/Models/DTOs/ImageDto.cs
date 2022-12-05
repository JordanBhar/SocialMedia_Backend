using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Entities;

namespace API.Models.DTOs
{
     public class ImageDto
    {
        public Guid Id { get; set; } 
        public string Url { get; set; } 
        public Guid UserId {get; set;}
        public String UserName {get; set;}
        public DateTime PostingDate { get; set; }
        public List<Tag> Tags { get; set; }
    }
}