using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Responses
{
    public class PageResponse<T>
    {
        public IEnumerable<T> Data {get; set;}

        public Dictionary<string, string> Links {get; set;} = new Dictionary<string, string>();

        public Dictionary<string, string> Meta {get; set;} = new Dictionary<string, string>();
    }
}