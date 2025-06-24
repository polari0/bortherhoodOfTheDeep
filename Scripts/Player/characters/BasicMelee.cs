using Godot;
using System;

public partial class BasicMelee : Player_controller
{

    internal override void Attack()
    {
        player_animation.Play("Attack");
    }
}
