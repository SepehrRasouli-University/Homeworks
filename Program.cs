using System.Data.SQLite;
using System;
using System.IO;

string dbFilePath = "animalsDB.db";
main();

void main(){
    showAnimals();
    System.Console.WriteLine("What action would you like to take :");
    System.Console.WriteLine("[A] Add Animal [E] Edit Animal, [R] Remove animal, [V] View specific animal");
    string input = Keyboard.GetKey("aerv").ToLower();
    System.Console.WriteLine(input);
    if (input == "a"){
        addAnimal();
    }
    if (input == "e"){
        editAnimalDataWithoutDatabaseChange();
    }
    if (input == "r"){
        deleteAnimal();
    }
    if (input == "v"){
        viewAnimal();
    }
}

void viewAnimal()
{
    Console.WriteLine("Please enter the ID of the animal you want to view > ");
    int id = Convert.ToInt32(Console.ReadLine());
    string query = "SELECT * FROM Animals WHERE ID = @ID;";
    using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
    {
        connection.Open();
        using (var command = new SQLiteCommand(query, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.Clear();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (reader[i] != DBNull.Value)
                        {
                            string columnName = reader.GetName(i);
                            string columnValue = reader[i].ToString();
                            Console.WriteLine($"{columnName}: {columnValue}");
                        }
                    }
                    Console.WriteLine("\nPress any key to exit this part...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("No animal found with the given ID.");
                }
            }
        }
    }
    main();
}


void deleteAnimal(){
    System.Console.WriteLine("Please enter the ID of the animal you want to change > ");
    int id = Convert.ToInt32(Keyboard.GetInput());
    string commandText = "DELETE FROM Animals WHERE ID = @ID";

    using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
    {
        connection.Open();

        using (var command = new SQLiteCommand(commandText, connection))
        {
            command.Parameters.AddWithValue("@ID", id);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                Console.WriteLine("Animal deleted successfully.");
            }
            else
            {
                Console.WriteLine("Animal not found.");
            }
        }
        string updateQuery = "UPDATE Animals SET ID = ID - 1 WHERE ID > @DeletedID";
        using (var updateCommand = new SQLiteCommand(updateQuery, connection))
        {
            updateCommand.Parameters.AddWithValue("@DeletedID", id);
            updateCommand.ExecuteNonQuery();
        }
    }
    main();
}

void editAnimalDataWithoutDatabaseChange(){
    System.Console.WriteLine("Please enter the ID of the animal you want to change > ");
    int id = Convert.ToInt32(Keyboard.GetInput());
    Dictionary<string, Type> speciesDictionary = new Dictionary<string, Type>
    {
        { "mammal", typeof(Mammal) },
        { "bird", typeof(Bird) },
        { "reptile", typeof(Reptile) },
        { "fish", typeof(Fish) },
        { "amphibian", typeof(Amphibian) },
        { "insect", typeof(Insect) }
    };
    using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;")){
        connection.Open();
        string species = GetAnimalSpeciesById(id, connection);
        Console.Write("Enter the name of the animal: ");
        string name = Keyboard.GetInput();

        Console.Write("Enter the age of the animal: ");
        int age = int.Parse(Keyboard.GetKey("0123456789"));


        var animalType = speciesDictionary[species];
        var animalProps = animalType.GetProperties()
                                    .Where(prop => prop.PropertyType == typeof(bool))
                                    .ToList();

        Dictionary<string, bool> answers = new Dictionary<string, bool>();
        foreach (var prop in animalProps)
        {
            Console.WriteLine("For each property answer y/n");
            Console.WriteLine($"{prop.Name}? (y/n)");
            string input = Keyboard.GetKey("yn");
            answers[prop.Name] = input == "y";
        }

        var parameters = new List<object> { name, species, age };
        parameters.AddRange(animalProps.Select(prop => (object)answers[prop.Name]));

        var animal = Activator.CreateInstance(animalType, parameters.ToArray());

        EditAnimal(id,(Animal)animal,connection);


    }
    main();

}

