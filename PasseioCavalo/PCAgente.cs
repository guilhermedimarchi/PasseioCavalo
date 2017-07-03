using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasseioCavalo.DataStructure;

namespace PasseioCavalo
{
    public class PCAgente : Graph
    {
        private int n;
        private int[,] tabuleiro;
        private int[] horizontal;
        private int[] vertical;
        private int[,] accessibility;

        //ATRIBUI VALORES PARA AS POSIÇÔES DA MATRIZ. QUANTO MAIS PROXIMO AS BORDAS MELHOR!
        public PCAgente(int[,] estadoInicial, int n)
        {
            this.n = n;
            switch (n)
            {
                case 5:
                    accessibility = new int[,]
                    {
                        {2,3,4,3,2},
                        {3,4,6,4,3},
                        {4,6,8,6,4},
                        {3,4,6,4,3},
                        {2,3,4,3,2}
                    };
                    break;
                case 6:
                    accessibility = new int[,]
                    {
                        {2,3,4,4,3,2},
                        {3,4,6,6,4,3},
                        {4,6,8,8,6,4},
                        {4,6,8,8,6,4},
                        {3,4,6,6,4,3},
                        {2,3,4,4,3,2}
                    };
                    break;
                default:
                    accessibility = new int[,]
                    {
                        {2,3,4,4,4,4,3,2},
                        {3,4,6,6,6,6,4,3},
                        {4,6,8,8,8,8,6,4},
                        {4,6,8,8,8,8,6,4},
                        {4,6,8,8,8,8,6,4},
                        {4,6,8,8,8,8,6,4},
                        {3,4,6,6,6,6,4,3},
                        {2,3,4,4,4,4,3,2}
                    };
                    break;
            }
            tabuleiro = new int[n, n];
            //POSSIVEIS MOVIMENTOS DO CAVALO (ELE PODE ANDAR DOIS NA HORIZONTAL E UM NA VERTICAL POR EXEMPLO)
            horizontal = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
            vertical = new int[]   { 1, 2, 2, 1, -1, -2, -2, -1 };
            int[,] ret = encontrarNivel(estadoInicial);
            if (ret[0, 0] == 0)
            {
                tabuleiro = new int[n, n];
                tabuleiro[0, 0] = 1;
            }
            else
            {
                tabuleiro = estadoInicial;
            }
                
        }

        public int[] ObterSolucao()
        {
            return BuscaSolucao(new Node(GerarNome(tabuleiro), tabuleiro));
        }

        //TRANSFORMA SITUACAO (MATRIZ DE INT) EM STRING DE ESTADO
        private string GerarNome(int[,] t)
        {
            string nome = "";
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    nome += t[i, j].ToString();
            return nome;
        }


        //ENCONTRA O NIVEL DO ESTADO PASSADO PARA LIMITAR A BUSCA EM PROFUNDIDADE
        private int[,] encontrarNivel(int[,] estado)
        {
            int nivel = Int32.MinValue;
            int[,] ret = new int[1, 3];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (nivel < estado[i, j])
                    {
                        nivel = estado[i, j];
                        ret[0, 0] = nivel;
                        ret[0, 1] = i;
                        ret[0, 2] = j;
                    }
            return ret;
        }

        private int[] BuscaSolucao(Node inicial)
        {
            //BUSCA EM PROFUNDIDADE POIS FEITO A ORDENACAO, A FUNCAO VAI PESQUISAR PRIMEIRO NOS CAMINHOS COM MELHORES VALORES
            Stack<Node> stack = new Stack<Node>();
            int limite = n * n;

            int[,] ret = encontrarNivel((int[,])inicial.Info);
            inicial.Nivel = ret[0, 0];
            inicial.X = ret[0, 1];
            inicial.Y = ret[0, 2];

            this.AddNode(inicial);
            stack.Push(inicial);

            while (stack.Count() > 0)
            {
                Node node = stack.Pop();
                
                if (AchouSolucao(node))
                {
                    Console.WriteLine("Solução encontrada:\n");
                    MostraSolucao(node);
                    return GerarSolucao(node);
                }

                List<Node> sucessores = FuncaoSucessor(node);
                foreach (Node nod in sucessores)
                {
                    if (Find(nod.Name) == null && node.Nivel < limite)
                    {
                        this.AddNode(nod);
                        this.AddEdge(nod.Name, node.Name, 0);
                        stack.Push(nod);
                    }
                }
            }
            Console.WriteLine("Não achou a solução!");
            return null;
        }

        private int[] GerarSolucao(Node node)
        {
            int[] ret = new int[n*n];
            int[,] estado = (int[,])node.Info;

            for (int p = 0; p < n * n; p++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        if (estado[i, j] == p + 1)
                            ret[p] = i * n + j;
            return ret;
        }


        //VERIFICA SE O ESTADO É O PROCURADO(SE TODA MATRIZ POSSUI 0)
        private bool AchouSolucao(Node node)
        {
            int[,] t = ((int[,])node.Info).Clone() as int[,];
            
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (t[i, j] == 0)
                        return false;
            return true;
        }

        //MOSTRA NO CONSOLE A SOLUCAO
        private void MostraSolucao(Node node)
        {
            int[,] t = ((int[,])node.Info).Clone() as int[,];
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(t[i, j].ToString() + " ");
                Console.WriteLine("");
            }
        }

        private List<Node> FuncaoSucessor(Node node)
        {
            List<Node> lista = new List<Node>();
            int[,] t = ((int[,])node.Info).Clone() as int[,];
            int i = node.Nivel + 1;
            int x = node.X;
            int y = node.Y;
            // 08 possibilidades de movimentos (HORIZONTAL E VERTICALL)
            for (int j = 0; j <= 7; j++)
            {
                //POSICAO X,Y ATUAL SOMADO COM OS VETORES HORIZONTAL E VERTICAL PARA PEGAR A PROXIMA POSIÇAO
                int u = x + horizontal[j];
                int v = y + vertical[j];

                if (EhMovimentoValido(t, u, v)){
                    int[,] tab = ((int[,])node.Info).Clone() as int[,];
                    tab[u, v] = i;
                    Node newNode = new Node(GerarNome(tab), tab);
                    newNode.Nivel = i;
                    newNode.X = u;
                    newNode.Y = v;
                    lista.Add(newNode);
                }
            }
            ordenaJogadas(lista);
            return lista;
        }

        //ORDENACAO POR SELECAO DIRETA PARA ORDENAR OS POSSIVEIS JOGADAS SUCESSORES
        private void ordenaJogadas(List<Node> lista)
        {
            for (int i = 0; i < lista.Count(); i++)
            {
                int pos = i;
                for (int j = i + 1; j < lista.Count(); j++)
                {
                    if(accessibility[lista[j].X, lista[j].Y]  > accessibility[lista[pos].X, lista[pos].Y])
                    {
                        pos = j;
                    }
                    Node temp = lista[i];
                    lista[i] = lista[pos];
                    lista[pos] = temp;
                }
            }
        }

        //VERIFICA SE O PROXIMO MOVIMENTO ESTA DENTRO DO TABULEIRO
        private bool EhMovimentoValido(int[,] t, int u, int v)
        {
            return (u >= 0) && (u < n) && (v >= 0) && (v < n) && t[u, v] == 0;
        }
    }
}
