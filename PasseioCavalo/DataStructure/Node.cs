using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasseioCavalo.DataStructure
{
    public class Node
    {
        public string Name { get; set; }

        public object Info { get; set; }

        public List<Edge> Edges { get; set; }

        public bool Visited { get; set; }

        public int Nivel { get; set; }

        public Node()
        {
            this.Edges = new List<Edge>();
        }

        public Node(string name, object info) : this()
        {
            this.Name = name;
            this.Info = info;
        }

        public void AddEdge(Node to)
        {
            AddEdge(to, 0);
        }

        public void AddEdge(Node to, double cost)
        {
            this.Edges.Add(new Edge(this, to, cost));
        }

        public override string ToString()
        {
            if (this.Info != null)
            {
                return String.Format("{0}({1})", this.Name, this.Info);
            }
            return this.Name;
        }


        public int X { get; set; }

        public int Y { get; set; }
    }
}
