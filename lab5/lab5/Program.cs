using System;
using System.Collections.Generic;
using MathNet.Symbolics;
using Expr = MathNet.Symbolics.SymbolicExpression;


namespace lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            //Используемые переменныые
            var x1 = Expr.Variable("x1");
            var x2 = Expr.Variable("x2");
            var x3 = Expr.Variable("x3");
            var x4 = Expr.Variable("x4");
            var x5 = Expr.Variable("x5");
            var x6 = Expr.Variable("x6");
            var x7 = Expr.Variable("x7");
            var x8 = Expr.Variable("x8");
            var x9 = Expr.Variable("x9");
            var x10 = Expr.Variable("x10");


            //Пример 1
            var values1 = new Dictionary<string, FloatingPoint>
            {
                { "x1", 0.9},
                { "x2", 0.5} 
            };

            var f1 = (x1 * x1);
            var f2 = (x2 * x2 - 1);
            var f3 = (x1 * x1 * x1);
            var f4 = (- x2);

            Expr[,] functions1 = new Expr[,] { { f1, f2 }, { f3, f4 } };

            var system1 = new EquationsSystem(functions1);

            system1.Print();

            var newton1 = new NewtonMethodSystem(system1, values1, 0.0001);

            newton1.Solve();

            //Пример 2
            var values2 = new Dictionary<string, FloatingPoint>
            {
                {"x1", 0.5 },
                {"x2", 0.5 },
                {"x3", 0.5 }
            };

            f1 = (x1 * x1);
            f2 = (x2 * x2);
            f3 = (x3 * x3 - 1);
            f4 = (2 * x1 * x1);
            var f5 = f2;
            var f6 = (-4 * x3);
            var f7 = (3 * x1 * x1);
            var f8 = (-4 * x2);
            var f9 = (x3 * x3);

            Expr[,] functions2 = new Expr[,] { { f1, f2, f3 }, { f4, f5, f6 }, {f7, f8, f9 } };

            var system2 = new EquationsSystem(functions2);

            system2.Print();

            var newton2 = new NewtonMethodSystem(system2, values2, 0.0001);

            newton2.Solve();


            Console.WriteLine("Введите уравнения системы, =0 в конце вводить не нужно");
            Console.WriteLine("По окончанию ввода введите 0");
            List<string> equations = new List<string>();
            while(true)
            {
                string temp = Console.ReadLine();
                if (temp == "0")
                    break;
                equations.Add(temp);
            }
            Expr[,] functions3 = new Expr[equations.Count, equations.Count];
            int i = 0;
            foreach(string equation in equations)
            {
                Expr temp = Expr.Parse(equation);
                List<Expr> expression = new List<Expr>();
                for (int j = 0; j < temp.NumberOfOperands; j++)
                {
                    expression.Add(temp[j]);
                }
                if(temp.NumberOfOperands>equations.Count)
                {
                   expression[0] = temp[0] + temp[1];
                    expression.RemoveAt(1);
                }
                for(int j=0;j < equations.Count; j++)
                {
                    functions3[i, j] = expression[j];
                }
                i++;
            }

            var system3 = new EquationsSystem(functions3);

            Console.WriteLine("Введите изначальное приближение");
            var values3 = new Dictionary<string, FloatingPoint>(equations.Count);
            for(int j=0;j<equations.Count;j++)
            {
                Console.Write($"x{j+1}=");
                String[] temp = Console.ReadLine().Split('=');
                FloatingPoint floatingPoint = float.Parse( temp[0]);
                values3.Add($"x{j + 1}", floatingPoint);
            }

            var newton3 = new NewtonMethodSystem(system3, values3, 0.0001);

            newton3.Solve();

            Console.ReadLine();
        }
    }
}
