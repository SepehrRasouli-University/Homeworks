void MainApp(){
    Console.WriteLine("\n Please enter your selected option by clicking the correspanding key on your keyboard.");
    Console.WriteLine("\n A. Prime number checker");
    Console.WriteLine("\n B. Palindrome number checker");
    Console.WriteLine("\n C. Number Guesser");
    Console.WriteLine("\n ESC. Exit the app");
    string ValidatedKey = Keyboard.GetHomeKey("abc");
    if(ValidatedKey == "a") PrimeNumberOption();
    else if(ValidatedKey == "b") PalindromeNumberOption();
    else NumberGuesserOption();
}

void PrimeNumberOption(){
    Console.Clear();
    int ValidatedNumber = Keyboard.GetNumber();
    Calculator prime = CalculatorFactory.CreateCalculator(ValidatedNumber,"prime");
    if(prime.IsTrue(ValidatedNumber)) Console.WriteLine($"{ValidatedNumber} is a prime number.");
    else Console.WriteLine($"{ValidatedNumber} is not a prime number");

    string ValidatedKey = Pagination();
    if (ValidatedKey == "a") PrimeNumberOption();
    else Console.Clear(); MainApp();
}

void PalindromeNumberOption(){
    Console.Clear();
    int ValidatedNumber = Keyboard.GetNumber();
    Calculator palindrome = CalculatorFactory.CreateCalculator(ValidatedNumber,"palindrome");
    if(palindrome.IsTrue(ValidatedNumber)) Console.WriteLine($"{ValidatedNumber} is a palindrome number.");
    else Console.WriteLine($"{ValidatedNumber} is not a palindrome number");

    string ValidatedKey = Pagination();
    if (ValidatedKey == "a") PalindromeNumberOption();
    else Console.Clear(); MainApp();

}

void NumberGuesserOption(){
    Console.Clear();
    Console.WriteLine("Please pick a number bigger or smaller than 50.");
    Console.WriteLine("Is your number bigger or smaller than 50 ? [Press y if bigger, n if smaller]");
    string ValidatedKey = Keyboard.GetHomeKey("yn");
    if (ValidatedKey == "y") GuessNumber(100,50);
    else GuessNumber(50,1);
}

void GuessNumber(int a1,int a2){
    int middle = (int)Math.Ceiling((double) ((a1-a2)/2));
    if (middle==0){
        if (a1==100){
            Console.WriteLine("\nYour number is 100");
            string ValidatedUserInput = Pagination();
            if (ValidatedUserInput == "a") NumberGuesserOption();
            else Console.Clear(); MainApp();

        }
        else
        {
            Console.WriteLine("\n Your number is 1");
            string ValidatedUserInput = Pagination();
            if (ValidatedUserInput == "a") NumberGuesserOption();
            else Console.Clear(); MainApp();
        }
    }
    int c = a2+middle;    
    Console.WriteLine($"\nIs your number bigger or smaller than {c} ? [Press y if bigger, n if smaller, e if equal]");
    string ValidatedKey = Keyboard.GetHomeKey("yne");
    if (ValidatedKey == "y") GuessNumber(a1,c);
    else if (ValidatedKey == "n")GuessNumber(c,a2);
    else if (ValidatedKey == "e") Console.WriteLine("\nYour number is "+c); 
    string ValidatedInput = Pagination();
    if (ValidatedInput == "a") NumberGuesserOption();
    else Console.Clear(); MainApp();
}

string Pagination(){
    Console.WriteLine("[A] Check another number      [B] Back to the main page   [ESC] Exit the app");
    string ValidatedKey = Keyboard.GetHomeKey("ab");
    return ValidatedKey;
}

MainApp();