using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3_otpi
{
    public partial class Form1 : Form
    {
        static string[] FromFile = File.ReadAllLines(@"C:\Users\User\Desktop\File.txt", Encoding.Default);
        static int size = FromFile.Length;
        static double[,] P = new double[size, size];
        static double[,] DCT;
        static double[,] DCTT;
        static double[,] PDCT;
        static double[,] QUANT;
        static double[,] PQDCT;


        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < size; i++)
            {
                string[] row = FromFile[i].Split(' ');
                for (int j = 0; j < size; j++)
                    P[i, j] = Convert.ToDouble(row[j]) - 128.0;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.Text = "";
            PDCT = new double[size, size];
            DCT = new double[size, size];
            DCTT = new double[size, size];
            label1.Text += "\nDCT по формуле 5:\n\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == 0)
                        DCT[i, j] = 1.0 / Math.Sqrt(size);
                    else
                        DCT[i, j] = Math.Sqrt(2.0 / size) * Math.Cos((2.0 * j + 1) * i * (3.14 / (2.0 * size)));
                    label1.Text += String.Format("{0,-11} ", Math.Round(DCT[i, j], 6));
                }
                label1.Text += "\n";
            }
            label1.Text += "\nDCT-транспонированная:\n\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    DCTT[i, j] = DCT[j, i];
                    label1.Text += String.Format("{0,-11} ", Math.Round(DCTT[i, j], 6));
                }
                label1.Text += "\n";
            }
            double[,] PDCT1 = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        PDCT1[i, j] += DCT[i, k] * P[k, j];
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        PDCT[i, j] += PDCT1[i, k] * DCTT[k, j];
                    }
                }
            }
            label1.Text += "\nPDCT:\n\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    label1.Text += String.Format("{0,-10}", Math.Round(PDCT[i, j], 4));
                label1.Text += "\n";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            PDCT = new double[size, size];
            DCT = new double[size, size];
            DCTT = new double[size, size];
            label1.Text += "\nDCT по формуле 6:\n\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == 0)
                        DCT[i, j] = Math.Sqrt(2.0 * size) * Math.Cos((2.0 * j + 1) * i * (3.14 / (2.0 * size)));
                    else
                        DCT[i, j] = 2.0 * Math.Sqrt(size) * Math.Cos((2.0 * j + 1) * i * (3.14 / (2.0 * size)));
                    label1.Text += String.Format("{0,-11}", Math.Round(DCT[i, j], 6));
                }
                label1.Text += "\n";
            }
            label1.Text += "\nDCT-транспонированная:\n\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    DCTT[i, j] = DCT[j, i];
                    label1.Text += String.Format("{0,-11} ", Math.Round(DCTT[i, j], 6));
                }
                label1.Text += "\n";
            }
            double[,] PDCT1 = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        PDCT1[i, j] += DCT[i, k] * P[k, j];
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        PDCT[i, j] += PDCT1[i, k] * DCTT[k, j];
                    }
                }
            }
            label1.Text += "\nPDCT:\n\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    label1.Text += String.Format("{0,-16}", Math.Round(PDCT[i, j], 4));
                label1.Text += "\n";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            QUANT = new double[size, size];
            PQDCT = new double[size, size];
            double q = Convert.ToDouble(textBox1.Text);
            label1.Text += "\nМатрица квантования:\n\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    QUANT[i, j] = 1 + ((1 + i + j) * q);
                    label1.Text += String.Format("{0,-5} ", QUANT[i, j]);
                }
                label1.Text += "\n";
            }
            label1.Text += "\nPQDCT:\n\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    PQDCT[i, j] = PDCT[i, j] / QUANT[i, j];
                    label1.Text += String.Format("{0,-10} ", Math.Round(PQDCT[i, j]));
                }
                label1.Text += "\n";
            }

        }

        static public double[,] Gauss(double[,] array)
        {
            double[,] aRevers = new double[size, size];
            double[,] aCopy = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j) aRevers[i, j] = 1;
                    else aRevers[i, j] = 0;
                    aCopy[i, j] = array[i, j];
                }
            }
            for (int k = 0; k < size; k++)
            {
                double div = aCopy[k, k];
                for (int m = 0; m < size; m++)
                {
                    aCopy[k, m] /= div;
                    aRevers[k, m] /= div;
                }
                for (int i = k + 1; i < size; i++)
                {
                    double multi = aCopy[i, k];
                    for (int j = 0; j < size; j++)
                    {
                        aCopy[i, j] -= multi * aCopy[k, j];
                        aRevers[i, j] -= multi * aRevers[k, j];
                    }
                }
            }
            for (int k = size - 1; k > 0; k--)
            {
                aCopy[k, size - 1] /= aCopy[k, k];
                aRevers[k, size - 1] /= aCopy[k, k];

                for (int i = k - 1; i + 1 > 0; i--)
                {
                    double multi = aCopy[i, k];
                    for (int j = 0; j < size; j++)
                    {
                        aCopy[i, j] -= multi * aCopy[k, j];
                        aRevers[i, j] -= multi * aRevers[k, j];
                    }
                }
            }
            return aRevers;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double[,] UP = new double[size, size];
            double[,] UP1 = new double[size, size];
            double[,] UP2 = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    UP1[i, j] = PQDCT[i, j] * QUANT[i, j];
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        UP2[i, j] += Gauss(DCT)[i, k] * UP1[k, j];
                    }
                }
            }
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        UP[i, j] += UP2[i, k] * Gauss(DCTT)[k, j];
                    }
                }
            }
            label1.Text += "\nРаспакованная матрица:\n\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    UP[i, j] = Math.Round(UP[i, j]) + 128;
                    label1.Text += String.Format("{0,-11} ", UP[i, j]);
                }
                label1.Text += "\n";
            }
            //Проверка на совпадение матриц
            bool flag = false;
            string C = null;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (UP[i, j] == P[i, j] + 128) flag = true;
                    else { flag = false; C += i.ToString() + ";" + j.ToString() + " "; }
                }
            }
            if (flag == true)
                label1.Text += "\nМатрицы совпадают.";
            else label1.Text += "\nМатрицы на совпадают." + C;

        }
    }
}
