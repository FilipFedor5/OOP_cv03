﻿using System;

namespace cv3
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Matrix matA = new Matrix(new double[,] { { 1, 2 }, { 3, 5} });
            Matrix matB = new Matrix(new double[,] { { -4, -3 }, { 2, 1 } });

            Matrix.ToString(matA + matB);
            Matrix.ToString(matA - matB);
            Matrix.ToString(matA * matB);

            Console.WriteLine("matrices are equal = {0}",matA == matB); 

            Matrix.ToString(matA);
            
        }
    }
}
