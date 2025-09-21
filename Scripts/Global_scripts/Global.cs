using Godot;
using System;
using System.Collections.Generic;
using System.Net;

public static class Global
{
    public static int ChosenCharacterID = 1;


    public static Dictionary<int, string> statNames = new Dictionary<int, string>
    {
        {0, "Health"},
        {1, "Speed"},
        {2, "Damage"},
        {3, "Attack_Range"},
        {4, "Attack_Speed"},
        {5, "AbilityDamage"}
    };
    
}
