// public class Animal
// {
    // private static int _nextId = 1;

    // public int Id { get; private set; }
//     public string Name { get; set; }
//     public string Species { get; set; }
//     public int Age { get; set; }
//     public int Count {get; set;}

//     // Constructor
//     public Animal(string name, string species, int age, int count)
//     {
//         Id = _nextId++;
//         Name = name;
//         Species = species;
//         Age = age;
//         Count = count;
//     }

//     public static void ResetId()
//     {
//         _nextId = 1;
//     }
// }

public class Animal
{
    public static int _nextId = 1;
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }

    // Constructor
    public Animal(string name, string species, int age)
    {
        Id = _nextId++;
        Name = name;
        Species = species;
        Age = age;

    }
}

public class Mammal : Animal
{
    public bool HasFur { get; set; }
    public bool IsWarmBlooded { get; set; }
    public bool GivesLiveBirth { get; set; }
    public bool HasMammaryGlands { get; set; }

    public Mammal(string name, string species, int age, bool hasFur, bool isWarmBlooded, bool givesLiveBirth, bool hasMammaryGlands)
        : base(name, species, age)
    {
        HasFur = hasFur;
        IsWarmBlooded = isWarmBlooded;
        GivesLiveBirth = givesLiveBirth;
        HasMammaryGlands = hasMammaryGlands;
    }
    public Mammal():this("default","mammal",0,false,false,false,false){}
}

public class Bird : Animal
{
    public bool CanFly { get; set; }
    public bool HasFeathers { get; set; }
    public bool LaysEggs { get; set; }

    public Bird(string name, string species, int age,bool canFly, bool hasFeathers, bool laysEggs)
        : base(name, species, age)
    {
        CanFly = canFly;
        HasFeathers = hasFeathers;
        LaysEggs = laysEggs;
    }
    public Bird():this("default","default",0,false,false,false){}
}

public class Reptile : Animal
{
    public bool IsColdBlooded { get; set; }
    public bool HasScalySkin { get; set; }
    public bool Venomous { get; set; }

    public Reptile(string name, string species, int age, bool isColdBlooded, bool hasScalySkin, bool venomous)
        : base(name, species, age)
    {
        IsColdBlooded = isColdBlooded;
        HasScalySkin = hasScalySkin;
        Venomous = venomous;
    }
    public Reptile():this("default","default",0,false,false,false){}
}

public class Fish : Animal
{
    public bool HasGills { get; set; }
    public bool HasFins { get; set; }
    public bool IsSaltwater { get; set; }
    public bool CanBreatheUnderwater { get; set; }

    public Fish(string name, string species, int age, bool hasGills, bool hasFins, bool isSaltwater, bool canBreatheUnderwater)
        : base(name, species, age)
    {
        HasGills = hasGills;
        HasFins = hasFins;
        IsSaltwater = isSaltwater;
        CanBreatheUnderwater = canBreatheUnderwater;
    }
    public Fish():this("default","default",0,false,false,false,false){}
}

public class Amphibian : Animal
{
    public bool CanLiveOnLand { get; set; }
    public bool CanLiveInWater { get; set; }
    public bool UndergoesMetamorphosis { get; set; }

    public Amphibian(string name, string species, int age, bool canLiveOnLand, bool canLiveInWater, bool undergoesMetamorphosis)
        : base(name, species, age)
    {
        CanLiveOnLand = canLiveOnLand;
        CanLiveInWater = canLiveInWater;
        UndergoesMetamorphosis = undergoesMetamorphosis;
    }
    public Amphibian():this("default","default",0,false,false,false){}
}

public class Insect : Animal
{
    public bool HasSixLegs { get; set; }
    public bool CanBite { get; set; }
    public Insect(string name, string species, int age, bool hasSixLegs, bool canBite)
        : base(name, species, age)
    {
        HasSixLegs = hasSixLegs;
        CanBite = canBite;
    }
    public Insect():this("default","default",0,false,false){}
}