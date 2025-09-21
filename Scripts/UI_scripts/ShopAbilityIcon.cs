using Godot;
using System;
using System.Xml.Serialization;

public partial class ShopAbilityIcon : Control
{

    [Export]
    public TextureRect abilityIcon;
    [Export]
    public Label levelLablel;

    public void setupIcon(CharacterAbilityBase ability)
    {
        levelLablel.Text = ability.abilityLevel.ToString();
        abilityIcon.Texture = ability.getAbilityIcon();
    }

}
