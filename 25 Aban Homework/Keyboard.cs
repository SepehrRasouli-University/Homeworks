class Keyboard{
    public static int GetNumber(){
        Console.WriteLine("Please Input Number > ");
        string input = Console.ReadLine();
        int number;
        while(!int.TryParse(input,out number)){
            Console.Clear();
            Console.WriteLine("Invalid Number, Plesae try again.");
            GetNumber();
        }
        return number;
    }

    public static string GetHomeKey(string options){
        char input = Console.ReadKey().KeyChar;
        if (input.ToString() == ConsoleKey.Escape.ToString()){
            Environment.Exit(0);
            return "";
        }
        while(!options.Contains(input.ToString().ToLower())){
            Console.Beep();
            GetHomeKey(options);
        }
        return input.ToString().ToLower();
    }   

}