using System;
using System.Collections.Generic;
using Expr = MathNet.Symbolics.SymbolicExpression;


namespace lab7
{
    class Program
    {
        static void Main()
        {
            //промежуток и шаг
            const float a = 0;
            const float b = 1;
            const double h = 0.1;
            Console.WriteLine("Введите колличесво уравнений в системе");
            int n = int.Parse(Console.ReadLine());
            List<Func<double, double, double, double>> functions = new List<Func<double, double, double, double>>();
            for (int i = 0; i < n; i++)
            {
                //Считывание функции и её компиляция средствами библиотеки MathNet.Symbolics
                Console.WriteLine("Диффиренциальное уравнение должно иметь вид: y'=f(x,y,z)");
                Console.WriteLine("Введите f(x,y,z)");
                string equation = Console.ReadLine();
                Func<double, double, double, double> function = Expr.Parse(equation).Compile("x", "y","z");
                functions.Add(function);
            }

            //Ввод начальных условий
            Console.WriteLine("Введите начальные условия");
            double x0, y0,z0;
            Console.Write("x0=");
            String temp = Console.ReadLine();
            x0 = float.Parse(temp);
            Console.Write("y0=");
            temp = Console.ReadLine();
            y0 = float.Parse(temp);
            Console.Write("z0=");
            temp = Console.ReadLine();
            z0 = float.Parse(temp);

            //определение колличества шагов алгоритма
            int numberOfSteps =(int) ((b-a)/h);

            //массив искомых значений
            double[] valuesY = new double[numberOfSteps];
            double[] valuesZ = new double[numberOfSteps];

            //алгоритм Рунге–Кутты 4–го порядка
            for (int i=0;i<numberOfSteps;i++)
            {
                double prevX = (i  * h)+x0;
                double prevY;
                double prevZ;
                if (i == 0)
                {
                    prevY = y0;
                    prevZ = z0;
                }
                else
                {
                    prevY = valuesY[i - 1];
                    prevZ = valuesZ[i - 1];
                }
                
                double k1 = functions[0](prevX, prevY,prevZ);
                double k2 = functions[0](prevX + h / 2, prevY + h * k1 / 2, prevZ + h * k1 / 2);
                double k3 = functions[0](prevX + h / 2, prevY + h * k2 / 2, prevZ + h * k2 / 2);
                double k4 = functions[0](prevX + h, prevY + h * k3, prevZ + h * k3);
                valuesY[i] = prevY + (h / 6 * (k1+2*k2+2*k3+k4));
                k1 = functions[1](prevX, prevY, prevZ);
                k2 = functions[1](prevX + h / 2, prevY + h * k1 / 2, prevZ + h * k1 / 2);
                k3 = functions[1](prevX + h / 2, prevY + h * k2 / 2, prevZ + h * k2 / 2);
                k4 = functions[1](prevX + h, prevY + h * k3, prevZ + h * k3);
                valuesZ[i] = prevZ+ (h / 6 * (k1 + 2 * k2 + 2 * k3 + k4));
            }

            //Вывод ответа
            Console.WriteLine($"x={x0}  y={y0}");
            for (int i=0;i<numberOfSteps;i++)
            {
                Console.WriteLine($"x={(i+1)*h+x0}  y={valuesY[i]} z={valuesZ[i]}");
            }

            Console.ReadLine();
        }
    }
}
