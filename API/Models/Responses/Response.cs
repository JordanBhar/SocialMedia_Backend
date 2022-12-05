using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Responses
{
    public class Response<T>
    {

        public T Data { get; set; }
        public List<String> Links {get; set;}
    }
}