using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iengine
{
    class Item
    {

        public string name;
        public bool valid;
        public List<Relation> relations;
    }

    class Relation
    {
        public string name;
        public string clause;
    }
    
}
