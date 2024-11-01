Console.WriteLine("Enter number 1 of the fibonacci sequence > ");
var a1 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Enter number 2 of the fibonacci sequence > ");
var a2 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Enter n to check if it is in the fibonacci sequence > ");
var n = Convert.ToInt32(Console.ReadLine());
int k = 0;

while (k < n)
{
    k = a1 + a2;
    a1 = a2;
    a2 = k;
}
if (k == n)
{
    Console.WriteLine("n is in the fibonacci sequence");
}
else 
{
    Console.WriteLine("n is not in the fibonacci sequence");
}