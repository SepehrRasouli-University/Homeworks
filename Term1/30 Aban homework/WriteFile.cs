
public interface Itxt{
    void WriteText(string data,string filepath);
    void ReadText(string filepath);
}

public interface Iini{
    void WriteText(string data,string filepath);
    void ReadText(string filepath);
}

public interface Icsv{
    void WriteText(string data,string filepath);
    void ReadText(string filepath);
}

public class TxtWriter : Itxt{
    public TxtWriter(){
    }
    public void WriteText(string filepath,string data){
        File.AppendAllText(filepath,data);
    }

    public void ReadText(string filepath){
        File.ReadAllText(filepath);
    }
}

public class IniWriter : Iini{
    public IniWriter(){
    }
    public void WriteText(string data,string filepath){
        File.AppendAllText(filepath,data);
    }

    public void ReadText(string filepath){
        File.ReadAllText(filepath);
    }
}

public class CsvWriter : Icsv{
    public CsvWriter(){
    }
    public void WriteText(string data,string filepath){
        File.AppendAllText(filepath,data);
    }

    public void ReadText(string filepath){
        File.ReadAllText(filepath);
    }
}