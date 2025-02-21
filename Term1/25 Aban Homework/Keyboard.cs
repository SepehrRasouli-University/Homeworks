class Keyboard{
    public static int GetNumber(){
        List<string> numberslist = new List<string>(){""};
        bool ToContinue = true;
        while(ToContinue){
            Console.WriteLine($"> {String.Join("",numberslist)}");
            System.ConsoleKeyInfo input = Console.ReadKey();
            if(char.IsDigit(input.KeyChar)){
                Console.Clear();
                numberslist.Add(input.KeyChar.ToString());
                continue;
            }
            if (input.Key.Equals(ConsoleKey.Enter)){
                ToContinue = false;
                break;
            }
            else{
                Console.Clear();
                continue;
            }
        }
        return Convert.ToInt32(String.Join("",numberslist));

    }

    public static string GetHomeKey(string options){
        bool ToContinue = true;
        string ValidatedCharacter = "";
        while(ToContinue){
            System.ConsoleKey input = Console.ReadKey().Key;
            if (input.ToString() == "Escape"){
                Environment.Exit(0);
            }
            if(!options.Contains(input.ToString().ToLower())){
                continue;
            }
            ToContinue = false;
            ValidatedCharacter = input.ToString().ToLower();
        }
        return ValidatedCharacter;
    }   


}