void addAnimal(){
    Console.Clear();
    
    Dictionary<string, Type> allowedSpecies = new Dictionary<string, Type>
    {
        { "mammal", typeof(Mammal) },
        { "bird", typeof(Bird) },
        { "reptile", typeof(Reptile) },
        { "fish", typeof(Fish) },
        { "amphibian", typeof(Amphibian) },
        { "insect", typeof(Insect) }
    };

    Console.WriteLine($"Please input the species of the animal you want to add. Allowed species are: {string.Join(", ", allowedSpecies.Keys)}");
    string outputSpecies = Keyboard.GetInput().ToLower();

    if (!allowedSpecies.ContainsKey(outputSpecies))
    {
        Console.WriteLine("Invalid species. Try again.");
        addAnimal();
        return;
    }

    Console.Write("Enter the name of the animal: ");
    string name = Keyboard.GetInput();

    Console.Write("Enter the age of the animal: ");
    int age = int.Parse(Keyboard.GetKey("0123456789"));


    var animalType = allowedSpecies[outputSpecies];
    var animalProps = animalType.GetProperties()
                                 .Where(prop => prop.PropertyType == typeof(bool))
                                 .ToList();

    Dictionary<string, bool> answers = new Dictionary<string, bool>();
    foreach (var prop in animalProps)
    {
        Console.WriteLine("For each property answer y/n");
        Console.WriteLine($"{prop.Name}? (y/n)");
        string input = Keyboard.GetKey("yn");
        answers[prop.Name] = input == "y";
    }

    var parameters = new List<object> { name, outputSpecies, age };
    parameters.AddRange(animalProps.Select(prop => (object)answers[prop.Name]));

    var animal = Activator.CreateInstance(animalType, parameters.ToArray());
    
    InsertAnimalToDatabase((Animal)animal);

    Console.WriteLine($"Animal added: {animal}");
    main();
}


void InsertAnimalToDatabase(Animal animal)
{
    using (var connection = new SQLiteConnection($"Data Source={dbFilePath};Version=3;"))
    {
        connection.Open();
        string commandText = @"";
        if (animal.Species == "mammal")
        {
            commandText = @"
            INSERT INTO Animals (
                Name, Species, Age, HasFur, IsWarmBlooded, GivesLiveBirth, HasMammaryGlands
            ) VALUES (
                @Name, @Species, @Age, @HasFur, @IsWarmBlooded, @GivesLiveBirth, @HasMammaryGlands
            );";
        }
        else if (animal.Species == "bird")
        {
            commandText = @"
            INSERT INTO Animals (
                Name, Species, Age, CanFly, HasFeathers, LaysEggs
            ) VALUES (
                @Name, @Species, @Age, @CanFly, @HasFeathers, @LaysEggs
            );";
        }
        else if (animal.Species == "reptile")
        {
            commandText = @"
            INSERT INTO Animals (
                Name, Species, Age, IsColdBlooded, HasScalySkin, Venomous
            ) VALUES (
                @Name, @Species, @Age, @IsColdBlooded, @HasScalySkin, @Venomous
            );";
        }
        else if (animal.Species == "fish")
        {
            commandText = @"
            INSERT INTO Animals (
                Name, Species, Age, HasGills, HasFins, IsSaltwater, CanBreatheUnderwater
            ) VALUES (
                @Name, @Species, @Age, @HasGills, @HasFins, @IsSaltwater, @CanBreatheUnderwater
            );";
        }
        else if (animal.Species == "amphibian")
        {
            commandText = @"
            INSERT INTO Animals (
                Name, Species, Age, CanLiveOnLand, CanLiveInWater, UndergoesMetamorphosis
            ) VALUES (
                @Name, @Species, @Age, @CanLiveOnLand, @CanLiveInWater, @UndergoesMetamorphosis
            );";
        }
        else
        {
            commandText = @"
            INSERT INTO Animals (
                Name, Species, Age, HasSixLegs, CanBite
            ) VALUES (
                @Name, @Species, @Age, @HasSixLegs, @CanBite
            );";
        }

        using (var command = new SQLiteCommand(commandText, connection))
        {
            command.Parameters.AddWithValue("@Name", animal.Name);
            command.Parameters.AddWithValue("@Species", animal.Species);
            command.Parameters.AddWithValue("@Age", animal.Age);

            AddBooleanProperties(command, animal);

            command.ExecuteNonQuery();
        }

        connection.Close();
    }
}

