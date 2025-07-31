using Godot;
using System;

public partial class CharacterAbilityBase : Node2D
{
    [Export]
    public Timer abilityTimer;


    internal virtual void useAbility() { }

}

