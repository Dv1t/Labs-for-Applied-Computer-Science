using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    class Matrix
    {
        #region Properties
        public int Size { get { return _n; } }
        private int _n;
        public List<List<float>> Value { get { return _matrix; } }
        private List<List<float>> _matrix;
        #endregion 

        #region Constructors
        public Matrix()
        {
            Console.WriteLine("Введите размер матрицы");
            string size = System.Console.ReadLine();
            
                if (!int.TryParse(size, out _n))
                {
                    throw new Exception("Ошибка ввода");
                }
                if (_n<2)
                {
                    throw new Exception("Минимальный размер матрицы - 2");
                }

                if (_n > 100)
                {
                    throw new Exception("Введите размер матрицы поменьше");
                }
            _matrix = new List<List<float>>(_n);
            for (int i = 0; i < _n; i++)
            {
                List<float> line = new List<float>(_n);
                for (int j = 0; j < _n; j++)
                {
                    Console.WriteLine($"Введите {j + 1} элемент {i + 1} строки");
                    float element;
                    string input = System.Console.ReadLine();
                    if (!float.TryParse(input, out element))
                    {
                        throw new Exception("Ошибка ввода");
                    }
                    line.Add(element);
                }
                _matrix.Add(line);
            }
        }

        public Matrix(int size, List<List<float>> matrix)
        {
            bool check = true;
            if (size == matrix.Count)
            {

                foreach (List<float> line in matrix)
                {
                    if (line.Count != size)
                    {
                        check = false;
                    }
                }
            }
            else
            {
                check = false;
            }
            if (!check)
            {
                Console.WriteLine("Неверный формат записи матрицы");
                return;
            }
            _n = size;
            _matrix = matrix;
        }

        #endregion

        #region Methods
        override
        public string ToString()
        {
            string result;
            StringBuilder stringBuilder = new StringBuilder();
            foreach(List<float>line in _matrix)
            {
                foreach(float element in line)
                {
                    stringBuilder.Append(element);
                    stringBuilder.Append(" ");
                }
                stringBuilder.Append("\n");
            }
            result = stringBuilder.ToString();
            return result;
        }

        public Matrix Multiply(Matrix m)
        {
            Matrix result;
            if(_n!=m.Size)
            {
                Console.WriteLine("Разный размер умножаемых матриц");
                return this;
            }
            List<List<float>> table = new List<List<float>>(_n);
            for(int i=0;i<_n;i++)
            {
                List<float> line = new List<float>(_n);
                for (int j = 0; j < _n; j++)
                {
                    float elem = 0;
                    for (int k = 0; k < _n; k++)
                    {
                        elem+= _matrix[i][k] * m.Value[k][j];
                    }
                    line.Add(elem);
                }
                table.Add(line);
            }
            result = new Matrix(_n, table);
            return result;
        }
        public Matrix Add(Matrix m)
        {
            Matrix result;
            if (_n != m.Size)
            {
                Console.WriteLine("Разный размер складываемых матриц");
                return this;
            }
            List<List<float>> table = new List<List<float>>(_n);
            for (int i = 0; i < _n; i++)
            {
                List<float> line = new List<float>(_n);
                for (int j = 0; j < _n; j++)
                {
                    float elem = _matrix[i][j] + m.Value[i][j];
                    line.Add(elem);
                }
                table.Add(line);
            }
            result = new Matrix(_n, table);
            return result;
        }
        public float GetDeterminant(Matrix m)
        {
            float det=0;
            if(m.Size == 1)
            {
                return m.Value[0][0];
            }
            if (m.Size>2)
            {
                for (int k = 0; k < m.Size; k++)
                {
                    List<List<float>> addition = new List<List<float>>(m.Size - 1);
                    for (int i = 1; i < m.Size; i++)
                    {
                        List<float> line = new List<float>(m.Size - 1);
                        for (int j = 0; j < m.Size; j++)
                        {
                            if (j == k)
                                continue;
                            line.Add(m.Value[i][j]);
                        }
                        addition.Add(line);
                    }
                    det += (float)Math.Pow(-1, k ) * m.Value[0][k] * GetDeterminant(new Matrix(m.Size-1,addition));
                }
                return det;
                        
            }
            else
            {
                det = m.Value[0][0] * m.Value[1][1] - m.Value[0][1] * m.Value[1][0];
                return det;
            }
        }
        public Matrix GetOpposite()
        {
            Matrix result;
            float det = GetDeterminant(this);
            if(det == 0)
            {
                Console.WriteLine("Детерминант равен 0, обратной матрицы не существует");
                return this;
            }
            List<List<float>> table = new List<List<float>>(_n);
            for (int k = 0; k < this.Size; k++)
            {
                List<float> mainLine = new List<float>(_n);
                for (int s = 0; s < this.Size; s++)
                {
                    List<List<float>> addition = new List<List<float>>(this.Size - 1);
                    for (int i = 0; i < this.Size; i++)
                    {
                        if (i == s)
                            continue;
                        List<float> line = new List<float>(this.Size - 1);
                        for (int j = 0; j < this.Size; j++)
                        {
                            if (j == k)
                                continue;
                            line.Add(this.Value[i][j]);
                        }
                        addition.Add(line);
                    }
                    mainLine.Add((float)Math.Pow(-1, k +s)*GetDeterminant(new Matrix(this.Size - 1, addition)));
                }
                table.Add(mainLine);
            }
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    table[i][j] = table[i][j]/det;
                }
            }
            result = new Matrix(_n, table);
            return result;
        }
        #endregion
    }
}
