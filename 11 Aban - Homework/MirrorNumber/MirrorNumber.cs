Console.WriteLine("Please Enter Integer 1 > ");
int number_1 = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Please Enter Integer 2 > ");
int number_2 = Convert.ToInt32(Console.ReadLine());
for (int i = number_1; i <= number_2; i++){
    if (if_palindrome(i.ToString())) Console.WriteLine(i);
}

bool if_palindrome(string number)
{
    string reversed_number = new string(number.Reverse().ToArray());
    if (number == reversed_number) return true;
    return false;
}

