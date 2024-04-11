using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Input_output
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuUI();

        }

        static void MenuUI()
        {
            //enter and read two sets of numbers (split text at , and convert to both numbers and their own arrays)
            Console.WriteLine("Enter first set of numbers: ");
            int[] set1 = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();

            Console.WriteLine("Enter second set of numbers: ");
            int[] set2 = Console.ReadLine().Split(' ').Select(s => int.Parse(s)).ToArray();

            //organise numbers in size
            int number1 = set1[0];
            int number2 = set1[1];
            int number3 = set2[0];
            int number4 = set2[1];

            int lowprime1, hiprime1;
            int lowprime2, hiprime2;

            if (number1 < number2)
            {
                lowprime1 = number1;
                hiprime1 = number2;
            }
            else
            {
                lowprime1 = number2;
                hiprime1 = number1;
            }
            if (number3 < number4)
            {
                lowprime2 = number3;
                hiprime2 = number4;
            }
            else
            {
                lowprime2 = number4;
                hiprime2 = number3;
            }

            //output prime number text
            Console.Write($"Primes between {lowprime1}-{hiprime1}: ");
            PrintPrimes(lowprime1, hiprime1);

            Console.Write($"Primes between {lowprime2}-{hiprime2}: ");
            PrintPrimes(lowprime2, hiprime2);

        }

        //calculate prime numbers
        static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        //print output of all prime numbers between a - b
        static void PrintPrimes(int low, int high)
        {
            for (int i = low; i <= high; i++)
            {
                if (IsPrime(i))
                {
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine();

        }

        //reset button for task 2
        static void Reset(){
            
        }
        
    }

   
}
