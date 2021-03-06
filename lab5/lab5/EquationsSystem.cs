﻿using System;
using Expr = MathNet.Symbolics.SymbolicExpression;

namespace lab5
{
    class EquationsSystem
    {
        public Expr[,] Functions { get; set; }

        public EquationsSystem(Expr[,] expr)
        {
            Functions = expr;
        }

        public void Print()
        {
            for (int i = 0; i < Functions.GetLength(0); i++)
            {
                for (int j = 0; j < Functions.GetLength(1); j++)
                {
                    if(j != Functions.GetLength(1) - 1)
                        Console.Write($"({Functions[i, j]}) + ");
                    else
                        Console.Write($"({Functions[i, j]})");
                }

                Console.WriteLine(" = 0");
            }

            Console.WriteLine();
        }

    }
}
