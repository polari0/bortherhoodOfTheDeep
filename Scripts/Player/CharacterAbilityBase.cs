using Godot;
using System;
using System.ComponentModel;

public partial class CharacterAbilityBase : Node2D
{
    [Export]
    public Timer abilityTimer;

    [Export]
    public Node2D parent;

    [Export]
    public int abilityID;

    public int abilityLevel;

    internal float abilityDamage;

    internal virtual void useAbility() { }

    public override void _Ready()
    {
        parent = GetParent<Node2D>();
        Player_controller player = parent.GetParent<Player_controller>();
        player.AbilitySetUp += GetAbilityDamage;
    }

    internal void GetAbilityDamage()
    {
        Player_controller b = parent.GetParent<BasicMelee>();
        abilityDamage = (float)b.player_stats["AbilityDamage"];
    }

    public Texture2D getAbilitiIcon()
    {
        Image image = new Image();
        Texture2D texture2D;
        string query = "SELECT a.AbilitySprite AS image FROM Abilities a WHERE a.ID = ?";
        Godot.Collections.Dictionary<String, Byte[]> a = (Godot.Collections.Dictionary<String, Byte[]>)DataBase.query_with_bindings(query, [abilityID])[0];
        image.LoadPngFromBuffer((Byte[])a["image"]);
        texture2D = ImageTexture.CreateFromImage(image);
        return texture2D;
    }
}

