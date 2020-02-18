using System;

namespace lab1
{
    class Program
    {
        public static void Main()
        {
            Matrix m1;
            Matrix m2;
            Console.WriteLine("Ввод первой матрицы...");
            try
            {
               m1 = new Matrix();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Main();
                return;
            }
            float d = m1.GetDeterminant(m1);
            Console.WriteLine("Ввод второй матрицы...");
            try
            {
                m2 = new Matrix();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Main();
                return;
            }
            Matrix m3 = m1.Add(m2);
            Matrix m4 = m1.Multiply(m2);
            Matrix mr = m1.GetOpposite();
            Console.WriteLine($"Первая матрица \n{m1.ToString()}");
            Console.WriteLine($"Детерминант первой матрицы {d}");
            Console.WriteLine($"Обратная к первой матрица\n{mr.ToString()}");
            Console.WriteLine($"Вторая матрица \n{m2.ToString()}");
            Console.WriteLine($"Сумма первой и второй матриц \n{m3.ToString()}");
            Console.WriteLine($"Произведение первой и второй матриц \n{m4.ToString()}");
            System.Console.ReadLine();
        } 
    }
}
