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
        public bool ASK = false;
        public bool valid = false;
        public bool Checked = false;

        public List<Relation> relations;

        public Item()
        {
            relations = new List<Relation>();
        }
        public bool Contains(String name)
        {
            if (name == this.name)
                return true;
            return false;
        }
        
    }

    class Relation
    {
        public List<string> name;
        public string clause;

        public Relation()
        {
            name = new List<string>();
        }
    }
}
