using System;
using System.Collections.Generic;

namespace lab3
{
    public class GraphTable : IGraph
    {
        private int _vertexCount;
        private int _edgeCount;
        private int[,] _table;

        public GraphTable(int[,] table,int edgeCount)
        {
            _edgeCount = edgeCount;
            _vertexCount =(int) Math.Sqrt(table.Length);
            _table = table;
        }
        public GraphTable(int vertexCount, int edgeCount)
        {
            _vertexCount = vertexCount+1;
            _edgeCount = edgeCount+1;
            _table = new int[_vertexCount,_vertexCount];
            for (int i = 1; i < _edgeCount; i++)
            {
                Console.WriteLine("Введите сопряженые вершины");
                int a = int.Parse(System.Console.ReadLine());
                int b = int.Parse(System.Console.ReadLine());
                Console.WriteLine("Они направлены?(y/n)");
                if (System.Console.ReadLine() == "y")
                {
                    _table[a,b] = 1;
                }
                else
                {
                    _table[a,b] = 1;
                    _table[b, a] = 1;
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
            if (_table[vertex1, vertex2] > 0)
            {
                return _table[vertex1, vertex2];
            }
            else
                return -1;
        }

        public List<int> GetNeighbours(int vertex)
        {
            List<int> result = new List<int>();
            for(int i=1;i<_vertexCount;i++)
            {
                if(_table[vertex,i]!=0)
                {
                    result.Add(i);
                }
            }
            return result;
        }
    }
}
