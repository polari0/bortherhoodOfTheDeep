using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

/// <summary>
/// Handles mainly weapons sprites and contact with enemy
/// </summary>
public partial class Weapon : Node2D
{
    private Area2D collision_area;
    private Sprite2D weapon_sprite;
    // public override void _Ready()
    // {
    //     weapon_sprite = GetNode<Sprite2D>("%weapon_sprite");
    //     collision_area = GetNode<Area2D>("%weapon_hitbox");
    // }

    [Export]
    public Godot.Collections.Dictionary<String, Godot.Variant> helper_data;

    public void setup_weapon(bool direction_right)
    {
        weapon_sprite = GetNode<Sprite2D>("%weapon_sprite");
        collision_area = GetNode<Area2D>("%weapon_hitbox");
        if (!direction_right)
        {
            weapon_sprite.FlipH = true;
            collision_area.Rotation = -45;
        }
    }

    internal async void Wait(double wait_time)
    {
        await ToSignal(GetTree().CreateTimer(wait_time), "timeout");
    }


    private void swing()
    {
        Mathf.LerpAngle(Rotation, 360f, 10f);
    }
    public void _on_attack_timer_timeout()
    {
        GD.Print("Swung");
        swing();
    }

}
//Texture2D weapon_texture, 