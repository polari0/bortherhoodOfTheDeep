using Godot;
using System;
using System.ComponentModel;

public partial class CharacterAbilityBase : Node2D
{
    [Export]
    public Timer abilityTimer;

    [Export]
    public Node2D parent;

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
        GD.Print((float)b.player_stats["AbilityDamage"]);
    }
}

