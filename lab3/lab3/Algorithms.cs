using System;
using System.Collections.Generic;
using System.Web.UI;

namespace lab3
{
    public class Algorithms
    {

        private bool[] _visited;
        int[] _colors;
        Queue<int> _queue;
        IGraph _graph;
        List<Pair> _result;
        int _recCount;

        public Algorithms(IGraph graph)
        {
            _graph = graph;
            _visited = new bool[graph.CountVertexs()];
            _colors = new int[graph.CountVertexs()];
            _queue = new Queue<int>();
            _result = new List<Pair>();
            _recCount = 0;
        }

        public List <int> DFS(int vertex)
        {
            List<int> result = new List<int>();
            result.Add(vertex);
            _visited[vertex] = true;
            List<int> neighbours = _graph.GetNeighbours(vertex);
            foreach(int neighbour in neighbours)
            {
                if(_visited[neighbour]==false)
                {
                    _recCount++;
                    result.AddRange(DFS(neighbour));
                }
            }
            _recCount--;
            if(_recCount==0)
            {
                Clear();
            }
            return result;
        }

        public void BFS(int startVertex,int targetVertex)
        {
            _colors[startVertex] = 1;
            if(startVertex == targetVertex)
            {
                EndOfBfs(targetVertex);
                return; 
            }
            List<int> neighbours = _graph.GetNeighbours(startVertex);
            foreach(int neighbour in neighbours)
            {
                if (_colors[neighbour] == 0)
                {
                    Pair step = new Pair();
                    step.First = startVertex;
                    step.Second = neighbour;
                    _result.Add(step);
                    if (neighbour == targetVertex)
                    {
                        EndOfBfs(targetVertex);
                        return;
                    }
                    _queue.Enqueue(neighbour);
                    _colors[neighbour] = 1;
                }
            }
            if(_queue.Count!=0)
            {
                BFS(_queue.Dequeue(), targetVertex);
            }
            _colors[startVertex] = 2;
        }

        private void EndOfBfs(int targetVertex)
        {
            Console.WriteLine("Порядок обхода");
            foreach(Pair pair in _result)
            {
                Console.Write($"{pair.First}->{pair.Second} ");
            }
            int start = (int)_result[0].First;
            int end = targetVertex;
            Console.WriteLine($"\nКратчайший путь из {start} в {end}");
            int point = end;
            int step=0;
            for(int i=0;i<_result.Count;i++)
            {
                if ((int)_result[i].Second == targetVertex)
                {
                    step = i;
                }
            }
            Stack<int> path = new Stack<int>();
            while(point!=start)
            {
                path.Push(point);
                point = (int)_result[step].First;
                for(int i=0; i< _result.Count;i++)
                {
                    if((int)_result[i].Second == point)
                    {
                        step = i;
                        break;
                    }
                }
            }
            path.Push(start);
            while(path.Count>1)
            {
                Console.Write(path.Pop()+"->");
            }
            Console.WriteLine(path.Pop());
            Clear();
        }

        private void Clear()
        {
            _result = new List<Pair>();
            _visited = new bool[_graph.CountVertexs()];
            _colors = new int[_graph.CountVertexs()];
            _queue = new Queue<int>();
        }

        public void Dijkstra(int startVertex)
        {
            int maxInt = 999999;
            _visited = new bool[_graph.CountVertexs()];
            int[] _distance = new int [_graph.CountVertexs()];
            for (int i = 0; i < _distance.Length; i++)
            {
                _distance[i] = maxInt;
            }
            int index=0,u;
            _distance[startVertex] = 0;
            int vertexCount = _graph.CountVertexs();
            for (int count =0;count<vertexCount-1;count++)
            {
                int min = maxInt;
                for(int i=1;i<vertexCount;i++)
                {
                    if(!_visited[i]&&_distance[i]<=min)
                    {
                        min = _distance[i];
                        index = i;
                    }
                    u= index;
                    _visited[u] = true;
                    List<int> neighbours = _graph.GetNeighbours(u);
                    foreach(int neighbour in neighbours)
                    {
                        int distanceTemp = _graph.GetDistance(u,neighbour);
                        if ((!_visited[neighbour])&&(distanceTemp+_distance[u]<_distance[neighbour])&&(_distance[u]!= maxInt))
                        {
                            _distance[neighbour] = _distance[u] + distanceTemp;
                        }
                    }
                }
                
            }
            Console.WriteLine($"Стоимость пути от {startVertex} вершины до остальных");
            for (int i = 1; i < _distance.Length; i++)
            {
                if (_distance[i] == maxInt)
                {
                    Console.WriteLine($"Из {startVertex} в {i} пути нет");
                    continue;
                }
                Console.WriteLine($"{startVertex}->{i} = {_distance[i]}");
            }
        }

    }
}
