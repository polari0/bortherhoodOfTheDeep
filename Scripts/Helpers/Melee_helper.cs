using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

public partial class Melee_helper : LittleHelper
{


    public override void setup(Dictionary<string, Variant> data)
    {
        helper_data = data;
        ID = (int)data["ID"];

    }

    public override void _Process(double delta)
    {
        Wait((double)helper_data["Weapon_AttackSpeed"]);
        swing();
    }
    private void swing()
    {
        Console.WriteLine("Attack");
    }


}
