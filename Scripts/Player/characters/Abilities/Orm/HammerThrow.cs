using Godot;
using System;

public partial class HammerThrow : CharacterAbilityBase
{

    internal override void useAbility()
    {
        for (int i = 0; i <= 10; i++)
        {
            
        }
    }

    public void OnAbilityTimerTimeOut()
    {
        useAbility();
    }
}
