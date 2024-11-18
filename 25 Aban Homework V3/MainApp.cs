using System.Text.RegularExpressions;
void main(){
    Console.WriteLine("Please enter the employee's firstname  ");
    string FirstName = GetCharacter();

    Console.WriteLine("Please enter the employee's lastname  ");
    string LastName = GetCharacter();

    Console.WriteLine("Please enter the employee's phonenumber  ");
    long PhoneNumber = GetNumber();
    while(!IsPhoneNumberValid(PhoneNumber)){
        Console.WriteLine("Incorrect phonenumber. Please enter the employee's phonenumber correctly  ");
        PhoneNumber = GetNumber();
    }

    Console.WriteLine("Is the employee Married ? ");
    string ValidatedKey = GetKey("yn");
    bool IsMarried = false;
    if (ValidatedKey == "y")IsMarried = true;

    Console.WriteLine("Please enter the employee's Code-e meli  ");
    long CodeMeli = GetNumber();
    while(!IsCodeMeliValid(CodeMeli)){
        Console.WriteLine("Incorrect Code-e Meli. Please enter the employee's Code-e meli correctly  ");
        CodeMeli = GetNumber();
    }

    Console.WriteLine("Please enter the employee's base salary  ");
    long BaseSalary = GetNumber();

    Console.WriteLine("Please enter the employee's base salary's floating point  ");
    long BaseSalaryFloatingPoint = GetNumber();

    Console.WriteLine("Please enter the employee's type [S for SimpleEmployee, E for ServiceEmployee, M for ManagerEmployee, C for CEOEmployee ] ");
    string ValidatedType = GetKey("semc");
    System.Console.WriteLine(ValidatedType);
    if (ValidatedType == "s"){
        SimpleEmployee simpleemployee = new SimpleEmployee(FirstName,LastName,PhoneNumber,CodeMeli,IsMarried,float.Parse($"{BaseSalary}.{BaseSalaryFloatingPoint}"));
        System.Console.WriteLine(simpleemployee.ToString());
    }
    else if (ValidatedType == "e"){
        ServiceEmployee serviceemployee = new ServiceEmployee(FirstName,LastName,PhoneNumber,CodeMeli,IsMarried,float.Parse($"{BaseSalary}.{BaseSalaryFloatingPoint}"));
        System.Console.WriteLine(serviceemployee.ToString());
    }
    else if (ValidatedType == "m"){
        ManagerEmployee manageremployee = new ManagerEmployee(FirstName,LastName,PhoneNumber,CodeMeli,IsMarried,float.Parse($"{BaseSalary}.{BaseSalaryFloatingPoint}"));
        System.Console.WriteLine(manageremployee.ToString());
    }
    else if (ValidatedType == "c"){
        CEOEmployee ceoemployee = new CEOEmployee(FirstName,LastName,PhoneNumber,CodeMeli,IsMarried,float.Parse($"{BaseSalary}.{BaseSalaryFloatingPoint}"));
        System.Console.WriteLine(ceoemployee.ToString());
    }
}

bool IsCodeMeliValid(long CodeMeli){
    if(CodeMeli.ToString().ToCharArray().Distinct().ToArray().Length == 1){
        return false;
    }
    char[] CodeMeliArray = $"{CodeMeli:D10}".ToCharArray();
    char ControlDigit = CodeMeliArray[CodeMeliArray.Length-1];
    char[] CodeMeliRemainder = CodeMeliArray.Take(9).ToArray();
    int CodeMeliSum = 0;
    int i = 10;
    foreach(char c in CodeMeliRemainder){
        CodeMeliSum += int.Parse(c.ToString())*i;
        i -= 1;
    }
    int CheckNumber = CodeMeliSum - (CodeMeliSum/11)*11;
    if (
        (CheckNumber == 0 && ControlDigit == CheckNumber) || 
        (CheckNumber == 1 && ControlDigit == 1) || 
        (CheckNumber > 1 && int.Parse(ControlDigit.ToString()) == Math.Abs(CheckNumber -11))
        ){
            return true;
        }
    return false;
}

bool IsPhoneNumberValid(long PhoneNumber){
    string pattern = @"((0?9)|(\+?989))((14)|(13)|(12)|(19)|(18)|(17)|(15)|(16)|(11)|(10)|(90)|(91)|(92)|(93)|(94)|(95)|(96)|(32)|(30)|(33)|(35)|(36)|(37)|(38)|(39)|(00)|(01)|(02)|(03)|(04)|(05)|(41)|(20)|(21)|(22)|(23)|(31)|(34)|(9910)|(9911)|(9913)|(9914)|(9999)|(999)|(990)|(9810)|(9811)|(9812)|(9813)|(9814)|(9815)|(9816)|(9817)|(998))\d{7}";
    Regex rg = new Regex(pattern);
    return rg.IsMatch(PhoneNumber.ToString());
}

long GetNumber(){
    List<string> NumbersList = new List<string>(){};
    bool ToContinue = true;
    while(ToContinue){
        Console.WriteLine($"> {String.Join("",NumbersList)}");
        System.ConsoleKeyInfo input = Console.ReadKey();
        if(char.IsDigit(input.KeyChar)){
            NumbersList.Add(input.KeyChar.ToString());
            Console.Clear();
            continue;
        }
        if (input.Key.Equals(ConsoleKey.Enter) && NumbersList.Count > 0){
            ToContinue = false;
            Console.Clear();
            break;
        }
        if (input.Key.Equals(ConsoleKey.Backspace)){
            if (NumbersList.Count == 0){
                continue;
            }
            NumbersList.RemoveAt(NumbersList.Count-1);
            Console.Clear();
            continue;
        }
        else{
            Console.Clear();
            continue;
        }
    }
    return Convert.ToInt64(String.Join("",NumbersList));
}

string GetCharacter(){
    List<string> CharacterList = new List<string>(){};
    bool ToContinue = true;
    while(ToContinue){
        Console.WriteLine($"> {String.Join("",CharacterList)}");
        System.ConsoleKeyInfo input = Console.ReadKey();
        if(char.IsLetter(input.KeyChar)){
            CharacterList.Add(input.KeyChar.ToString());
            Console.Clear();
            continue;
        }
        if (input.Key.Equals(ConsoleKey.Enter) && CharacterList.Count >0){
            ToContinue = false;
            Console.Clear();
            break;
        }
        if (input.Key.Equals(ConsoleKey.Backspace)){
            if (CharacterList.Count == 0){
                continue;
            }
            CharacterList.RemoveAt(CharacterList.Count-1);
            Console.Clear();
            continue;
        }
        else{
            Console.Clear();
            continue;
        }
    }
    return String.Join("",CharacterList);
}

string GetKey(string options){
    bool ToContinue = true;
    string ValidatedCharacter = "";
    while(ToContinue){
        System.ConsoleKey input = Console.ReadKey(true).Key;
        if(!options.Contains(input.ToString().ToLower())){
            continue;
        }
        ToContinue = false;
        ValidatedCharacter = input.ToString().ToLower();
    }
    return ValidatedCharacter;
}   
main();