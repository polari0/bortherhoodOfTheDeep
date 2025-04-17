using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class LittleHelper : Node2D
{

    private AnimatedSprite2D helper_animation;

    private Player_controller player_controller;

    public override void _Ready()
    {
         helper_animation = GetNode<AnimatedSprite2D>("%helper_animation");
         player_controller = (Player_controller)GetParent().GetParent().GetParent();
         player_controller.AnimationChanged += _On_parent_animation_changed;
    }
    

    private void _On_parent_animation_changed(String newAnimation, bool direction)
    {
        helper_animation.Play(newAnimation);
        helper_animation.FlipH = direction;
    }

    // public override void _PhysicsProcess(double delta)
    // {
    //     MoveAndCollide(player_controller.move_direction);
    // }

}
