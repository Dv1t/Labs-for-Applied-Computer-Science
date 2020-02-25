using System;
using System.Collections.Generic;

namespace lab3
{
    public class GraphMatrix : IGraph
    {
        private int _vertexCount;
        private int _edgeCount;
        private int[,] _table;

        public GraphMatrix(int[,]table,int edgeCount)
        {
            _table = table;
            _edgeCount = edgeCount+1;
            _vertexCount = table.Length/(edgeCount+1);
        }
        public GraphMatrix(int vertexCount,int edgeCount)
        {
            _vertexCount = vertexCount+1;
            _edgeCount = edgeCount+1;
            _table = new int[vertexCount+1, edgeCount+1];
            for(int i=0;i<vertexCount;i++)
            {
                Console.WriteLine($"Введите ребра выходящие из {i+1} вершины, по окончанию ввода введите 0");
                int temp = int.Parse(System.Console.ReadLine());
                while (temp!=0)
                {
                    _table[i+1,temp] = 1;
                    temp =int.Parse(System.Console.ReadLine());
                }
            }
        }

        public int CountEdges()
        {
            return _edgeCount;
        }

        public int CountVertexs()
        {
            return _vertexCount;
        }

        public int GetDistance(int vertex1, int vertex2)
        {
            for(int i=1;i<_edgeCount;i++)
            {
                if(_table[vertex1,i]!=0&&_table[vertex2,i]!=0)
                {
                    return _table[vertex1, i];
                }
            }
            return -1;
        }

        public List<int> GetNeighbours(int vertex)
        {
            List<int> result = new List<int>();
            for(int i =1;i<_edgeCount;i++)
            {
                if(_table[vertex,i]!=0)
                {
                    int edge = i;
                    for(int j=1;j<_vertexCount;j++)
                    {
                        if((_table[j,edge]!=0)&&(j != vertex))
                        {
                            result.Add(j);
                        }
                    }
                }
            }
            return result;
        }
    }
}
