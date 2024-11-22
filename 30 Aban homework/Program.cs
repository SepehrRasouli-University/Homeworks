void main(){
    Console.Write("Enter Sender's name > ");
    string Sender = Keyboard.GetInput();

    Console.Write("Enter Reciever's name > ");
    string Reciever = Keyboard.GetInput();

    Console.Write("Enter your message > ");
    string Message = Keyboard.GetInput();

    Console.Write("What type of key calculation to use ? [0 for Sender+Reciever, 1 for Sender*Reciever/Sender+Reciever]");
    int Type = Keyboard.GetNumber();

    Encode encode = new Encode(Sender,Reciever,Message,Type);
    Tuple<string,string> EncodedData = encode.EncodeData();
    Decode decode = new Decode(Sender,Reciever,EncodedData.Item1,EncodedData.Item2);

    System.Console.WriteLine($"The encoded message is {EncodedData.Item1}");
    System.Console.WriteLine($"The decoded message is {decode.DecodeData()}");

    string FileExtension = Keyboard.GetFileExtension();
    if (FileExtension == "txt"){
        TxtWriter txtWriter = new TxtWriter();
       txtWriter.WriteText(EncodedData.Item1,$"encoded.{FileExtension}");
    }
    else if (FileExtension == "ini"){
        IniWriter iniWriter = new IniWriter();
        iniWriter.WriteText(EncodedData.Item1,$"encoded.{FileExtension}");
    }
    else{
        CsvWriter csvWriter = new CsvWriter();
        csvWriter.WriteText(EncodedData.Item1,$"encoded.{FileExtension}");
    }

    System.Console.WriteLine($"The decoded and the encoded message were written to decoded.{FileExtension} and encoded.{FileExtension}");
}
main();