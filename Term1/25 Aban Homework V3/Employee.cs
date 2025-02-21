using System.Reflection.Metadata.Ecma335;
using System.Security.Permissions;

public class Employee{
    public string _FirstName;
    public string _LastName;
    public long _PhoneNumber;
    public long _CodeMeli;
    public bool _IsMarried;
    public float _BaseSalary;
    public Employee(
        
        string FirstName,string LastName,long PhoneNumber,long CodeMeli,bool IsMarried,float BaseSalary
        ){
            _FirstName = FirstName;
            _LastName = LastName;
            _PhoneNumber = PhoneNumber;
            _CodeMeli = CodeMeli;
            _IsMarried = IsMarried;
            _BaseSalary = BaseSalary;
        }
    public double CalculateSalary(
        float NumberOfMonths, int WorkingHours, 
        double FamilySalary, float OverTime,float EmployeeRate){

        double FinalSalary = (NumberOfMonths*WorkingHours*_BaseSalary)+(OverTime*_BaseSalary*EmployeeRate);
        if (_IsMarried){
            FinalSalary += FamilySalary;
        }
        return FinalSalary;
    }
    public new virtual string ToString(){
        throw new NotImplementedException();
    }

    protected string StringPrepare(string EmployeeType,string EmployeeWork){
        string BaseText = $"{FullName} Is a {EmployeeType}, Their role is {EmployeeWork}, Thier Phone number is {_PhoneNumber:D11}, Their Code-e Meli is {_CodeMeli:D10},";
        if (_IsMarried){
            BaseText += $" They are married, ";
        }
        else{
            BaseText += $" They aren't married, ";
        }
        BaseText+= $"And their Base salary is {_BaseSalary:C}";
        return BaseText;
    }

    public string FirstName{
        get {return _FirstName;}
    }
    public string LastName{
        get {return _LastName;}
    }
    public string FullName{
        get {return $"{_FirstName} {_LastName}";}
    }
    public long PhoneNumber{
        get {return PhoneNumber;}
    }
    public long CodeMeli{
        get {return _CodeMeli;}
    }
    public bool IsMarried{
        get {return _IsMarried;}
    }
    public float BaseSalary{
        get {return _BaseSalary;}
    }

}

public class SimpleEmployee:Employee{
    private float _EmployeeRate = 1.5f;
    public SimpleEmployee(
        string FirstName,string LastName,long PhoneNumber,
        long CodeMeli,bool IsMarried,float BaseSalary
    ):base(FirstName,LastName,PhoneNumber,CodeMeli,IsMarried,BaseSalary){}

    public double CalculateSalary(
        float NumberOfMonths, int WorkingHours, 
        double FamilySalary, float OverTime
    ){
        return base.CalculateSalary(NumberOfMonths,WorkingHours,FamilySalary,OverTime,_EmployeeRate);
    }

    public override string ToString()
    {
        return base.StringPrepare("SimpleEmployee","Simple employee works");
    }

    public float EmployeeRate{
        get {return _EmployeeRate;}
    }

}

public class ServiceEmployee:Employee{
    private float _EmployeeRate = 2.5f;
    public ServiceEmployee(
        string FirstName,string LastName,long PhoneNumber,
        long CodeMeli,bool IsMarried,float BaseSalary
    ):base(FirstName,LastName,PhoneNumber,CodeMeli,IsMarried,BaseSalary){}

    public double CalculateSalary(
        float NumberOfMonths, int WorkingHours, 
        double FamilySalary, float OverTime
    ){
        return base.CalculateSalary(NumberOfMonths,WorkingHours,FamilySalary,OverTime,_EmployeeRate);
    }

    public override string ToString()
    {
        return base.StringPrepare("ServiceEmployee","Service Works");
    }

    public float EmployeeRate{
        get {return _EmployeeRate;}
    }
    
}

public class ManagerEmployee:Employee{
    private float _EmployeeRate = 4f;
    public ManagerEmployee(
        string FirstName,string LastName,long PhoneNumber,
        long CodeMeli,bool IsMarried,float BaseSalary
    ):base(FirstName,LastName,PhoneNumber,CodeMeli,IsMarried,BaseSalary){}

    public double CalculateSalary(
        float NumberOfMonths, int WorkingHours, 
        double FamilySalary, float OverTime
    ){
        return base.CalculateSalary(NumberOfMonths,WorkingHours,FamilySalary,OverTime,_EmployeeRate);
    }

    public override string ToString()
    {
        return base.StringPrepare("ManagerEmployee","Managment");
    }

    public float EmployeeRate{
        get {return _EmployeeRate;}
    }
    
}

public class CEOEmployee:Employee{
    private float _EmployeeRate = 5f;
    public CEOEmployee(
        string FirstName,string LastName,long PhoneNumber,
        long CodeMeli,bool IsMarried,float BaseSalary
    ):base(FirstName,LastName,PhoneNumber,CodeMeli,IsMarried,BaseSalary){}

    public double CalculateSalary(
        float NumberOfMonths, int WorkingHours, 
        double FamilySalary, float OverTime
    ){
        return base.CalculateSalary(NumberOfMonths,WorkingHours,FamilySalary,OverTime,_EmployeeRate);
    }

    public override string ToString()
    {
        return base.StringPrepare("CEOEmployee","CEO works");
    }

    public float EmployeeRate{
        get {return _EmployeeRate;}
    }
    
}