void AddBooleanProperties(SQLiteCommand command, Animal animal)
{
    var boolProperties = animal.GetType().GetProperties()
        .Where(prop => prop.PropertyType == typeof(bool))
        .ToList();

    foreach (var prop in boolProperties)
    {
        var value = (bool)prop.GetValue(animal);
        string paramName = "@" + prop.Name;
        command.Parameters.AddWithValue(paramName, value);
    }

}

void EditAnimal(int animalId, Animal updatedAnimal, SQLiteConnection connection)
{
    string species = GetAnimalSpeciesById(animalId, connection);
    if (string.IsNullOrEmpty(species))
    {
        throw new ArgumentException($"No animal found with ID: {animalId}");
    }

    string commandText = species switch
    {
        "mammal" => @"
            UPDATE Animals
            SET Name = @Name, Species = @Species, Age = @Age, 
                HasFur = @HasFur, IsWarmBlooded = @IsWarmBlooded, 
                GivesLiveBirth = @GivesLiveBirth, HasMammaryGlands = @HasMammaryGlands
            WHERE Id = @Id;
        ",
        "bird" => @"
            UPDATE Animals
            SET Name = @Name, Species = @Species, Age = @Age, 
                CanFly = @CanFly, HasFeathers = @HasFeathers, LaysEggs = @LaysEggs
            WHERE Id = @Id;
        ",
        "reptile" => @"
            UPDATE Animals
            SET Name = @Name, Species = @Species, Age = @Age, 
                IsColdBlooded = @IsColdBlooded, HasScalySkin = @HasScalySkin, 
                Venomous = @Venomous
            WHERE Id = @Id;
        ",
        "fish" => @"
            UPDATE Animals
            SET Name = @Name, Species = @Species, Age = @Age, 
                HasGills = @HasGills, HasFins = @HasFins, 
                IsSaltwater = @IsSaltwater, CanBreatheUnderwater = @CanBreatheUnderwater
            WHERE Id = @Id;
        ",
        "amphibian" => @"
            UPDATE Animals
            SET Name = @Name, Species = @Species, Age = @Age, 
                CanLiveOnLand = @CanLiveOnLand, CanLiveInWater = @CanLiveInWater, 
                UndergoesMetamorphosis = @UndergoesMetamorphosis
            WHERE Id = @Id;
        ",
        "insect" => @"
            UPDATE Animals
            SET Name = @Name, Species = @Species, Age = @Age, 
                HasSixLegs = @HasSixLegs, CanBite = @CanBite
            WHERE Id = @Id;
        ",
        _ => throw new ArgumentException($"Unsupported species type: {species}")
    };

    using var command = new SQLiteCommand(commandText, connection);
    command.Parameters.AddWithValue("@Id", animalId);
    command.Parameters.AddWithValue("@Name", updatedAnimal.Name);
    command.Parameters.AddWithValue("@Species", updatedAnimal.Species);
    command.Parameters.AddWithValue("@Age", updatedAnimal.Age);

    // Add species-specific parameters
    switch (updatedAnimal)
    {
        case Mammal mammal:
            command.Parameters.AddWithValue("@HasFur", mammal.HasFur);
            command.Parameters.AddWithValue("@IsWarmBlooded", mammal.IsWarmBlooded);
            command.Parameters.AddWithValue("@GivesLiveBirth", mammal.GivesLiveBirth);
            command.Parameters.AddWithValue("@HasMammaryGlands", mammal.HasMammaryGlands);
            break;

        case Bird bird:
            command.Parameters.AddWithValue("@CanFly", bird.CanFly);
            command.Parameters.AddWithValue("@HasFeathers", bird.HasFeathers);
            command.Parameters.AddWithValue("@LaysEggs", bird.LaysEggs);
            break;

        case Reptile reptile:
            command.Parameters.AddWithValue("@IsColdBlooded", reptile.IsColdBlooded);
            command.Parameters.AddWithValue("@HasScalySkin", reptile.HasScalySkin);
            command.Parameters.AddWithValue("@Venomous", reptile.Venomous);
            break;

        case Fish fish:
            command.Parameters.AddWithValue("@HasGills", fish.HasGills);
            command.Parameters.AddWithValue("@HasFins", fish.HasFins);
            command.Parameters.AddWithValue("@IsSaltwater", fish.IsSaltwater);
            command.Parameters.AddWithValue("@CanBreatheUnderwater", fish.CanBreatheUnderwater);
            break;

        case Amphibian amphibian:
            command.Parameters.AddWithValue("@CanLiveOnLand", amphibian.CanLiveOnLand);
            command.Parameters.AddWithValue("@CanLiveInWater", amphibian.CanLiveInWater);
            command.Parameters.AddWithValue("@UndergoesMetamorphosis", amphibian.UndergoesMetamorphosis);
            break;

        case Insect insect:
            command.Parameters.AddWithValue("@HasSixLegs", insect.HasSixLegs);
            command.Parameters.AddWithValue("@CanBite", insect.CanBite);
            break;

        default:
            throw new ArgumentException("Unsupported animal type");
    }


    command.ExecuteNonQuery();
}

