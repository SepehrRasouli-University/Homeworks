void TakeInput(){
     char character = Console.ReadKey().KeyChar;
     if (!char.IsNumber(character)){
          TakeInput();
     }
     Console.Clear();
     Console.WriteLine("Invalid Character");
     Console.Write("Enter Character > ");
     TakeInput();
}
Console.Write("Enter Character > ");
TakeInput();
