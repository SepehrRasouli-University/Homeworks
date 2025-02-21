class Program
{
    static void Main()
    {
        var employees = new Dictionary<long, IEmployee>();
        var validEmployeeTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Simple", "Senior", "Manager", "HeadOfDepartment", "Deputy", "CEO"
        };
        while (true)
        {
            foreach (var kvp in employees)
            {
                Console.WriteLine($"National Code: {kvp.Key}, Employee: {kvp.Value}");
            }

            Console.WriteLine("Would you like to calculate salary for any employee? (y/n): ");
            string calculateSalaryResponse = Keyboard.GetKey("yn");
            if (calculateSalaryResponse == "y")
            {
                Console.Write("Enter National Code to calculate salary: ");
                long code = Convert.ToInt64(Console.ReadLine());
                if (employees.ContainsKey(code))
                {
                    Console.WriteLine($"Salary for {employees[code]} is: {employees[code].CalculateSalary():C}");
                }
                else
                {
                    Console.WriteLine("Employee not found.");
                }
            }
            else{
                Console.WriteLine("Enter employee details or 'exit' to quit:");

                Console.Write("Employee Type (simple, senior, manager, headofdepartment, deputy, ceo): ");
                string type = Keyboard.GetKey("abcdefghijklmnopqrstuvwxyz");

                if (!validEmployeeTypes.Contains(type)){
                    continue;
                }
                
                if (type == "exit") break;

                Console.Write("First Name: ");
                string firstName = Keyboard.GetKey("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

                Console.Write("Last Name: ");
                string lastName = Keyboard.GetKey("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ");

                Console.Write("National Code (10 digits): ");
                long nationalCode = Convert.ToInt64(Keyboard.GetKey("1234567890"));

                Console.Write("Level: ");
                int level = int.Parse(Keyboard.GetKey("1234567890"));

                Console.Write("Base Salary: ");
                decimal baseSalary = decimal.Parse(Keyboard.GetKey("1234567890."));

                Console.Write("Total Hours in Month: ");
                int totalHours = int.Parse(Keyboard.GetKey("1234567890"));

                Console.Write("Extra Time Hours: ");
                int extraTime = int.Parse(Keyboard.GetKey("1234567890"));

                try
                {
                    IEmployee employee = EmployeeFactory.CreateEmployee(type, firstName, lastName, nationalCode, level, baseSalary, totalHours, extraTime);
                    employees[nationalCode] = employee;
                    Console.WriteLine("Employee added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}

