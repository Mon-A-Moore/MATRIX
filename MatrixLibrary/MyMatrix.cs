using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLibrary
{
    public class Matrix_DeT<T>//обобщенный тип
    {
        public dynamic[,] numbers;
        public int xSize;
        public int ySize;

        public Matrix_DeT(int x, int y)
        {
            if (x == 0 || y == 0) throw new Exception("Матрицы не могут быть нулевые");
            xSize = x;
            ySize = y;
            numbers = new dynamic[xSize, ySize];
        }

        public dynamic this[int i, int j]//индексатор
        {
            get
            {
                return numbers[i, j];
            }
            set
            {
                numbers[i, j] = value;
            }
        }

        public static Matrix_DeT<T> GenerateMatrix(int x, int y, Func<int, int, Random, T> f)
        {
            Matrix_DeT<T> newMatrix = new Matrix_DeT<T>(x, y);
            Random rnd = new Random();

            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                {
                    newMatrix[j, i] = f(j, i, rnd);
                }

            return newMatrix;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        //перегрузка
        public static Matrix_DeT<T> operator +(Matrix_DeT<T> matrix_one, Matrix_DeT<T> matrix_two)
        {
            if ((matrix_one.xSize == 0  || matrix_two.xSize == 0) || (matrix_one.ySize == 0 || matrix_two.ySize==0)) throw new Exception("Матрицы не могут быть нулевые");
            if (matrix_one.xSize != matrix_two.xSize || matrix_one.ySize != matrix_two.ySize) throw new Exception("Матрицы не сложить");

            Matrix_DeT<T> matrix_result = new Matrix_DeT<T>(matrix_one.xSize, matrix_two.ySize);

            for (int i = 0; i < matrix_one.xSize; i++)
            {
                for (int j = 0; j < matrix_two.ySize; j++)
                {
                    matrix_result[i, j] = matrix_one[i, j] + matrix_two[i, j];
                }
            }

            return matrix_result;
        }

        public static Matrix_DeT<T> operator *(Matrix_DeT<T> matrix_one, Matrix_DeT<T> matrix_two)
        {
            if ((matrix_one.xSize == 0 || matrix_two.xSize == 0) || (matrix_one.ySize == 0 || matrix_two.ySize == 0)) throw new Exception("Матрицы не могут быть нулевые");
            if (matrix_one.xSize != matrix_two.ySize) throw new Exception("Матрицы не перемножить");

            Matrix_DeT<T> matrix_result = new Matrix_DeT<T>(matrix_two.xSize, matrix_one.ySize);

            for (int i = 0; i < matrix_one.ySize; i++)
            {
                for (int j = 0; j < matrix_two.xSize; j++)
                {
                    matrix_result[j, i] = 0;
                    for (int k = 0; k < matrix_two.ySize; k++)
                    {
                        matrix_result[j, i] += matrix_one[k, i] * matrix_two[j, k];
                    }
                }
            }

            return matrix_result;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------

        //вывод 
        public void WriteMatrix()
        {
            for (int i = 0; i < ySize; i++)
            {
                for (int j = 0; j < xSize; j++)
                {
                    Console.Write(numbers[j, i] + "\t");
                }

                Console.WriteLine();
            }
        }
    }
}
