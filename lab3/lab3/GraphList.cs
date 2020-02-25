using System;
using System.Collections.Generic;

namespace lab3
{
    class GraphList : IGraph
    {
        public int CountEdges()
        {
            throw new NotImplementedException();
        }

        public int CountVertexs()
        {
            throw new NotImplementedException();
        }

        public int GetDistance(int vertex1, int vertex2)
        {
            throw new NotImplementedException();
        }

        public List<int> GetNeighbours(int vertex)
        {
            throw new NotImplementedException();
        }

        //const int _vMax = 100;
        //const int _eMax = _vMax * 2;
        //static int[] _head = new int[_vMax];
        //static int[] _nextEl = new int[_eMax];
        //static int[] _terminal = new int[_eMax];
        //private static int _n, _m, _j, _k;
        //_k = 0;
        //Console.WriteLine("Введите кол. вершин");
        //_n = int.Parse(System.Console.ReadLine());
        //Console.WriteLine("Введите кол. рёбер");
        //_m = int.Parse(System.Console.ReadLine());
        //Console.WriteLine("Вводите смежные вершины");
        //for(int i=0;i<_m;i++)
        //{
        //    int v, u;
        //    v = int.Parse(System.Console.ReadLine());
        //    u = int.Parse(System.Console.ReadLine());
        //    Console.WriteLine("Ребро ориентировано?(y/n)");
        //    if(System.Console.ReadLine()=="y")
        //    {
        //        Add(v, u);
        //    }
        //    else
        //    {
        //        Add(v, u);
        //        Add(u, v);
        //    }
        //}
        //Console.WriteLine("Вывод списка смежности");
        //for(int i=1;i<_n+1;i++)
        //{
        //    _j = _head[i];
        //    Console.Write($"{i}->");
        //    while(_j>0)
        //    {
        //        if(_nextEl[_j]!=0)
        //        {
        //            Console.Write(_terminal[_j]);
        //        }
        //        else
        //        {
        //            Console.Write($"{_terminal[_j]}, ");
        //        }
        //        _j = _nextEl[_j];
        //    }
        //    Console.WriteLine();
        //}
        //private static void Add(int v, int u)
        //{
        //    _k++;
        //    _terminal[_k] = u;
        //    _nextEl[_k] = _head[v];
        //    _head[v] = _k;
        //}
    }
}
