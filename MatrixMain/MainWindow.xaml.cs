using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MatrixLibrary;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace MatrixSolution
{
    //delegate int N_Fync(int i, int j, Random rnd);//указатель на метод(делегат)
   // public delegate void Action(int i, int j, Random rnd);

    public partial class MainWindow : Window
    {
       
        Matrix_DeT<double> numbersMatrix_First;
        Matrix_DeT<double> numbersMatrix_Second;
        Matrix_DeT<double> numbersResult;

        public MainWindow()
        {
            InitializeComponent();
        }

        int priznak = 0;
        private void btCalculate_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }
        
        private void btCreate_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "";
            int x = Convert.ToInt32(matrixSizeX.Text);
            int y = Convert.ToInt32(matrixSizeY.Text);

            GetRandomMatrix(x, y);
            DrawMatrix();
        }

        ///------------------------------------------------------------------------------------------------------------------------
        // сохранение результата
        private void btSave_Click(object sender, RoutedEventArgs e)
        {
            CreateFile();
        }

        private void CreateFile()
        {
            Microsoft.Win32.SaveFileDialog sv = new Microsoft.Win32.SaveFileDialog { Filter = "CSV|*.csv|Text Documents|*.txt" };
            sv.ShowDialog();

            if (!string.IsNullOrEmpty(sv.FileName))
            {
                using (StreamWriter sw = new StreamWriter(sv.FileName, false, Encoding.Unicode, 10485760))
                {
                    sw.Write(Clipboard.GetData(TextDataFormat.UnicodeText.ToString()));
                }

                string resultText = "";

                for (int i = 0; i < numbersResult.ySize; i++)
                {
                    for (int j = 0; j < numbersResult.xSize; j++)
                    {
                        resultText += Convert.ToString(numbersResult[j, i]) + ";" ;
                    }
                    resultText += "\n";
                }

                File.WriteAllText(sv.FileName, resultText);
            }
        }
        ///------------------------------------------------------------------------------------------------------------------------
        ///
        private void GetRandomMatrix(int x, int y)
        {
   
            numbersMatrix_First = Matrix_DeT<double>.GenerateMatrix(x, y, (x1, y1, rnd) => rnd.Next(-100, 100) + x1 - y1);
            Thread.Sleep(5);
            numbersMatrix_Second = Matrix_DeT<double>.GenerateMatrix(x, y, (x1, y1, rnd) => rnd.Next(-100, 100) + x1 - y1);
            numbersResult = new Matrix_DeT<double>(x, y);

        }


        /// расчёт
        private void Calculate()
        {
            if (priznak != 1) throw new Exception("Необходимо создать матрицу!");

            DateTime timeStart = DateTime.Now;
            this.Cursor = Cursors.Wait;

            switch (matrixMethod.SelectedIndex)
            {
                case 0:
                    numbersResult = numbersMatrix_First + numbersMatrix_Second;
                    break;
                case 1:
                    numbersResult = numbersMatrix_First * numbersMatrix_Second;
                    break;
                default:
                    numbersResult = numbersMatrix_First + numbersMatrix_Second;
                    break;
            }

            DateTime timeStop = DateTime.Now;     
            
            tbResult.Text = Convert.ToString((timeStop - timeStart).TotalMilliseconds);
            DrawResult();
            numbersResult.WriteMatrix();

            this.Cursor = Cursors.Arrow;
        }


        /// отрисовка
        private void DrawMatrix()
        {
            string matrixText1 = "";
            string matrixText2 = "";

            for (int i = 0; i < numbersMatrix_First.ySize; i++)
            {
                for (int j = 0; j < numbersMatrix_First.xSize; j++)
                {
                    matrixText1 += Convert.ToString(numbersMatrix_First[j, i]) + "\t";
                    matrixText2 += Convert.ToString(numbersMatrix_Second[j, i]) + "\t";
                }

                matrixText1 += "\n";
                matrixText2 += "\n";
            }

            matrix1.Text = matrixText1;
            matrix2.Text = matrixText2;

            priznak = 1;
        }
        //вывод результата
        private void DrawResult()
        {
            string resultText = "";

            for (int i = 0; i < numbersResult.ySize; i++)
            {
                for (int j = 0; j < numbersResult.xSize; j++)
                {
                    resultText += Convert.ToString(numbersResult[j, i]) + "\t";
                }

                resultText += "\n";
            }

            result.Text = resultText;
        }

        private void tbResult_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void matrixSizeX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
