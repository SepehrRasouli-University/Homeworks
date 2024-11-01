using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
Console.WriteLine("Input > ");
var input = Console.ReadLine();
input += " ";
int Current_Input_Index = 0;
int Integer_start = 0;
int Integer_len = 0;
List<string> numbers = new List<string>();
bool add_numbers(char[] input_array){
    Integer_start = Current_Input_Index;
    Integer_len = 0;
    do{
        Integer_len += 1;
        Current_Input_Index += 1;
    }while("0123456789".Contains(input_array[Current_Input_Index]));
    int Integer_end = Integer_len + Integer_start;
    numbers.Add(input.Substring(Integer_start,Integer_len));
    Current_Input_Index = Integer_end;
    return true;
}
while (Current_Input_Index <= input.ToCharArray().Length - 1){
    if ("0123456789".Contains(input[Current_Input_Index])){
        //Console.WriteLine(input.ToCharArray().Length);
        add_numbers(input.ToCharArray());
    }
    else{
        Current_Input_Index += 1;
        continue;
    }
}
foreach (string x in numbers){
    if (x.StartsWith("0") && (x.Length == 10) && (HasMoreThanTwoElementNumbers(x).Length >= 2)){
        Console.WriteLine(x);
    }
}
string HasMoreThanTwoElementNumbers(string x){
    return new string(x.ToCharArray().Distinct().ToArray());
}