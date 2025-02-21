internal enum EmployeeMaxExtraHours
{
    Simple = 50,
    Senior = 60,
    Manager = 70,
    HeadOfDepartment = 80,
    Deputy = 90,
    CEO = 100,
    
}
internal enum EmployeeRatios
{
    Simple = 2,
    Senior = 4,
    Manager = 6,
    HeadOfDepartment = 8,
    Deputy = 10,
    CEO = 12,
}

internal enum WorkingHours
{
    Simple = 30,
    Senior = 35,
    Manager = 40,
    HeadOfDepartment = 45,
    Deputy = 50,
    CEO = 55,
}

internal enum InsuranceDuration
{
    Simple = 3,
    Senior = 4,
    Manager = 5,
    HeadOfDepartment = 5,
    Deputy = 6,
    CEO = 7,
}

internal enum BaseSalaryRange
{
    Simple = 100,
    Senior = 300,
    Manager = 500,
    HeadOfDepartment = 600,
    Deputy = 700,
    CEO = 800,

}

public static class NationalCodeValidator
{
    public static bool IsNationalCodeValid(long nationalCode)
    {
        string codeStr = nationalCode.ToString("D10");
        if (codeStr.Distinct().Count() == 1) return false;

        int checkDigit = codeStr[9] - '0';
        int sum = 0;

        for (int i = 0; i < 9; i++)
        {
            sum += (codeStr[i] - '0') * (10 - i);
        }

        int remainder = sum % 11;

        return (remainder == 0 && checkDigit == 0) ||
               (remainder == 1 && checkDigit == 1) ||
               (remainder > 1 && checkDigit == 11 - remainder);
    }
}


public interface IEmployee
{
    decimal CalculateSalary();
}

internal abstract class EmployeeBase : IEmployee
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public required long NationalCode {get; set; }
    public int Level { get; set; }
    public decimal BaseSalary { get; set; }
    public int TotalHoursInMonth { get; set; }
    public int ExtraTimePerHours { get; set; }

    protected EmployeeBase(string firstName, string lastName, int level, decimal baseSalary, int totalHours, int extraTime)
    {
        FirstName = firstName;
        LastName = lastName;
        Level = level;
        BaseSalary = baseSalary;
        TotalHoursInMonth = totalHours;
        ExtraTimePerHours = extraTime;
    }

    public virtual decimal CalculateSalary()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} - National Code: {NationalCode} - Salary: {CalculateSalary():C}";
    }
}

internal class SimpleEmployee : EmployeeBase
{
    public SimpleEmployee(string firstName, string lastName, int level, decimal baseSalary, int totalHours, int extraTime)
        : base(firstName, lastName, level, baseSalary, totalHours, extraTime)
    {
        if (extraTime > (int)EmployeeMaxExtraHours.Simple){
            throw new InvalidDataException("Extra time is more than the allowed maximum.");
        }
        else if (baseSalary > (int)BaseSalaryRange.Simple){
            throw new InvalidDataException("Base salary is more than maximum.");
        }
        else if ((totalHours-extraTime) > (int)WorkingHours.Simple){
            throw new InvalidDataException("Total working hours is more than the allowed maximum.");
        }
    }
}

internal class SeniorEmployee : EmployeeBase
{
    public SeniorEmployee(string firstName, string lastName, int level, decimal baseSalary,int totalHours, int extraTime)
        : base(firstName, lastName, level, baseSalary, totalHours, extraTime)
    {
        if (extraTime > (int)EmployeeMaxExtraHours.Senior){
            throw new InvalidDataException("Extra time is more than the max extra hours for the simple employee type.");
        }
        else if (baseSalary > (int)BaseSalaryRange.Senior){
            throw new InvalidDataException("Base salary is more than maximum.");
        }
        else if ((totalHours-extraTime) > (int)WorkingHours.Senior){
            throw new InvalidDataException("Total working hours is more than the allowed maximum.");
        }
    }
}

