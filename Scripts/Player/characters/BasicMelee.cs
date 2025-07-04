using Godot;
using System;
using System.Collections.Generic;

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
        GetCharacterStats(Global.ChosenCharacterID);
    }

    private void temp()
    {
        bool test;
        bool test2;
        test = player_stats.TryAdd("Health", 100f);
        test2 = player_stats.TryAdd("Damage", 4f);
        if (test == false || test2 == false)
        {
            GD.Print(test);
            GD.Print(test2);
        }
    }

    public void _on_attack_area_area_entered(Area2D area)
    {
        GD.Print("Got here");
        if (area is BaseEnemy)
        {
            GD.Print("Enemy detected");
            BaseEnemy target = (BaseEnemy)area;
            target.Take_damage((float)player_stats["Damage"]);
        }
    }

    public override void Take_damage(float damage)
    {
        if (canTakeDamage)
        {
            player_stats["Health"] = (float)(player_stats["Health"]) - damage;
            GD.Print("Damage taken");
            if ((float)player_stats["Health"] < 0f)
                die();
        }
    }

    internal override void GetCharacterStats(int characterID)
    {
        base.GetCharacterStats(characterID);
        GD.Print("Also do this");
    }
}
