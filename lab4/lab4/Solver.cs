using System;
using System.Text;

namespace lab4
{
    class Solver
    {
        private int _rows;
        private int _columns;
        private double[,] _matrix;

        public Solver(double[,] matrix)
        {
            _rows = matrix.GetLength(0);
            _columns = matrix.GetLength(1);

            _matrix = matrix;
        }
        private bool SelectLeading(int n)
        {
            //Найдем номер строки, с наибольшим
            //элементом в столбце n
            int iMax = n;
            for (int i = n + 1; i < _rows; i++)
                if (Math.Abs(_matrix[iMax, n]) < Math.Abs(_matrix[i, n]))
                    iMax = i;
            // Переставить строки iMax и n
            if (iMax != n)
            {
                for (int i = n; i < _columns; i++)
                {
                    double t = _matrix[n, i];
                    _matrix[n, i] = _matrix[iMax, i];
                    _matrix[iMax, i] = t;
                }
                return true;
            }
            return false;
        }

        private void SubtractRow(int k)
        {
            double m = _matrix[k, k];
            for (int i = k + 1; i < _rows; i++)
            {
                double t = _matrix[i, k] / m;
                for (int j = k; j < _columns; j++)
                {
                    _matrix[i, j] = _matrix[i, j] - _matrix[k, j] * t;
                }
            }
        }

        private bool ToTriangleMatrix()
        {
            for (int i = 1; i < _rows; i++)
            {
                SelectLeading(i - 1);
                if (Math.Abs(_matrix[i - 1, i - 1]) != 0)
                {
                    SubtractRow(i - 1);
                }
                else return false;
            }

            return true;
        }

        private int Rank(bool isExtendedMatrix)
        {
            int rang = 0;
            int q = 1;

            int rows, columns;

            rows = _rows;

            if (isExtendedMatrix)
                columns = _columns;
            else columns = _columns - 1;

            while (q <= MinValue(rows, columns))
            {
                double[,] matbv = new double[q, q];
                for (int i = 0; i < (rows - (q - 1)); i++)
                {
                    for (int j = 0; j < (columns - (q - 1)); j++)
                    {
                        for (int k = 0; k < q; k++)
                        {
                            for (int c = 0; c < q; c++)
                            {
                                matbv[k, c] = _matrix[i + k, j + c];
                            }
                        }

                        if (CalculateDeterminant(matbv, matbv.GetLength(0)) != 0)
                        {
                            rang = q;
                        }
                    }
                }
                q++;
            }

            return rang;
        }

        private int MinValue(int a, int b)
        {
            if (a >= b)
                return b;
            else
                return a;
        }

        private double CalculateDeterminant(double[,] matrix, int rows)
        {
            double det = 0;
            int k = 1;
            double[,] new_matrix = new double[rows, rows];

            if (rows == 1)
                return matrix[0, 0];
            else if (rows == 2)
            {
                det = matrix[0, 0] * matrix[1, 1] - matrix[1, 0] * matrix[0, 1];

                return det;
            }
            else
            {
                for (int i = 0; i < rows; i++)
                {
                    det += k * matrix[i, 0] * CalculateDeterminant(GetMatr(matrix, new_matrix, i, 0, rows), rows - 1);
                    k = -k;
                }

                return det;
            }
        }

        //Получение матрицы путем вычеркивание i-ой строки и j-ого столбца
        private double[,] GetMatr(double[,] matrix, double[,] p, int i, int j, int m)
        {
            int ki, kj, di, dj;
            di = 0;

            for (ki = 0; ki < m - 1; ki++)
            { // проверка индекса строки
                if (ki == i) di = 1;
                dj = 0;
                for (kj = 0; kj < m - 1; kj++)
                { // проверка индекса столбца
                    if (kj == j) dj = 1;
                    p[ki, kj] = matrix[ki + di, kj + dj];
                }
            }

            return p;
        }

        private int[] FindBaseVariables(int r)
        {
            int rows, columns;
            int[] baseVar = new int[r];

            rows = _rows;
            columns = _columns - 1;

            double[,] matbv = new double[r, r];

            for (int i = 0; i < (rows - (r - 1)); i++)
            {
                for (int j = 0; j < (columns - (r - 1)); j++)
                {
                    if (CalculateDeterminant(matbv, matbv.GetLength(0)) == 0)
                    {
                        for (int k = 0; k < r; k++)
                        {
                            for (int c = 0; c < r; c++)
                            {
                                matbv[k, c] = _matrix[i + k, j + c];
                                baseVar[c] = j + c;
                            }
                        }

                    }
                    else break;
                }
            }

            return baseVar;
        }

