using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketLib
{
    public class Astronaut : Human
    {
        private string post = "Undefined";

        public string Post { get { return post; } set { post = value; } }

        public Astronaut(string name, string post) {
            FullName = name;
            Post = post;
        }

    }
}
