using System;
using System.Collections.Generic;
using Autofac;

namespace lab3
{
    class Program
    {

        public static void Main()
        {
            #region Preset graphs
            //Набранные заранее графы
            int[,] t1 = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 0, 1, 0, 1 }, { 0, 0, 0, 1, 1 }, { 0, 0, 1, 0, 0 }, { 0, 1, 0, 1, 0 } };
            int[,] t2 = new int[,] { { 0, 0, 0, 0, 0, 0 }, { 0, 1, 1, 0, 0, 0 }, { 0, 1, 0, 1, 1, 0 }, { 0, 0, 0, 0, 1, 1 }, { 0, 0, 1, 1, 0, 1 } };
            int[,] t3 = new int[,] {
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 1, 4, 0, 2, 0 },
                { 0, 0, 0, 0, 9, 0, 0 },
                { 0, 4, 0, 0, 7, 0, 0 },
                { 0, 0, 9, 0, 0, 0, 2 },
                { 0, 0, 0, 0, 0, 0, 8 },
                { 0, 0, 0, 0, 0, 0, 0 } };
            IGraph graph3 = new GraphTable(t1, 5);
            IGraph graph4 = new GraphMatrix(t2, 5);
            IGraph graph5 = new GraphTable(t3, 7);
            #endregion

            #region Dependency Insertion
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance<GraphTable>(new GraphTable(t3, 7)).As<IGraph>();
            containerBuilder.RegisterType<Algorithms>().AsSelf();
            IContainer container = containerBuilder.Build();
            #endregion

            #region Console input
            //Ввод графов через консоль
            Console.WriteLine("Введите колличество вершин в графе");
            int vertexCount;
            if (!int.TryParse(System.Console.ReadLine(), out vertexCount))
            {
                Main();
                return;
            }
            Console.WriteLine("Введите колличество ребер в графе");
            int edgeCount; ;
            if (!int.TryParse(System.Console.ReadLine(), out edgeCount))
            {
                Main();
                return;
            }
            Console.WriteLine("Ввод таблицы смежности");
            IGraph graph1 = new GraphTable(vertexCount,edgeCount);
            Console.WriteLine("Ввод матрицы инцедентности");
            IGraph graph2 = new GraphMatrix(vertexCount, edgeCount);
            #endregion

            #region Main job
            //Создание экземпляра класса Algorithms для графа3
            Algorithms algorithms = container.Resolve<Algorithms>();

            //Вызов методов класса Algorithms 
            List<int> dfs = algorithms.DFS(1);
            Console.Write("Порядок обхода DFS: ");
            foreach(int element in dfs)
            {
                Console.Write($"{element} ");
            }
            Console.WriteLine();

            algorithms.BFS(1, 6);
            Console.WriteLine();

            algorithms.Dijkstra(1);
            #endregion

            System.Console.ReadKey();
        }
       
    }
}
