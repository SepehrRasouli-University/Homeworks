Console.WriteLine("Please Enter Integer > ");
string number = Console.ReadLine();
static bool if_palindrome(string number)
{
    string reversed_number = new string(number.Reverse().ToArray());
    if (number == reversed_number) return true;
    return false;
}


if (if_palindrome(number))
    Console.WriteLine("Number is palindrome");
else{
    Console.WriteLine("Number is not palindrome");
}
