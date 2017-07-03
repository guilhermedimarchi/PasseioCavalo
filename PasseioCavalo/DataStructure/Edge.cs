using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasseioCavalo.DataStructure
{
    public class Edge
    {
        public Node From { get; private set; }

        public Node To { get; private set; }

        public double Cost { get; set; }

        public Edge(Node from, Node to)
            : this(from, to, 0)
        {  }

        public Edge(Node from, Node to, double cost)
        {
            this.From = from;
            this.To = to;
            this.Cost = cost;
        }

        public override string ToString()
        {
            return String.Format("{0} = ({1:F4}) => {2}", 
                this.From, this.Cost, this.To);
        }
    }
}
