using Godot;
using System;
using System.Xml.Serialization;

public partial class ShopAbilityIcon : Control
{

    public void setupIcon(CharacterAbilityBase ability)
    {
        GD.Print("ABILITY id");
        GD.Print(ability.abilityID);
    }

}
