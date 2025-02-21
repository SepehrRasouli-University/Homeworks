
public class Keyboard{
    public static string GetInput(){
        List<string> Input = new List<string>(){};
        bool ToContinue = true;
        while(ToContinue){
            Console.WriteLine($"> {String.Join("",Input)}");
            System.ConsoleKeyInfo input = System.Console.ReadKey();
            if (input.Key.Equals(ConsoleKey.Enter) && Input.Count > 0){
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

    public static string GetKey(string AllowedKeyStrokes){
        List<string> Input = new List<string>(){};
        bool ToContinue = true;
        while(ToContinue){
            Console.WriteLine($"> {String.Join("",Input)}");
            System.ConsoleKeyInfo input = System.Console.ReadKey();
            System.Console.WriteLine(input.KeyChar);
            System.Console.WriteLine(Input.Count);
            if (input.Key.Equals(ConsoleKey.Enter) && Input.Count > 0){
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
            if (!AllowedKeyStrokes.Contains(input.KeyChar.ToString())){
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
}