using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasseioCavalo.DataStructure
{
    public class Graph
    {
        private List<Node> nodes;

        public Node[] Nodes
        {
            get { return this.nodes.ToArray(); }
        }

        public Graph()
        {
            this.nodes = new List<Node>();
        }

        public Node Find(string name)
        {
            foreach (Node n in nodes)
            {
                if (n.Name == name)
                    return n;
            }
            return null;
        }

        public void AddNode(string name)
        {
            AddNode(name, null);
        }

        public void AddNode(string name, object info)
        {
            if (Find(name) != null)
            {
                throw new Exception("Já existe um nó com esse nome!");
            }
            this.nodes.Add(new Node(name, info));
        }

        protected void AddNode(Node n)
        {
            if (Find(n.Name) == null)
            {
                nodes.Add(n);
            }
        }

        public void RemoveNode(string name)
        {
            Node existingNode = Find(name);
            if (existingNode == null)
            {
                throw new Exception("Não existe um nó com esse nome!");
            }
            this.nodes.Remove(existingNode);
        }

        public void AddEdge(string from, string to, double cost)
        {
            Node start = Find(from);
            Node end = Find(to);
            if (start == null)
                throw new Exception("Não existe o nó origem!");
            if (end == null)
                throw new Exception("Não existe o nó destino!");
            start.AddEdge(end, cost);
        }

        public Node[] GetNeighbours(string from)
        {
            Node start = Find(from);
            if (start == null)
                throw new Exception("Não existe um nó com esse nome!");
            List<Edge> edges = start.Edges;
            List<Node> neighbours = new List<Node>();
            foreach (Edge e in edges)
            {
                neighbours.Add(e.To);
            }
            return neighbours.ToArray();
        }

        public List<Node> DepthFirstSearch(string startNode)
        {
            Node start = Find(startNode);
            if (start == null)
                throw new Exception("Não existe um nó com esse nome!");

            List<Node> list = new List<Node>();

            Stack<Node> stack = new Stack<Node>();
            start.Visited = true;
            stack.Push(start);

            while (stack.Count > 0)
            {
                Node node = stack.Pop();
                list.Add(node);

                foreach (Edge e in node.Edges)
                {
                    if (e.To.Visited == false)
                    {
                        e.To.Visited = true;
                        stack.Push(e.To);
                    }
                }
            }
            return list;
        }

        public List<Node> BreadthFirstSearch(string startNode)
        {
            Node start = Find(startNode);
            if (start == null)
                throw new Exception("Não existe um nó com esse nome!");
            List<Node> list = new List<Node>();

            Queue<Node> queue = new Queue<Node>();
            start.Visited = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                list.Add(node);
                foreach (Edge e in node.Edges)
                {
                    if (e.To.Visited == false)
                    {
                        e.To.Visited = true;
                        queue.Enqueue(e.To);
                    }
                }
            }
            return list;
        }
    }
}

