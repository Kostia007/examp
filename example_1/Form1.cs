using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coursework
{
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();

            

            this.dataGridView1.RowHeadersWidth = 150;
            this.dataGridView1.Rows.Add(4);
            this.dataGridView1.Rows[1].HeaderCell.Value = "Средний спрос  ";
            this.dataGridView1.Rows[2].HeaderCell.Value = "Объем выпуска при нормальном режиме(min)";
            this.dataGridView1.Rows[3].HeaderCell.Value = "Объем выпуска при нормальном режиме(max)";
            for (int i = 0; i < 2; i++)
            {
                this.dataGridView1[0, 0].Value = "1";
                this.dataGridView1[1, 0].Value = "2";
                this.dataGridView1[2, 0].Value = "3";
                this.dataGridView1[3, 0].Value = "4";

            }
            this.dataGridView1[0, 1].Value = "120";
            this.dataGridView1[1, 1].Value = "140";
            this.dataGridView1[2, 1].Value = "200";
            this.dataGridView1[3, 1].Value = "150";

            this.dataGridView1[0, 2].Value = "80";
            this.dataGridView1[1, 2].Value = "120";
            this.dataGridView1[2, 2].Value = "120";
            this.dataGridView1[3, 2].Value = "100";


            this.dataGridView1[0, 3].Value = "100";
            this.dataGridView1[1, 3].Value = "150";
            this.dataGridView1[2, 3].Value = "210";
            this.dataGridView1[3, 3].Value = "120";

            this.dataGridView2.RowHeadersWidth = 150;
            this.dataGridView2.Rows.Add(1);
            this.dataGridView2.Rows[0].HeaderCell.Value = "Затраты на единицу";

            this.dataGridView2[0, 0].Value = "8";
            this.dataGridView2[1, 0].Value = "12";
            this.dataGridView2[2, 0].Value = "10";
            this.dataGridView2[3, 0].Value = "5";
            this.dataGridView2[4, 0].Value = "15";

            List<string> vegetables = new List<string> { "0.9", "0.85", "0.75", "0.7", "0.65" };

            //comboBox1.DataSource = vegetables;
            

            this.dataGridView5.RowHeadersWidth = 150;
            this.dataGridView5.Rows.Add(3);
            this.dataGridView5.Rows[0].HeaderCell.Value = "Вероятность удовлетворить спрос";
            this.dataGridView5.Rows[1].HeaderCell.Value = "Вероятность произвести максимум ";
            this.dataGridView5.Rows[2].HeaderCell.Value = "Начальный запас";


            this.dataGridView5[0, 0].Value = "80";
            this.dataGridView5[0, 1].Value = "50";
            this.dataGridView5[0, 2].Value = "0";
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            int[][] MatrixA = new int[][]{
                new int[]{  Convert.ToInt32(dataGridView1[0, 1].Value), Convert.ToInt32(dataGridView1[1, 1].Value), Convert.ToInt32(dataGridView1[2, 1].Value), Convert.ToInt32(dataGridView1[3, 1].Value) },
                new int[] { Convert.ToInt32(dataGridView1[0, 2].Value), Convert.ToInt32(dataGridView1[1, 2].Value), Convert.ToInt32(dataGridView1[2, 2].Value), Convert.ToInt32(dataGridView1[3, 2].Value)},
                new int[] { Convert.ToInt32(dataGridView1[0, 3].Value), Convert.ToInt32(dataGridView1[1, 3].Value), Convert.ToInt32(dataGridView1[2, 3].Value), Convert.ToInt32(dataGridView1[3, 3].Value)},
                };
            int[] MatrixB = new int[] { Convert.ToInt32(dataGridView5[0, 0].Value), Convert.ToInt32(dataGridView5[0, 1].Value), Convert.ToInt32(dataGridView5[0, 2].Value) };
            int[] MatrixC = new int[] { Convert.ToInt32(dataGridView2[0, 0].Value), Convert.ToInt32(dataGridView2[1, 0].Value), Convert.ToInt32(dataGridView2[2, 0].Value), Convert.ToInt32(dataGridView2[3, 0].Value) , Convert.ToInt32(dataGridView2[4, 0].Value) };



            mefunc f = new mefunc();
            int []y= new int[5];
            int []x= new int[4];
            int[][] F = new int[10][];
            for (int o = 0; o < F.Length; o++)
            {
                F[o] = new int[2000];
                for (int p = 0; p < 2000; p++)
                    F[o][p] = 0;
            }
            /* x[3] = MatrixA[0][3];
             x[2] = MatrixA[0][2];
             x[1] = MatrixA[0][1];
             x[0] = MatrixA[0][0];*/
            //int[] x = new int[MatrixA.Length];
            try
            {
                x[0] = f.algo1(MatrixA, MatrixB, MatrixC, y, F);
                x[1] = f.algo2(MatrixA, MatrixB, MatrixC, y, F);
                x[2] = f.algo3(MatrixA, MatrixB, MatrixC, y, F);
                x[3] = f.algo4(MatrixA, MatrixB, MatrixC, y, F);
                int[] d = f.d_exp(MatrixA, MatrixB);
                if ((F[6][0] >= 1000000000) || (F[6][1] >= 1000000000) || (F[6][2] >= 1000000000) || (F[6][3] >= 1000000000))
                    MessageBox.Show("Слишком большой шаг", "Ошибка");
                else
                {
                    richTextBox1.Text =
                "x1 = " + x[0].ToString() + "  y1 =  " + y[1].ToString() + "  d1 =  " + d[0].ToString() + "  F1 =  " + F[6][0].ToString() + ";\n" +
                "x2 = " + x[1].ToString() + "  y2 =  " + y[2].ToString() + "  d2 =  " + d[1].ToString() + "  F2 =  " + F[6][1].ToString() + ";\n" +
                "x3 = " + x[2].ToString() + "  y3 =  " + y[3].ToString() + "  d3 =  " + d[2].ToString() + "  F3 =  " + F[6][2].ToString() + ";\n" +
                "x4 = " + x[3].ToString() + "  y4 =  " + y[4].ToString() + "  d4 =  " + d[3].ToString() + "  F4 =  " + F[6][3].ToString() + ";\n";
                    textBox4.Text = F[6][3].ToString();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Слишком большой шаг", "Ошибка");


            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}

