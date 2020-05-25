using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9._1
{

    /*
    dU             1      dU       t
   ---- = 3(1.1 - ---x) ------- + e + 1
    dt             2     dx*dx

    U(0,t) = 0
    U(1,t) = 0
    U(x,0) = 0.01(1-x)x = 0.01*x - 0.01*x*x
    */

    class Program
    {
        //Функция возвращающая коэффициент теплопроводности (сигма)
        private static double Sigma(double t, double x)
        {
            return (3.0 * (1.1 - 0.5 * x));
        }

        //------------------------------------------------------------------
        //Возмущающий член в правой части ДУЧПа
        static double Funcs(double t, double x)
        {
            return (Math.Exp(t) - 1.0);
        }

        //------------------------------------------------------------------
        //Функция возвращающая левое граничное условие
        static double LeftBC(double t, double x)
        {
            return 0.0;
        }

        //------------------------------------------------------------------
        //Функция возвращающая правое граничное условие
        static double RightBC(double t, double x)
        {
            return 0.0;
        }

        //------------------------------------------------------------------
        static void Main(string[] args)
        {
            double length = 1.0, //Протяженность пространства
                time, //Время расчета
                precision = 1.0, //0.0001,//Точность расчета
                dx, //Шаг по пространству
                dt, //Шаг по времени
                A; //Отношение dt/(dx*dx)

            int nX = 10, //Число точек разбиения по пространству
                nT = 30; //Число точек разбиения по времени
            //Расчет шага по пространству
            dx = length / (double)(nX - 1);
            //Расчет шага по времени для достижения устойчивости схемы
            double Max = 0.0, s;
            for (int x = 0; x < nX; x++)
            {
                s = Sigma(0, dx * x);
                Max = (Max > s) ? Max : s;
            }

            dt = dx * dx * precision / (2.0 * Max);
            A = dt / (dx * dx);
            time = 20.0 * dt; //1.0/pow(3.3,0.5);//0.000004;//1.0/Max;
            nT = (int)(time / dt + 1.0);
            //Расчетный массив U[time][x]
            double[,] U = new double[nT, nX];
            //Инициализация начальных условий
            for (int x = 0; x < nX; x++)
                U[0, x] = 0.01 * (1.0 - dx * x) * dx * x;
            //Граничные условия для стартового временного слоя
            U[0, 0] = LeftBC(0, 0);
            U[0, nX - 1] = RightBC(0, dt * (nX - 1));
            //Вспомогательные переменные
            double S, F, T, X;
            //Цикл расчета по времени
            for (int t = 0; t < nT - 1; t++)
            {
                //Цикл расчета по пространству
                for (int x = 0; x < nX; x++)
                {
                    T = dt * t;
                    X = dx * x;
                    S = Sigma(T, X);
                    F = Funcs(T, X);
                    //Если на левой границе
                    if (x == 0)
                    {
                        U[t + 1, x] = LeftBC(T, X);
                        //A*S*(LeftBC(T,X) + U[t][x+1]) + (1.0 - 2.0*A*S)*U[t][x] + dt*F;
                    } //else//Если на правой границе

                    if (x == nX - 1)
                    {
                        U[t + 1, x] = RightBC(T, X);
                        //A*S*(U[t][x-1] + RightBC(T,X)) + (1.0 - 2.0*A*S)*U[t][x] + dt*F;
                    } // else {//Если в промежуточных точках

                    if (x > 0 && x < nX - 1)
                    {
                        U[t + 1, x] = A * S * (U[t, x - 1] + U[t, x + 1]) + (1.0 - 2.0 * A * S) * U[t, x] + dt * F;
                    }
                } //Конец цикла расчета по пространству
            }
            //Вывод результата
            Console.WriteLine($"nX =  {nX}");
            Console.WriteLine($"nT = {nT}");
            Console.WriteLine($"dt = {dt}");
            Console.WriteLine($"time = {time}");
            Console.WriteLine($"A = {A}");
            Console.WriteLine();

            int xx = (int)(0.6 * (nX - 1) / length);
            Console.WriteLine("U = U(0.6,t)");
            for (int t = 0; t < nT; t++)
            {
                Console.WriteLine($"{dt * t} t {U[t, xx]}");
            }
            Console.WriteLine();

            double tt = time / 10.0;

            xx = (int)(tt * (nT - 1) * 1 / time);
            Console.WriteLine($"U = U(x,{tt})");
            for (int x = 0; x < nX; x++)
            {
                Console.WriteLine($"{dx * x} t {U[xx, x]}");
            }
            Console.WriteLine();


            xx = (int)(tt * (nT - 1) * 2 / time);
            Console.WriteLine($"U = U(x,{tt * 2}");

            for (int x = 0; x < nX; x++)
            {
                Console.WriteLine($"{dx * x} t {U[xx, x]}");
            }
            Console.WriteLine();

            xx = (int)(tt * (nT - 1) * 4 / time);
            Console.WriteLine($"U = U(x,{tt * 4})");
            for (int x = 0; x < nX; x++)
            {
                Console.WriteLine($"{dx * x} t {U[xx, x]}");
            }
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