// Helper method to get species by ID
string GetAnimalSpeciesById(int id, SQLiteConnection connection)
{
    string query = "SELECT Species FROM Animals WHERE Id = @Id;";
    using var command = new SQLiteCommand(query, connection);
    command.Parameters.AddWithValue("@Id", id);

    object result = command.ExecuteScalar();
    return result?.ToString();
}



void showAnimals(){
    
    if (File.Exists(dbFilePath)){
        using (var connection = new SQLiteConnection($"Data Source={dbFilePath}"))
        {
            connection.Open();
            var cmd = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Animals (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT,
                    Species TEXT,
                    Age INTEGER,
                    HasFur BOOLEAN,
                    IsWarmBlooded BOOLEAN,
                    GivesLiveBirth BOOLEAN,
                    HasMammaryGlands BOOLEAN,
                    CanFly BOOLEAN,
                    HasFeathers BOOLEAN,
                    LaysEggs BOOLEAN,
                    IsColdBlooded BOOLEAN,
                    HasScalySkin BOOLEAN,
                    Venomous BOOLEAN,
                    HasGills BOOLEAN,
                    HasFins BOOLEAN,
                    IsSaltwater BOOLEAN,
                    CanBreatheUnderwater BOOLEAN,
                    CanLiveOnLand BOOLEAN,
                    CanLiveInWater BOOLEAN,
                    UndergoesMetamorphosis BOOLEAN,
                    HasSixLegs BOOLEAN,
                    CanBite BOOLEAN
                )", connection);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("SELECT * FROM Animals", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader[i] != DBNull.Value)
                    {
                        string columnName = reader.GetName(i);
                        string columnValue = reader[i].ToString();
                        Console.WriteLine($"{columnName}: {columnValue}");
                    }
                }
                Console.WriteLine("\n==================================\n");
            }
        }
    }
    else{
        SQLiteConnection.CreateFile(dbFilePath);
        using (var connection = new SQLiteConnection($"Data Source={dbFilePath}"))
        {
            connection.Open();
            var cmd = new SQLiteCommand(@"
                CREATE TABLE IF NOT EXISTS Animals (
                    Id INTEGER PRIMARY KEY,
                    Name TEXT,
                    Species TEXT,
                    Age INTEGER,
                    HasFur BOOLEAN,
                    IsWarmBlooded BOOLEAN,
                    GivesLiveBirth BOOLEAN,
                    HasMammaryGlands BOOLEAN,
                    CanFly BOOLEAN,
                    HasFeathers BOOLEAN,
                    LaysEggs BOOLEAN,
                    IsColdBlooded BOOLEAN,
                    HasScalySkin BOOLEAN,
                    Venomous BOOLEAN,
                    HasGills BOOLEAN,
                    HasFins BOOLEAN,
                    IsSaltwater BOOLEAN,
                    CanBreatheUnderwater BOOLEAN,
                    CanLiveOnLand BOOLEAN,
                    CanLiveInWater BOOLEAN,
                    UndergoesMetamorphosis BOOLEAN,
                    HasSixLegs BOOLEAN,
                    CanBite BOOLEAN
                )", connection);
            cmd.ExecuteNonQuery();

            Console.WriteLine("Database and table created successfully.");
        }
    }
}

