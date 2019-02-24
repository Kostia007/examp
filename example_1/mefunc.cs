using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ValueTuple;

namespace Coursework
{
    public class mefunc
    {
        int [] g_norm(int [][] MatrixA, int[] MatrixB)
        {
            int[] x=new int[4];
            for(int j=0;j<4;j++)
            x[j]= Convert.ToInt32((MatrixB[1]*0.01) * (MatrixA[2][j] - MatrixA[1][j]) + MatrixA[1][j]); //g ̃_i = (1 - β)(b_i - a_i) + a_i
            return x;
        }
        public int [] d_exp(int[][] MatrixA, int[] MatrixB)
        {
            int[] x = new int[4];
            for (int j = 0; j < 4; j++)
                x[j] = Convert.ToInt32((-1) * (Math.Log((100-MatrixB[0])*0.01, 2.71828))*(MatrixA[0][j])); //d ̃_i=-ln⁡(α)*λ_i
            return x;
        }
        int saving(int [] MatrixC, int rest)
        {

            if (rest > 0)
                return MatrixC[3] * rest;
            else
                return MatrixC[4] * (-rest);

        }
        int costprod(int[][] MatrixA, int[] MatrixB, int[] MatrixC,int i,int x)
        {
            int[] t = g_norm(MatrixA, MatrixB);
            if (x <= t[i]) return x * MatrixC[0];
            else if (x <= t[i] + Convert.ToInt32(0.25 * t[i])) return t[i] * MatrixC[0] + (x - t[i]) * MatrixC[1];
            else return t[i] * MatrixC[0] + Convert.ToInt32(0.25 * t[i]) * MatrixC[1] + (x - Convert.ToInt32(1.25 * t[i])) * MatrixC[2];
        }
        int F_target(int[][] MatrixA, int[] MatrixB, int[] MatrixC,int i, int x, int y)
        {
            int[] t = d_exp(MatrixA, MatrixB);
            //for( i=0;i<3;i++)
            return costprod(MatrixA, MatrixB, MatrixC,i, x) + saving(MatrixC, x + y - t[i]);
        }
        public int algo1(int[][] MatrixA, int[] MatrixB, int[] MatrixC, int[] y,int [][]F)
        {
            //int i = 0;
            y[0] = MatrixB[2];
            int[] d = d_exp(MatrixA, MatrixB);
            int[] g = g_norm(MatrixA, MatrixB);
            //First step
            //int xmin1 = 0;
            int xmax1 = Math.Min(Convert.ToInt32(1.55 * g[0]), d[0] + d[1] + d[2] + d[3]);
            int ymin1 = (-1)*( d[0]);
            int ymax1 = d[1] + d[2] + d[3];
            int F_min = 1000000000;
            int X_min = 0;
            int x_t;
            int y_t;
            //if (d[0] > xmax1) return xmax1;
            for ( y_t = ymin1; y_t <= ymax1; y_t++) {
                
                x_t = y_t + d[0];
                if ((x_t <= xmax1)&& (x_t >= 0)) {
                    if (F_min > F_target(MatrixA, MatrixB, MatrixC, 0, x_t, y[0]))
                    {
                        F_min = F_target(MatrixA, MatrixB, MatrixC, 0, x_t, y[0]);
                        X_min = x_t;
                        y[1] = y_t;
                    }
                    if (y_t>0)
                        F[0][y_t] = F_target(MatrixA, MatrixB, MatrixC, 0, x_t, y[0]);
                    else if (y_t<0)
                        F[1][(-1)*y_t] = F_target(MatrixA, MatrixB, MatrixC, 0, x_t, y[0]);
                    else { F[0][y_t] = F_target(MatrixA, MatrixB, MatrixC, 0, x_t, y[0]);
                           F[1][(-1) * y_t] = F_target(MatrixA, MatrixB, MatrixC, 0, x_t, y[0]);
                         }

                }
                else break;    
            }
            F[6][0] = F_min;
            
            return (X_min);
        }
        public int algo2(int[][] MatrixA, int[] MatrixB, int[] MatrixC, int[] y, int[][] F)
        {
            //y[] = MatrixB[2];
            int[] d = d_exp(MatrixA, MatrixB);
            int[] g = g_norm(MatrixA, MatrixB);
            //second step
            //int xmin2 = 0;
            int xmax2 = Math.Min(Convert.ToInt32(1.55 * g[1]), d[1] + d[2] + d[3]);
            int ymin2 = -d[1];
            int ymax2 = d[2] + d[3];
            int F_min = 1000000000;
            int X_min = 0;
            //if (d[1] > xmax2) { X_min = xmax2; return (X_min); }

            for (int y_t = ymin2; y_t <= ymax2; y_t++)
            {
                int x_t;
                int y_pre;
                int minF = 1000000000;
                for (x_t = 0; x_t <= xmax2; x_t++)
                   // if (x_t <= xmax2)
                    {
                        y_pre = y_t + d[1] - x_t;
                    if (y_pre >= 0)
                    {
                        if (F[0][y_pre] != 0)
                        {
                            if ((F_min > F_target(MatrixA, MatrixB, MatrixC, 1, x_t, y_t) + F[0][y_pre]))
                            {
                                F_min = F_target(MatrixA, MatrixB, MatrixC, 1, x_t, y_t) + F[0][y_pre];
                                X_min = x_t;
                                y[2] = y_t;
                            }
                            if ((minF > F_target(MatrixA, MatrixB, MatrixC, 1, x_t, y_t) + F[0][y_pre]))
                                minF = F_target(MatrixA, MatrixB, MatrixC, 1, x_t, y_t) + F[0][y_pre];
                        }
                    }
                    else if (y_pre < 0)
                    {
                        if (F[1][(-1) * y_pre] != 0)
                        {
                            if ((F_min > F_target(MatrixA, MatrixB, MatrixC, 1, x_t, y_t) + F[1][(-1) * y_pre]))
                            {
                                F_min = F_target(MatrixA, MatrixB, MatrixC, 1, x_t, y_t) + F[1][(-1) * y_pre];
                                X_min = x_t;
                                y[2] = y_t;
                            }
                            if ((minF > F_target(MatrixA, MatrixB, MatrixC, 1, x_t, y_t) + F[1][(-1)*y_pre]))
                                minF = F_target(MatrixA, MatrixB, MatrixC, 1, x_t, y_t) + F[1][(-1)*y_pre];
                        }
                    }
                        //x_t = y_t + d[1];
                    }
                if (y_t>0) F[2][y_t] = minF;
                else if (y_t<0) F[3][(-1)*y_t] = minF;
                else
                {
                    F[2][y_t] = minF;
                    F[3][(-1) * y_t] = minF;
                }
            }
            F[6][1] = F_min;
            return (X_min);
        }
        public int algo3(int[][] MatrixA, int[] MatrixB, int[] MatrixC,  int[] y, int[][] F)
        {
            //y[] = MatrixB[2];
            int[] d = d_exp(MatrixA, MatrixB);
            int[] g = g_norm(MatrixA, MatrixB);
            //second step
            //int xmin3 = 0;
            int xmax3 = Math.Min(Convert.ToInt32(1.55 * g[2]), d[2] + d[3]);
            int ymin3 = -d[2];
            int ymax3 = d[3];
            int F_min = 1000000000;
            int X_min = 0;
            for (int y_t = ymin3; y_t <= ymax3; y_t++)
            {
                int x_t;
                int y_pre;
                int minF = 1000000000;
                for (x_t = 0; x_t <= xmax3; x_t++)
                // if (x_t <= xmax2)
                {
                    y_pre = y_t + d[2] - x_t;
                    if (y_pre >= 0)
                    {
                        if (F[2][y_pre] != 0)
                        {
                            if ((F_min > F_target(MatrixA, MatrixB, MatrixC, 2, x_t, y_t) + F[2][y_pre]))
                            {
                                F_min = F_target(MatrixA, MatrixB, MatrixC, 2, x_t, y_t) + F[2][y_pre];
                                X_min = x_t;
                                y[3] = y_t;
                            }
                            if ((minF > F_target(MatrixA, MatrixB, MatrixC, 2, x_t, y_t) + F[2][y_pre]))
                                minF = F_target(MatrixA, MatrixB, MatrixC, 2, x_t, y_t) + F[2][y_pre];
                        }
                    }
                    else if (y_pre < 0)
                    {
                        if (F[3][(-1) * y_pre] != 0)
                        {
                            if ((F_min > F_target(MatrixA, MatrixB, MatrixC, 2, x_t, y_t) + F[3][(-1) * y_pre]))
                            {
                                F_min = F_target(MatrixA, MatrixB, MatrixC, 2, x_t, y_t) + F[3][(-1) * y_pre];
                                X_min = x_t;
                                y[3] = y_t;
                            }
                            if ((minF > F_target(MatrixA, MatrixB, MatrixC, 2, x_t, y_t) + F[3][(-1) * y_pre]))
                                minF = F_target(MatrixA, MatrixB, MatrixC, 2, x_t, y_t) + F[3][(-1) * y_pre];
                        }
                    }
                    //x_t = y_t + d[1];
                }
                if (y_t > 0) F[4][y_t] = minF;
                else if (y_t < 0) F[5][(-1) * y_t] = minF;
                else
                {
                    F[4][y_t] = minF;
                    F[5][(-1) * y_t] = minF;
                }
            }
            F[6][2] = F_min;
            return (X_min);
        }
        public int algo4(int[][] MatrixA, int[] MatrixB, int[] MatrixC,  int[] y, int[][] F)
        {
            //y[] = MatrixB[2];
            int[] d = d_exp(MatrixA, MatrixB);
            int[] g = g_norm(MatrixA, MatrixB);
            //second step
            //int xmin4 = 0;
            int xmax4 = Math.Min(Convert.ToInt32(1.55 * g[3]),d[3]);
            int ymin4 = -d[3];
            int ymax4 = 0; 
            int F_min = 1000000000;
            int X_min = 0;
            if ( (d[3] - xmax4 + (-1)*y[3]) <= d[3])
            {
                for (int y_t = ymin4; y_t <= ymax4; y_t++)
                {
                    int x_t;
                    int y_pre;
                    for (x_t = 0; x_t <= xmax4; x_t++)

                    {
                        y_pre = y_t + d[3] - x_t;
                        if (y_pre >= 0)
                        {
                            if (F[4][y_pre] != 0)
                                if ((F_min > F_target(MatrixA, MatrixB, MatrixC, 3, x_t, y_t) + F[4][y_pre]))
                                {
                                    F_min = F_target(MatrixA, MatrixB, MatrixC, 3, x_t, y_t) + F[4][y_pre];
                                    X_min = x_t;
                                    y[4] = y_t;
                                }
                        }
                        else if (y_pre < 0)
                        {
                            if (F[5][(-1) * y_pre] != 0)
                                if ((F_min > F_target(MatrixA, MatrixB, MatrixC, 3, x_t, y_t) + F[5][(-1) * y_pre]))
                                {
                                    F_min = F_target(MatrixA, MatrixB, MatrixC, 3, x_t, y_t) + F[5][(-1) * y_pre];
                                    X_min = x_t;
                                    y[4] = y_t;
                                }
                        }
                        //x_t = y_t + d[1];
                    }
                }

                F[6][3] = F_min;
            }
            else
            {
                int y_pre =  d[3] - xmax4+(d[3] - xmax4 + (-1)*y[3]);
                
                y[4] = (-1) * (d[3] - xmax4 + (-1) * y[3]);
                F[6][3] = F[6][2] + F_target(MatrixA, MatrixB, MatrixC, 3, xmax4, y[4]);
                X_min = xmax4;
            }
            return (X_min);
        }


    }
}