        public string Solve()
        {
            StringBuilder stringBuilder = new StringBuilder(); 
            if (Rank(true) == Rank(false))
            {
                int rank = Rank(true);

                double[] vector = new double[_columns - 1];

                ToTriangleMatrix();
                stringBuilder.Append("Система совместна и ");
                Console.Write("Система совместна и ");
                if (rank == _columns - 1)
                {
                    stringBuilder.Append("имеет 1 единственное решение\n");
                    Console.WriteLine("имеет 1 единственное решение");

                    int nb = _columns - 1;

                    for (int n = _rows - 1; n >= 0; n--)
                    {
                        double sum = 0;
                        for (int i = n + 1; i < nb; i++)
                            sum += vector[i] * _matrix[n, i];
                        vector[n] = (_matrix[n, nb] - sum) / _matrix[n, n];
                    }
                }
                else
                {
                    stringBuilder.Append("имеет бесконечное множество решений\n");
                    Console.WriteLine("имеет бесконечное множество решений");

                    int[] baseVar = FindBaseVariables(rank);
                    bool[] free = new bool[vector.Length];

                    stringBuilder.Append("Базисные переменные: ");
                    Console.Write("Базисные переменные: ");

                    for (int i = 0; i < baseVar.Length; i++)
                        {
                            stringBuilder.Append($"x{baseVar[i] + 1} ");
                            Console.Write($"x{baseVar[i] + 1} ");
                        }
                    stringBuilder.Append("\n");
                    Console.WriteLine();

                    stringBuilder.Append("Свободные переменные: ");
                    Console.Write("Свободные переменные: ");

                    for (int i = 0; i < vector.Length; i++)
                    {
                        int count = 0;

                        for (int j = 0; j < baseVar.Length; j++)
                        {
                            if (i != baseVar[j])
                            {
                                count++;
                            }
                        }

                        if (count == baseVar.Length)
                            free[i] = true;
                    }

                    for (int i = 0; i < vector.Length; i++)
                    {
                        if (free[i])
                        {
                            stringBuilder.Append($"x{i + 1} = 1");
                            Console.Write($"x{i + 1} = 1");
                            vector[i] = 1;
                        }
                    }

                    stringBuilder.Append("\n");
                    Console.WriteLine();

                    int new_rows = _rows;

                    for (int i = 0; i < _rows; i++)
                    {
                        int count = 0;

                        for (int j = 0; j < _columns - 1; j++)
                        {
                            if (_matrix[i, j] == 0) count++;
                        }

                        if (count == _columns-1) new_rows--;
                    }

                    double[] sum = new double[new_rows];

                    for (int n = new_rows - 1; n >= 0; n--)
                    {
                        for (int i = n + 1; i < _columns - 1; i++)
                            sum[n] += vector[i] * _matrix[n, i];
                    }

                    for (int n = new_rows - 1; n >= 0; n--)
                    {
                        if (!free[n])
                        {
                            if (_matrix[n, n] != 0)
                                vector[n] = (_matrix[n, _columns - 1] - sum[n]) / _matrix[n, n];
                            else
                            {
                                for (int i = n - 1; i >= 0; i--)
                                {
                                    sum[i] = 0;
                                    for (int j = 0; j < _columns - 1; j++)
                                        sum[i] += vector[j] * _matrix[i, j];
                                    vector[n] = (_matrix[i, _columns - 1] - sum[i]) / _matrix[i, n];
                                    if (!Double.IsNaN(vector[n]))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                stringBuilder.Append("Ответ: \n");
                Console.WriteLine("Ответ: ");
                for (int i = 0; i < vector.Length; i++)
                {
                    stringBuilder.Append($"x{i + 1} = {vector[i]:0.00}\n");
                    Console.WriteLine($"x{i + 1} = {vector[i]:0.00}");
                }
                stringBuilder.Append("\n");
                Console.WriteLine();
            }

            else
            {
                stringBuilder.Append($"Ранг матрицы системы не равен рангу расширенной матрицы: {Rank(true)} != {Rank(false)}.\nСистема не имеет решений.\n");
                Console.WriteLine($"Ранг матрицы системы не равен рангу расширенной матрицы: {Rank(true)} != {Rank(false)}.\nСистема не имеет решений.\n");
            }

            return stringBuilder.ToString();
        }

    }
}