internal class ManagerEmployee : EmployeeBase
{
    public ManagerEmployee(string firstName, string lastName, int level, decimal baseSalary,int totalHours, int extraTime)
        : base(firstName, lastName, level, baseSalary, totalHours, extraTime)
    {
        if (extraTime > (int)EmployeeMaxExtraHours.Manager){
            throw new InvalidDataException("Extra time is more than the max extra hours for the simple employee type.");
        }
        else if (baseSalary > (int)BaseSalaryRange.Manager){
            throw new InvalidDataException("Base salary is more than maximum.");
        }
        else if ((totalHours-extraTime) > (int)WorkingHours.Manager){
            throw new InvalidDataException("Total working hours is more than the allowed maximum.");
        }
    }
}
internal class HeadOfDepartmentEmployee : EmployeeBase
{
    public HeadOfDepartmentEmployee(string firstName, string lastName, int level, decimal baseSalary, int totalHours, int extraTime)
        : base(firstName, lastName, level, baseSalary, totalHours, extraTime)
    {
        if (extraTime > (int)EmployeeMaxExtraHours.HeadOfDepartment){
            throw new InvalidDataException("Extra time is more than the max extra hours for the simple employee type.");
        }
        else if (baseSalary > (int)BaseSalaryRange.HeadOfDepartment){
            throw new InvalidDataException("Base salary is more than maximum.");
        }
        else if ((totalHours-extraTime) > (int)WorkingHours.HeadOfDepartment){
            throw new InvalidDataException("Total working hours is more than the allowed maximum.");
        }
    }
}

internal class DeputyEmployee : EmployeeBase
{
    public DeputyEmployee(string firstName, string lastName, int level, decimal baseSalary, int totalHours, int extraTime)
        : base(firstName, lastName, level, baseSalary, totalHours, extraTime)
    {
        if (extraTime > (int)EmployeeMaxExtraHours.Deputy){
            throw new InvalidDataException("Extra time is more than the max extra hours for the simple employee type.");
        }
        else if (baseSalary > (int)BaseSalaryRange.Deputy){
            throw new InvalidDataException("Base salary is more than maximum.");
        }
        else if ((totalHours-extraTime) > (int)WorkingHours.Deputy){
            throw new InvalidDataException("Total working hours is more than the allowed maximum.");
        }
    }
}

internal class CEOEmployee : EmployeeBase
{
    public CEOEmployee(string firstName, string lastName, int level, decimal baseSalary, int totalHours, int extraTime)
        : base(firstName, lastName, level, baseSalary, totalHours, extraTime)
    {
        if (extraTime > (int)EmployeeMaxExtraHours.CEO){
            throw new InvalidDataException("Extra time is more than the max extra hours for the simple employee type.");
        }
        else if (baseSalary > (int)BaseSalaryRange.CEO){
            throw new InvalidDataException("Base salary is more than maximum.");
        }
        else if ((totalHours-extraTime) > (int)WorkingHours.CEO){
            throw new InvalidDataException("Total working hours is more than the allowed maximum.");
        }
    }
}


public class EmployeeFactory
{
    public static IEmployee CreateEmployee(string type, string firstName, string lastName, long nationalCode, int level,
        decimal baseSalary, int totalHours, int extraTime)
    {
        if (NationalCodeValidator.IsNationalCodeValid(Convert.ToInt64(nationalCode))){
            return type switch
            {
                "simple" => new SimpleEmployee(firstName, lastName, level, baseSalary, totalHours, extraTime)
                {
                    NationalCode = nationalCode
                },
                "senior" => new SeniorEmployee(firstName, lastName, level, baseSalary, totalHours, extraTime)
                {
                    NationalCode = nationalCode
                },
                "manager" => new SeniorEmployee(firstName, lastName, level, baseSalary, totalHours, extraTime)
                {
                    NationalCode = nationalCode
                },
                "headofdepartment" => new SeniorEmployee(firstName, lastName, level, baseSalary, totalHours, extraTime)
                {
                    NationalCode = nationalCode
                },
                "deputy" => new SeniorEmployee(firstName, lastName, level, baseSalary, totalHours, extraTime)
                {
                    NationalCode = nationalCode
                },
                "ceo" => new SeniorEmployee(firstName, lastName, level, baseSalary, totalHours, extraTime)
                {
                    NationalCode = nationalCode
                },
                _ => throw new ArgumentException("Invalid employee type")
            };
        }
        else{
            throw new InvalidDataException("National code is invalid.");
        }

    }
}

