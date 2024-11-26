public class Keyboard{
    public static string GetInput(){
        List<string> Input = new List<string>(){};
        bool ToContinue = true;
        while(ToContinue){
            Console.WriteLine($"> {String.Join("",Input)}");
            System.ConsoleKeyInfo input = System.Console.ReadKey();
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
        return Convert.ToInt32(String.Join("",NumbersList));
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

