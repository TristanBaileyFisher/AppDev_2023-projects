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

namespace PrimeNumber.views
{
    /// <summary>
    /// Interaction logic for PrimeViewer.xaml
    /// </summary>
    public partial class PrimeViewer : UserControl

    {
        public PrimeViewer()
        {
            InitializeComponent();
        }

      

        private void Btn1_click(object sender, RoutedEventArgs e)
        {
            //code to be implimented on click of button

            int[] set1 = InBox1.Text.Split(' ').Select(s => int.Parse(s)).ToArray(); 
            int[] set2 = InBox2.Text.Split(' ').Select(s => int.Parse(s)).ToArray();

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
            static string GetPrimesString(int low, int high)
            {
                StringBuilder primeNumbers = new StringBuilder();

                for (int i = low; i <= high; i++)
                {
                    if (IsPrime(i))
                    {
                        primeNumbers.Append(i).Append(" ");
                    }
                }

                return primeNumbers.ToString();
            }

            //output prime number text
            PrimeNumbersOutput.Text += $"Primes between {lowprime1}-{hiprime1}: {GetPrimesString(lowprime1, hiprime1)}\n";


            Console.WriteLine($"Primes between {lowprime2}-{hiprime2}: ");
            PrintPrimes(lowprime2, hiprime2);

            StringBuilder resultBuilder = new StringBuilder();

            // Append prime numbers to the result builder
            resultBuilder.AppendLine($"Primes between {lowprime1}-{hiprime1}: {GetPrimesString(lowprime1, hiprime1)}");
            resultBuilder.AppendLine($"Primes between {lowprime2}-{hiprime2}: {GetPrimesString(lowprime2, hiprime2)}");

            // Set the Text property of the TextBlock to the accumulated results
            PrimeNumbersOutput.Text = resultBuilder.ToString();
        




    }
        private void Btn2_click(object sender, RoutedEventArgs e)
        {
            ResetUI();

        }
        
        private void ResetUI()
        {
            InBox1.Text = string.Empty;
            InBox2.Text = string.Empty;
            PrimeNumbersOutput.Text = string.Empty;
        }
    }
}
