using System.Globalization;
using System.Reflection.Metadata.Ecma335;

public abstract class Calculator{
   public abstract bool IsTrue(int number);
}

public class SPrime : Calculator{
    public override bool IsTrue(int number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        else{
            return CheckIfPrime(number);
        }
    }
    private bool CheckIfPrime(int number){
        for (int i = 2; i < number;i++){
            if ((number % i) == 0) return false;
        }
        return true;
    }
}

public class PPrime : Calculator{
    public override bool IsTrue(int number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        else{
            return CheckIfPrime(number);
        }
    }
    private bool CheckIfPrime(int number){
        for (int i = 2; i <= Math.Ceiling(Math.Sqrt(number));i++){
            if ((number % i) == 0) return false;
        }
        return true;
    }
}

public class SPalindrome : Calculator{
    public override bool IsTrue(int number)
    {
        return CheckIfPalindrome(number);
    }
    private bool CheckIfPalindrome(int number){
        int original_number = number;
        int reverse = 0;
        do{
            reverse = (reverse*10)+(number%10);
            number = (int)number / 10;    
        }while(number > 0);
        if (original_number == reverse) return true;
        return false;
    }
}

public class PPalindrome : Calculator{
    public override bool IsTrue(int number)
    {
        return CheckIfPalindrome(number);
    }
    private bool CheckIfPalindrome(int number){
        string reversed_number = new string(number.ToString().Reverse().ToArray());
        if (number.ToString() == reversed_number) return true;
        return false;
    }
}

public class CalculatorFactory{
    public static Calculator CreateCalculator(int number,string CalculationType){
        if (CalculationType == "prime"){
            if (number>=1000){
                return new PPrime();
            }
            return new SPrime();
        }
        if (number>=1000){
            return new PPalindrome();
        }
        return new SPalindrome();
    }
}