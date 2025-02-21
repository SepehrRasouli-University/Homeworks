// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Number 1 > ");
int number_1 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Number 2 > ");
int number_2 = Convert.ToInt32(Console.ReadLine());
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

for (int i = number_1; i <= number_2; i++){
    if (is_prime(i)){
        Console.WriteLine(i);
    }
}