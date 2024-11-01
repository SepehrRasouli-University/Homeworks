using System.Formats.Asn1;
main();
void main(){
    Console.Clear();
    Console.WriteLine("Please pick a number bigger or smaller than 50.");
    Console.WriteLine("Is your number bigger or smaller than 50 ? [Press y if bigger, n if smaller]");
    char y_n = Console.ReadKey().KeyChar;
    if (y_n.ToString() == "y"){
        guess_number(100,50);
    }
    else if (y_n.ToString() == "n"){
        guess_number(50,1);
    }
    else {
        main();
    }
}
bool guess_number(int a1,int a2){
    // a1 > a2
    int middle = (int)Math.Ceiling((double) ((a1-a2)/2));
    int c = a2+middle;    
    if ((a1-a2) == 1){
        Console.WriteLine("\nYour number is " + a1);
        return true;
    }
    Console.WriteLine("\nIs your number bigger or smaller than " + c + " ? [Press y if bigger, n if smaller, e if equal]");
    Console.WriteLine("\n"+a1 + " "+a2 + " "+middle+"  "+c+"");
    char y_n = Console.ReadKey().KeyChar;
    if (y_n.ToString() == "y"){
        guess_number(a1,c);
        return true;
    }
    else if (y_n.ToString() == "n"){
        guess_number(c,a2);
        return true;
    }
    else if (y_n.ToString() == "e"){
       Console.WriteLine("\nYour number is "+c);
       return true;
    }
    else {
        Console.WriteLine("\nInvalid Answer, Try again.");
        guess_number(a1,a2);
        return true;
    }
}
