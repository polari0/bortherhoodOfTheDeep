using Godot;
using System;

public partial class BasicMelee : Player_controller
{

    internal AnimationPlayer attack_Animation;
    private Area2D attack_area;

    internal override void Attack()
    {
        player_animation.Play("Attack");
        attack_Animation.Play("attack");
    }

    internal override void Setup_player()
    {
        attack_Animation = GetNode<AnimationPlayer>("%Attack_Animation");
        attack_area = GetNode<Area2D>("%Attack_Area");
    }

    public void _on_attack_area_area_entered(Area2D area)
    {

    }

    public override void Take_damage()
    {
        player_stats["Health"] = (float)(player_stats["Health"]) - 10f;
        GD.Print("Damage taken");
    }
}
