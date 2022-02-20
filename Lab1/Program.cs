using System;
using System.Text;

namespace Lab1
{
    internal class Program
    {
        public static double f1(double x) => Math.Pow(x, 3) - 7 * x - 6;
        public static double f2(double x) => Math.Pow(x, 3) - 6 * x * x + 5*x + 12;
        public static double df2(double x) => 3 * Math.Pow(x, 2) - 12 * x + 5;
        public static double dfi3(double x) => (1 - 3 * x * x) / (6 * Math.Sqrt((3 + x - x * x * x) / 3));
        public static double fi3(double x) => Math.Sqrt((3 + x - x * x * x) / 3);

        public static void DivHalf(double a, double b, double E)
        {
            Console.WriteLine("Метод ділення навпіл для рівняння x^3 - 7x - 6, проміжок: [{0}; {1}]\n", a, b);
            Console.WriteLine("║  i║               x         ║              f(x)               ║       x(n) - x(n-1)     ║");
            double x = (a + b) / 2;
            double prev_x = 0;
            
            int n = (int)(Math.Log2((b - a) / E)) + 1;
            int c = 0;
            Console.WriteLine("║{0, 3}║{1, 25}║{2, 33}║{3, 25}║", c, x, f1(x), x);
            while(true)
            {
                if (f1(x) * f1(a) > 0)
                    a = x;
                else
                    b = x;
                prev_x = x;
                x = (a + b) / 2;
                c++;
                Console.WriteLine("║{0, 3}║{1, 25}║{2, 33}║{3, 25}║", c, x, f1(x), Math.Abs(x - prev_x));
                if (Math.Abs(x - prev_x) < E)
                    break;
            }
            Console.WriteLine("Відповідь: x = {0}", x);
            Console.WriteLine("Апріорна к-сть ітерацій {0}", n);
            Console.WriteLine("Реальна к-сть ітерацій {0}\n", c+1);
        }

        public static void NewtonMeth(double a, double b, double E)
        {
            Console.WriteLine("Метод Ньютона для рівняння x^3 - 6x^2 + 5x + 12, проміжок: [{0}; {1}]\n", a, b);
            double x = -1.5;
            double prev_x = 0;
            double q = (12 - 6*a) * (0.9) / (2 * df2(b));
            int n = (int)(Math.Log2(Math.Log(0.9 / E) / Math.Log(1 / q)) + 1) + 1;
            int c = 0;
            Console.WriteLine("║  i║               x         ║              f(x)               ║       x(n) - x(n-1)     ║");
            Console.WriteLine("║{0, 3}║{1, 25}║{2, 33}║{3, 25}║", c, x, f2(x), x);
            while (true)
            {
                prev_x = x;
                x -= f2(prev_x) / df2(prev_x);

                c++;
                Console.WriteLine("║{0, 3}║{1, 25}║{2, 33}║{3, 25}║", c, x, f2(x), Math.Abs(x - prev_x));
                if (Math.Abs(x - prev_x) < E)
                    break;
            }
            Console.WriteLine("Відповідь: x = {0}", x);
            Console.WriteLine("Апріорна к-сть ітерацій {0}", n);
            Console.WriteLine("Реальна к-сть ітерацій {0}\n", c + 1);
        }

        public static void SimpIt(double a, double b, double E)
        {
            Console.WriteLine("Метод простої ітерації для рівняння x^3 + 3x^2 - x - 3, проміжок: [{0}; {1}]\n", a, b);
            double q = Math.Abs(dfi3(b));
            double x = b;
            double prev_x;
            int n = (int)(Math.Log(Math.Abs(x - fi3(x))/((1 - q)*E))/Math.Log(1/q)) + 1;
            int c = 0;
            Console.WriteLine("║  i║               x         ║              f(x)               ║       x(n) - x(n-1)     ║");
            Console.WriteLine("║{0, 3}║{1, 25}║{2, 33}║{3, 25}║", c, x, x - fi3(x), x);
            while (true)
            {
                prev_x = x;
                x = fi3(prev_x);
                c++;
                Console.WriteLine("║{0, 3}║{1, 25}║{2, 33}║{3, 25}║", c, x, x - fi3(x), Math.Abs(x - prev_x));
                if (Math.Abs(x - prev_x) < (1-q)*E/q)
                    break;
            }
            Console.WriteLine("Відповідь: x = {0}", x);
            Console.WriteLine("Апріорна к-сть ітерацій {0}", n);
            Console.WriteLine("Реальна к-сть ітерацій {0}\n", c + 1);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            double E = 1e-3;
            DivHalf(0, 5, E);
            NewtonMeth(-2.4, -0.6, E);
            SimpIt(-1.5, 1.3, E);
        }
    }
}
