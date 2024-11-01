// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

var number = 0;
Console.WriteLine("Number > ");
number = Convert.ToInt32(Console.ReadLine());

bool is_prime(int number)
{
    if (number < 2) return false;
    else if (number == 2) return true;

    int boundary = (int)Math.Ceiling(Math.Sqrt(number));

    for (int i = 2; i <= boundary; i += 1)
        if (number % i == 0)
            return false;
    return true;
}

if (is_prime(number))
{
    Console.WriteLine("The number is prime");
}
else
{
    Console.WriteLine("The number is not prime");
}       
