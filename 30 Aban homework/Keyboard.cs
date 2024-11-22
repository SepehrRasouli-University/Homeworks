public class Keyboard{
    public static string GetInput(){
        List<string> Input = new List<string>(){};
        bool ToContinue = true;
        while(ToContinue){
            Console.WriteLine($"> {String.Join("",Input)}");
            System.ConsoleKeyInfo input = Console.ReadKey();
            if (input.Key.Equals(ConsoleKey.Enter) && Input.Count > 0){
                ToContinue = false;
                Console.Clear();
                break;
            }
            if (input.Key.Equals(ConsoleKey.Backspace)){
                if (Input.Count == 0){
                    continue;
                }
                Input.RemoveAt(Input.Count-1);
                Console.Clear();
                continue;
            }
            else{
                Input.Add(input.KeyChar.ToString());
                Console.Clear();
                continue;
            }
        }
        return String.Join("",Input);

    }

    public static int GetNumber(){
        bool ToContinue = true;
        string ValidatedCharacter = "";
        while(ToContinue){
            System.ConsoleKey input = Console.ReadKey(true).Key;
            if(!"01".Contains(input.ToString().ToLower())){
                continue;
            }
            ToContinue = false;
            ValidatedCharacter = input.ToString().ToLower();
        }
        return Convert.ToInt16(ValidatedCharacter);
    }   

    public static string GetFileExtension(){
        System.Console.WriteLine("Enter the type of file to write data to [ini,txt,csv]> ");
        string FileExtension = GetInput();
        if (FileExtension == "ini"){
            return "ini";
        }
        else if (FileExtension == "txt"){
            return "txt";
        }
        else if (FileExtension == "csv"){
            return "csv";
        }
        else{
            Console.Clear();
            System.Console.WriteLine("Invalid File extension");
            GetFileExtension();
            return "";
        }
    }
}