// Dictionary<string,List<Animal>> retrieveAllAnimalsFromDatabase()
// {
//     Dictionary<string,List<Animal>> animals = new Dictionary<string,List<Animal>>();
//     using (var connection = new SQLiteConnection("Data Source=animals.db"))
//     {
//         connection.Open();
//         var cmd = new SQLiteCommand("SELECT * FROM Animals", connection);
//         var reader = cmd.ExecuteReader();
//         while (reader.Read())
//         {
//             var id = Convert.ToInt32(reader["Id"]);
//             var name = reader["Name"].ToString();
//             var species = reader["Species"].ToString();
//             var age = Convert.ToInt32(reader["Age"]);

//             if (species == "Mammal")
//             {
//                 var mammal = new Mammal(
//                     name!,
//                     species,
//                     age,
//                     Convert.ToBoolean(reader["HasFur"]),
//                     Convert.ToBoolean(reader["IsWarmBlooded"]),
//                     Convert.ToBoolean(reader["GivesLiveBirth"]),
//                     Convert.ToBoolean(reader["HasMammaryGlands"])
//                 );
//                 animals["mammal"].Add(mammal);
//             }
//             else if (species == "Bird")
//             {
//                 var bird = new Bird(
//                     name!,
//                     species,
//                     age,
//                     Convert.ToBoolean(reader["CanFly"]),
//                     Convert.ToBoolean(reader["HasFeathers"]),
//                     Convert.ToBoolean(reader["LaysEggs"])
//                 );
//                 animals["bird"].Add(bird);
//             }
//             else if (species == "Reptile")
//             {
//                 var reptile = new Reptile(
//                     name!,
//                     species,
//                     age,
//                     Convert.ToBoolean(reader["IsColdBlooded"]),
//                     Convert.ToBoolean(reader["HasScalySkin"]),
//                     Convert.ToBoolean(reader["Venomous"])
//                 );
//                 animals["reptile"].Add(reptile);
//             }
//             else if (species == "Fish")
//             {
//                 var fish = new Fish(
//                     name!,
//                     species,
//                     age,
//                     Convert.ToBoolean(reader["HasGills"]),
//                     Convert.ToBoolean(reader["HasFins"]),
//                     Convert.ToBoolean(reader["IsSaltwater"]),
//                     Convert.ToBoolean(reader["CanBreatheUnderwater"])
//                 );
//                 animals["fish"].Add(fish);
//             }
//             else if (species == "Amphibian")
//             {
//                 var amphibian = new Amphibian(
//                     name!,
//                     species,
//                     age,
//                     Convert.ToBoolean(reader["CanLiveOnLand"]),
//                     Convert.ToBoolean(reader["CanLiveInWater"]),
//                     Convert.ToBoolean(reader["UndergoesMetamorphosis"])
//                 );
//                 animals["amphibian"].Add(amphibian);
//             }
//             else if (species == "Insect")
//             {
//                 var insect = new Insect(
//                     name!,
//                     species,
//                     age,
//                     Convert.ToBoolean(reader["HasSixLegs"]),
//                     Convert.ToBoolean(reader["CanBite"])
//                 );
//                 animals["insect"].Add(insect);
//             }
//         }
//     }
//     return animals;
// }

// void showAllAnimals(){
//     using (var connection = new SQLiteConnection("Data Source=animals.db"))
//     {
//         connection.Open();
//         var cmd = new SQLiteCommand("SELECT * FROM Animals", connection);
//         var reader = cmd.ExecuteReader();
//         while (reader.Read())
//         {
//             for (int i = 0; i < reader.FieldCount; i++)
//             {
//                 if (reader[i] != DBNull.Value)
//                 {
//                     string columnName = reader.GetName(i);
//                     string columnValue = reader[i].ToString();
//                     Console.WriteLine($"{columnName}: {columnValue}");
//                 }
//                 Console.WriteLine("\n==================================\n");
//             }
//         }
//     }
// }