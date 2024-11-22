using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

class Cipher{
    protected string alphabet = "abcdefghijklmnopqrstuvwxyz";
    public string _Sender;
    public string _Reciever;
    public string _Data;
    public Cipher(string Sender,string Reciever,string Data){
        _Sender = Sender;
        _Reciever = Reciever;
        _Data = Data;
    }

    protected int FindNumericalValueOfSenderAndReciever(string sender,string reciever,int type){
        int RecieverNumericalValue = 0;
        int SenderNumericalValue = 0;
        foreach(char c in reciever.ToLower()){
            RecieverNumericalValue += alphabet.IndexOf(c);
        }
        foreach(char c in sender.ToLower()){
            SenderNumericalValue += alphabet.IndexOf(c);
        }
        if (type == 0) return RecieverNumericalValue + SenderNumericalValue;
        else{
            return RecieverNumericalValue*SenderNumericalValue/RecieverNumericalValue+SenderNumericalValue;
        }
    }
}
class Encode : Cipher{
    public int _Type;
    public Encode(string Sender,string Reciever,string Data, int type) : base(Sender,Reciever,Data){
        _Type = type;
    }
    public Tuple<string,string> EncodeData(){
        string EncodedData = "";
        int NumericalValueOfSenderAndReciever = base.FindNumericalValueOfSenderAndReciever(_Sender,_Reciever,_Type);
        int NumberOfShifts = NumericalValueOfSenderAndReciever % 26;

        foreach (char c in _Data){
            if (!alphabet.Contains(c.ToString().ToLower())){
                EncodedData += c;
                continue;
            }
            int FirstPosition = alphabet.IndexOf(c.ToString().ToLower());
            if (FirstPosition + NumberOfShifts >= alphabet.Length){
                if (char.IsUpper(c)){
                    EncodedData += alphabet.ElementAt(Math.Abs(alphabet.Length - (FirstPosition + NumberOfShifts))).ToString().ToUpper();
                    continue;
                }
                EncodedData += alphabet.ElementAt(Math.Abs(alphabet.Length - (FirstPosition + NumberOfShifts)));
                continue;
            }
            else{
                if (char.IsUpper(c)){
                    EncodedData += alphabet.ElementAt(FirstPosition+NumberOfShifts).ToString().ToUpper();
                    continue;
                }
                EncodedData += alphabet.ElementAt(FirstPosition+NumberOfShifts);
                continue;
            }
        }
        return new Tuple<string,string>(EncodedData,_Data.ToLower().GetHashCode().ToString());
    }
}

class Decode : Cipher{
    public string _Hash;
    public Decode(string Sender, string Reciever,string Data, string Hash):base(Sender,Reciever,Data){
        _Hash = Hash;
    }

    public string DecodeData(){
        int Type1Shifts = base.FindNumericalValueOfSenderAndReciever(_Sender,_Reciever,0) % 26;
        int Type2Shifts = base.FindNumericalValueOfSenderAndReciever(_Sender,_Reciever,1) % 26;
        string DecodedDataWithType1Shifts = DataDecoder(_Data,Type1Shifts);
        string DecodedDataWithType2Shifts = DataDecoder(_Data,Type2Shifts);
        if (DecodedDataWithType1Shifts.ToLower().GetHashCode().ToString() == _Hash){
            return DecodedDataWithType1Shifts;
        }
        else{
            return DecodedDataWithType2Shifts;
        }
    }

    private string DataDecoder(string data,int NumberOfShifts){
        // This is done to enforce the DRY principle.
        string DecodedData = "";
        foreach (char c in data){
            if (!alphabet.Contains(c.ToString().ToLower())){
                DecodedData += c;
                continue;
            }
            int FirstPosition = alphabet.IndexOf(c.ToString().ToLower());
            if (FirstPosition - NumberOfShifts < 0){
                if (char.IsUpper(c)){
                    DecodedData += alphabet.ElementAt(Math.Abs(alphabet.Length + (FirstPosition - NumberOfShifts))).ToString().ToUpper();
                    continue;
                }
                DecodedData += alphabet.ElementAt(Math.Abs(alphabet.Length + (FirstPosition - NumberOfShifts)));
                continue;
            }
            else{
                if (char.IsUpper(c)){
                    DecodedData += alphabet.ElementAt(FirstPosition-NumberOfShifts).ToString().ToUpper();
                    continue;
                }
                DecodedData += alphabet.ElementAt(FirstPosition-NumberOfShifts);
                continue;
            }
        }
        return DecodedData;
    }

}