using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MatrixLibrary;
namespace UnitTests
{
    [TestClass]
    public class Matrix_Test
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Multiply_exception_Matrix()
        {
            Matrix_DeT<double> resultmatrix_one;
            Matrix_DeT<double> matrix_one = new Matrix_DeT<double>(2, 8);
            Matrix_DeT<double> matrix_two = new Matrix_DeT<double>(2, 8);
            resultmatrix_one = matrix_one * matrix_two;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Plus_exception_Matrix()
        {
            Matrix_DeT<double> resultmatrix_one;
            Matrix_DeT<double> matrix_one = new Matrix_DeT<double>(6, 6);
            Matrix_DeT<double> matrix_two = new Matrix_DeT<double>(8, 8);
            resultmatrix_one = matrix_one + matrix_two;
        }

        [TestMethod]
        public void Expected_Plus_Result()
        {
            const int size = 6;
            dynamic[,] realResult = new dynamic[size, size] { { 0, 2, 4, 6, 8, 10 }, { 0, 2, 4, 6, 8, 10 }, { 0, 2, 4, 6, 8, 10 }, { 0, 2, 4, 6, 8, 10 }, { 0, 2, 4, 6, 8, 10 }, { 0, 2, 4, 6, 8, 10 } };

            Matrix_DeT<double> matrix_one = new Matrix_DeT<double>(size, size);
            Matrix_DeT<double> matrix_two = new Matrix_DeT<double>(size, size);
            Matrix_DeT<double> resultmatrix_one;
            dynamic[,] resultmatrix_two = new dynamic[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix_one[j, i] = i;
                    matrix_two[j, i] = i;
                }
            }

            resultmatrix_one = matrix_one + matrix_two;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    resultmatrix_two[j, i] = resultmatrix_one[j, i];
                }
            }

            CollectionAssert.AreEqual(realResult, resultmatrix_two);
        }

        [TestMethod]
        public void Expected_Multiply_Result()
        {
            const int size = 5;
            dynamic[,] realResult = new dynamic[size, size] { { 0, 10, 20, 30, 40 }, { 0, 10, 20, 30, 40 }, { 0, 10, 20, 30, 40 }, { 0, 10, 20, 30, 40 }, { 0, 10, 20, 30, 40 } };

            Matrix_DeT<double> matrix_one = new Matrix_DeT<double>(size, size);
            Matrix_DeT<double> matrix_two = new Matrix_DeT<double>(size, size);
            Matrix_DeT<double> resultmatrix_one;
            dynamic[,] resultmatrix_two = new dynamic[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix_one[j, i] = i;
                    matrix_two[j, i] = i;
                }
            }

            resultmatrix_one = matrix_one * matrix_two;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    resultmatrix_two[j, i] = resultmatrix_one[j, i];
                }
            }

            CollectionAssert.AreEqual(realResult, resultmatrix_two);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Matrix_Error_0()
        {
            int x = 0;
            int y = 0;
            Matrix_DeT<double> matrix = new Matrix_DeT<double>(x, y);
        }
    }
}

 