using System;
using Expr = MathNet.Symbolics.SymbolicExpression;


namespace lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            //промежуток и шаг
            const float a = 0;
            const float b = 1;
            const double h = 0.1;

            //Считывание функции и её компиляция средствами библиотеки MathNet.Symbolics
            Console.WriteLine("Диффиренциальное уравнение должно иметь вид: y'=f(x,y)");
            Console.WriteLine("Введите f(x,y)");
            string equation = Console.ReadLine();
            Func<double, double, double> function = Expr.Parse(equation).Compile("x", "y");

            //Ввод начальных условий
            Console.WriteLine("Введите начальные условия");
            double x0, y0;
            Console.Write("x0=");
            String temp = Console.ReadLine();
            x0 = float.Parse(temp);
            Console.Write("y0=");
            temp = Console.ReadLine();
            y0 = float.Parse(temp);

            //определение колличества шагов алгоритма
            int numberOfSteps =(int) ((b-a)/h);

            //массив искомых значений
            double[] values = new double[numberOfSteps];

            //алгоритм Рунге–Кутты 4–го порядка
            for (int i=0;i<numberOfSteps;i++)
            {
                double prevX = (i  * h)+x0;
                double prevY;
                if (i == 0)
                    prevY = y0;
                else
                    prevY = values[i - 1];

                double k1 = function(prevX, prevY);
                double k2 = function(prevX + h / 2, prevY + h * k1 / 2);
                double k3 = function(prevX + h / 2, prevY + h * k2 / 2);
                double k4 = function(prevX + h, prevY + h * k3);

                values[i] = prevY + (h / 6 * (k1+2*k2+2*k3+k4));
            }

            //Вывод ответа
            Console.WriteLine($"x={x0}  y={y0}");
            for (int i=0;i<numberOfSteps;i++)
            {
                Console.WriteLine($"x={(i+1)*h+x0}  y={values[i]}");
            }

            Console.ReadLine();
        }
    }
}
