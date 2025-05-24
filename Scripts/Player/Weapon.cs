using Godot;
using System;

/// <summary>
/// Handles mainly weapons sprites and contact with enemy
/// </summary>
public partial class Weapon : Node
{
    private Area2D collision_area;
    private Sprite2D weapon_sprite;
    // public override void _Ready()
    // {
    //     weapon_sprite = GetNode<Sprite2D>("%weapon_sprite");
    //     collision_area = GetNode<Area2D>("%weapon_hitbox");
    // }

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
}
//Texture2D weapon_texture, 