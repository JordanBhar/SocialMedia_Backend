using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public User User { get; set; }
        public DateTime PostingDate { get; set; }
        public List<Tag> Tags { get; set; }
    }
}