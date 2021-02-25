using System;
using System.Collections.Generic;
using System.Text;

namespace cv3
{
    class Matrix
    {
        //constant for determining precision because of the use of double in matrices
        public static double Epsilon = 0.000001;
        public double[,] Values
        {
            get;
            set;
        }
        public Matrix(double[,] values)
        {
            Values = values;
        }

        static bool AreSameSize(Matrix matA, Matrix matB)
        {
            //argument of getlength 0 for number of rows, 1 for number of collums
            if (matA.Values.GetLength(0) == matB.Values.GetLength(0))
            {
                if (matA.Values.GetLength(1) == matB.Values.GetLength(1)) return true;
            }
            return false;
        }

        static bool IsSquare(Matrix matA)
        {
            if (matA.Values.GetLength(0) == matA.Values.GetLength(1)) return true;
            return false;
        }

        public static Matrix operator +(Matrix matA, Matrix matB)
        {
            if (!Matrix.AreSameSize(matA, matB)) throw new ArgumentException("matrices arent of the same dimensions");

            for (int row = 0; row < matA.Values.GetLength(0); row++)
            {
                for (int col = 0; col < matA.Values.GetLength(1); col++)
                {
                    matA.Values[row, col] = matA.Values[row, col] + matB.Values[row, col];
                }
            }
            return matA;
        }

        public static Matrix operator -(Matrix matA, Matrix matB)
        {
            if (!Matrix.AreSameSize(matA, matB)) throw new ArgumentException("matrices arent of the same dimensions");

            for (int row = 0; row < matA.Values.GetLength(0); row++)
            {
                for (int col = 0; col < matA.Values.GetLength(1); col++)
                {
                    matA.Values[row, col] = matA.Values[row, col] - matB.Values[row, col];
                }
            }
            return matA;
        }
        //unar minus
        public static Matrix operator -(Matrix matA)
        {
            for (int row = 0; row < matA.Values.GetLength(0); row++)
            {
                for (int col = 0; col < matA.Values.GetLength(1); col++)
                {
                    matA.Values[row, col] = -matA.Values[row, col];
                }
            }
            return matA;
        }

        //for boolean operators using contant epsilon to determine accuracy, for double can be rounded
        public static bool operator ==(Matrix matA, Matrix matB)
        {
            if (!Matrix.AreSameSize(matA, matB)) throw new ArgumentException("matrices arent of the same dimensions");

            for (int row = 0; row < matA.Values.GetLength(0); row++)
            {
                for (int col = 0; col < matA.Values.GetLength(1); col++)
                {
                    if (matA.Values[row, col] - matB.Values[row, col] > Matrix.Epsilon) return false;
                }
            }
            return true;
        }

        public static bool operator !=(Matrix matA, Matrix matB)
        {
            if (!Matrix.AreSameSize(matA, matB)) throw new ArgumentException("matrices arent of the same dimensions");

            for (int row = 0; row < matA.Values.GetLength(0); row++)
            {
                for (int col = 0; col < matA.Values.GetLength(1); col++)
                {
                    if (matA.Values[row, col] - matB.Values[row, col] > Matrix.Epsilon) return true;
                }
            }
            return false;
        }
        
        public static void ToString(Matrix matA)
        {
            for (int row = 0; row < matA.Values.GetLength(0); row++)
            {
                for (int col = 0; col < matA.Values.GetLength(1); col++)
                {
                    Console.Write(string.Format("{0:0.##}  ", matA.Values[row, col]));
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }




        //argument of getlength 0 for number of rows, 1 for number of collums
        public static Matrix operator *(Matrix matA, Matrix matB)
        {
            //You can only multiply matrices if the number of columns of the first matrix is the same as the number of rows as the second matrix.

            if (matA.Values.GetLength(1) != matB.Values.GetLength(0)) throw new ArgumentException("matrices arent compatible for multiplication");


            double[,] matC = new double[matA.Values.GetLength(0), matB.Values.GetLength(1)];

            for (int row = 0; row < matC.GetLength(0); row++)
            {
                for (int col = 0; col < matC.GetLength(1); col++)
                {
                    //firs two for loops acces the element of matrix , the third do the multiplication
                    matC[row, col] = 0;
                    
                    for (int colA = 0; colA < matA.Values.GetLength(1); colA++)
                    {
                        matC[row, col] = matC[row, col] + matA.Values[row, colA] * matB.Values[colA, col];
                    }
                }
            }



            return new Matrix(matC);
        }

        public static double Determinat(Matrix matA)
        {
            if (!Matrix.IsSquare(matA)) throw new ArgumentException("you need square matrix to return determinant");

            if (matA.Values.GetLength(0) > 3 && matA.Values.GetLength(1) > 3) throw new ArgumentException("the size of matrix need to be max 3x3 ");


            switch (matA.Values.GetLength(0))
            {
                case 1: return matA.Values[0, 0];

                case 2: return matA.Values[0, 0] * matA.Values[1, 1] - matA.Values[1, 0] * matA.Values[0, 1];

                case 3:
                    return  matA.Values[0, 0] * matA.Values[1, 1] * matA.Values[2, 2] +
                            matA.Values[0, 1] * matA.Values[1, 2] * matA.Values[2, 0] +
                            matA.Values[0, 2] * matA.Values[1, 0] * matA.Values[2, 1] -

                            matA.Values[0, 2] * matA.Values[1, 1] * matA.Values[2, 0] -
                            matA.Values[0, 0] * matA.Values[1, 2] * matA.Values[2, 1] -
                            matA.Values[0, 1] * matA.Values[1, 0] * matA.Values[2, 2];
            }

            throw new ArgumentException("matrix has 0 dimensions");
        }
    }
}
