using Godot;
using System;
using System.ComponentModel;

public partial class CharacterAbilityBase : Node2D
{
    [Export]
    public Timer abilityTimer;

    [Export]
    public Node2D parent;

    internal virtual void useAbility() { }

}

