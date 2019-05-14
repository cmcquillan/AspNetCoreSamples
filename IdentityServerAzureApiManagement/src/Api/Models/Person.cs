using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public bool Active { get; set; }
    }
}
