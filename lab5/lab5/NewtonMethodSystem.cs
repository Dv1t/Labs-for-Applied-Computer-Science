﻿using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace lab5
{
    class NewtonMethodSystem
    {
        //Данная константа необходима, чтобы цикл в методе Solve 
        //не уходил в некоторых случаях в бесконечный цикл
        private const int _maxCounter = 20;

        private double[,] _prev_vector;
        private double[,] _curr_vector;
        private double[,] _curr_func_value;
        private double[,] _numJac;
        private double[,] _revJacobian;

        #region Properties
        public EquationsSystem Equations { get; set; }
        //Точность вычислений
        public double Eps { get; set; }
        //Словарь начальных значений для каждой переменной
        public Dictionary<string, FloatingPoint> X { get; set; }

        //Якобиан
        public Expr[,] Jacobian { get; set; }

        //Счетчик итераций
        public int Counter { get; set; }

        #endregion

        public NewtonMethodSystem(EquationsSystem eq, Dictionary<string, FloatingPoint> values, double e)
        {
            Equations = eq;
            Eps = e;
            X = values;
            Counter = 0;

            //Все свойства якобиана вычисляются при создании объекта Newton
            Jacobian = new Expr[Equations.Functions.GetLength(0), Equations.Functions.GetLength(1)];

            for (int i = 0; i < Jacobian.GetLength(0); i++)
            {
                for (int j = 0; j < Jacobian.GetLength(1); j++)
                {
                    //Взятие производной из каждой функции
                    if (Equations.Functions[i, j] != null)
                    {
                        Jacobian[i, j] = Equations.Functions[i, j].Differentiate($"x{j + 1}");
                    }
                }
            }
        }

        public void Solve()
        {
            double max;

            //вектор, в который записывается текущее приближение
            _curr_vector = new double[X.Values.Count, 1];

            //вектор, в котором хранится предыдущее приближение
            _prev_vector = new double[X.Values.Count, 1];    

            //Создание временного словаря и присвоение ему начальных значений
            var temp = X;

            do
            {
                //Увеличение счетчика итераций
                Counter++;

                //Составление численного Якобиана из обновленных значений перменных
                _numJac = new double[Jacobian.GetLength(0), Jacobian.GetLength(1)];
                for (int i = 0; i < Jacobian.GetLength(0); i++)
                {
                    for (int j = 0; j < Jacobian.GetLength(1); j++)
                    {
                        //Перевод из FloatingPoint в double
                        if (Jacobian[i, j]!=null)
                            _numJac[i, j] = double.Parse(Jacobian[i, j].Evaluate(temp).RealValue.ToString().Replace(".", ","));
                    }
                }

                //Нахождение обратной матрицы Якобиана
                _revJacobian = Matrix.getInverse(_numJac); ;

                //Обновление матрицы приближенных значений предыдущего шага
                for (int i = 0; i < temp.Count; i++)
                {
                    var elem = temp.ElementAt(i);
                    _prev_vector[i, 0] = double.Parse(elem.Value.RealValue.ToString().Replace(".", ","));
                }

                //Матрица числовых значений начальных функций
                _curr_func_value = new double[Equations.Functions.GetLength(1), 1];

                //Расчет значений для матрицы функций
                for (int i = 0; i < Equations.Functions.GetLength(0); i++)
                {
                    for (int j = 0; j < Equations.Functions.GetLength(1); j++)
                    {
                        _curr_func_value[i, 0] += double.Parse(Equations.Functions[i, j].Evaluate(temp).RealValue.ToString().Replace(".", ","));
                    }
                }

                //Расчет дельта X
                var delta_x = Matrix.Multiply(_revJacobian, _curr_func_value);

                //Получение нового приближения
                _curr_vector = Matrix.Subtract(_prev_vector, delta_x);

                max = Math.Abs(delta_x[0, 0]);

                temp = new Dictionary<string, FloatingPoint>();

                //Вычисление максимального значения в массиве delta_x и обновление словаря temp
                for (int i = 0; i < X.Count; i++)
                {
                    if (max < Math.Abs(delta_x[i, 0]))
                        max = Math.Abs(delta_x[i, 0]);

                    var elem = X.ElementAt(i);
                    temp.Add(elem.Key, Expr.Parse(_curr_vector[i, 0].ToString().Replace(",", ".")).RealNumberValue);
                }

            } while (max > Eps && Counter != _maxCounter);

            Console.WriteLine($"Итоговое количество итераций: {Counter}");

            Console.WriteLine("Ответ: ");

            Matrix.Print(_curr_vector);

            Console.WriteLine("\n");
        }
    }
}
