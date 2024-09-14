using TurnBasedCombat;

// TODO: Add prompts to create player and foe character, default constructore otherwise
Unit player = new Unit(100, 20, 12, "Lightning Lord", Affiliation.Ally);
Unit foe = new Unit(125, 15, 7, "Dark Knight", Affiliation.Foe);
Random random = new Random(); // for determing foe's choice


//Print out the player and foe's HP values in a turn (with colors);
void PrintHpStatus()
{
    player.PrintUnitHp();
    Console.Write(", ");
    foe.PrintUnitHp();
    Console.Write("\n");
}

// TODO: Write function/class to perform turn orders
// TODO: Write turn orders to be compatible for 2-n (4) players
while (!player.IsDead && !foe.IsDead)
{
    PrintHpStatus();
    Console.WriteLine(player.UnitName + "'s Turn. What will they do?");
    string choice = Console.ReadLine();

    if (choice == "a")
    {
        player.Attack(foe);
    }
    else
    {
        player.Heal();
    }

    if (player.IsDead || foe.IsDead)
    {
        break;
    }

    PrintHpStatus();
    Console.WriteLine(foe.UnitName + "'s Turn. What will they do?");

    int rand = random.Next(0, 2);
    if (rand == 0)
    {
        foe.Attack(player);
    }
    else
    {
        foe.Heal();
    }
}