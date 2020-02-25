using System.Collections.Generic;

namespace lab3
{
    public interface IGraph
    {
        List<int> GetNeighbours(int vertex);

        int CountVertexs();

        int CountEdges();

        int GetDistance(int vertex1, int vertex2);
    }
}
