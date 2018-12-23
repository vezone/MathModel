using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModel.src.Models
{
    public class BoundaryProblem
    {
        int N;
        int NP;
        
        double h;
        double r;
        double l;
        double A;
        double B;

        double[] x;
        double[] y;
        double[] a;
        double[] b;
        double[] c;
        double[] d;
        double[] E;
        double[] Nu;

        public double[] X => x;
        public double[] Y => y;

        public double MinX
        {
            get
            {
                double value = x[0];
                for (int i = 1; i < x.Length; i++)
                {
                    if (value > x[i])
                        value = x[i];
                }
                return value;
            }
        }
        public double MaxX
        {
            get
            {
                double value = x[0];
                for (int i = 1; i < x.Length; i++)
                {
                    if (value < x[i])
                        value = x[i];
                }
                return value;
            }
        }

        public double MinY
        {
            get
            {
                double value = y[0];
                for (int i = 1; i < y.Length; i++)
                {
                    if (value > y[i])
                        value = y[i];
                }
                return value;
            }
        }
        public double MaxY
        {
            get
            {
                double value = y[0];
                for (int i = 1; i < y.Length; i++)
                {
                    if (value < y[i])
                        value = y[i];
                }
                return value;
            }
        }
        
        public delegate double FunctionPrototype(double x);

        public BoundaryProblem(int n = 30)
        {
            N = n;
            NP = N+1;
            r = 3;
            l = 3;

            x = new double[N + 1];
            y = new double[NP];
            a  = new double[NP];
            b  = new double[NP];
            c  = new double[NP];
            d  = new double[NP];

            E = new double[NP];
            Nu = new double[NP];
            E[0] = 0;
            Nu[0] = 0;

            //for kfunction
            A = 0.5;
            B = 2.5;
        }

        public void CreateGrid()
        {
            double a = 0, b = 3;
            
            x[0] = a;
            h = (b - a) / N;

            for (int j = 0; j <= N; j++)
            {
                x[j] = x[0] + j * h;
            }
        }
        
        public void LinearEquations(FunctionPrototype functionPrototype)
        {
            for (int j = 0; j < N; j++)
            {
                a[j] = 1 - 0.5 * pfunc(x[j]) * h;
                b[j] = 2 - qfunc(x[j]) * h * h;
                c[j] = 1 + 0.5 * pfunc(x[j]) * h;
                d[j] = functionPrototype(x[j]) * h * h;
            }
        }
        
        public void TridiagonalMatrixAlgorithm()
        {
            //прямой ход прогонки
            for (int i = 0; i < (N); i++)
            {
                double low = (b[i] - a[i] * E[i]);
                E[i + 1] = c[i] / low;
                Nu[i + 1] = (a[i] * Nu[i] - d[i]) / low;
            }
            
            Array.Copy(E, y, E.Length);
            
            //обратный ход прогонки
            for (int i = (N - 1); i >= 0; i--)
            {
                y[i] = E[i + 1] * y[i + 1] + Nu[i + 1];
            }
        }

        public void Model(FunctionPrototype functionPrototype)
        {
            CreateGrid();
            LinearEquations(functionPrototype);
            TridiagonalMatrixAlgorithm();
        }

        //Additional
        //не задана
        public double pfunc(double x)
        {
            return 0.0;
        }

        //не задана
        public double qfunc(double x)
        {
            return 0.0;
        }

        public double kfunction(double x)
        {
            // change A >= x and try it
            return ((x > A) && (x <= B)) ? 3 : 0;
        }

        public double function(double x)
        {
            return Math.Abs(Math.Sin(Math.PI * 3 * Math.Pow(x, r) / l));
        }

    }
}
