using Godot;
using System;
using System.Numerics;

public partial class Player_controller : CharacterBody2D
{

    [Export]
    public float player_speed = 200;


    private Godot.Vector2 velocity; 
    private AnimatedSprite2D player_animation;

    public Godot.Vector2 move_direction;

    [Signal]
    public delegate void AnimationChangedEventHandler(string newAnimation, bool direction);

    public override void _Ready()
    {
        player_animation = GetNode<AnimatedSprite2D>("%Animation_player");
        player_animation.Play("Idle");
        base._Ready();
    }


    public override void _PhysicsProcess(double delta)
    {
        velocity = Input.GetVector("Move_Left", "Move_Right", "Move_Up", "Move_Down");
        move_direction = velocity * player_speed * (float)delta;
        if (Input.IsActionPressed("Move"))
            MoveAndCollide(move_direction);
            if (velocity.X < -0.01)
                player_animation.FlipH = true;
            else if (velocity.X > 0.01)
                player_animation.FlipH = false;
            else 
                player_animation.FlipH = player_animation.FlipH;
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("Move") && player_animation.Animation != "Walking")
            player_animation.Play("Walking");
            EmitSignal("AnimationChanged", player_animation.Animation, player_animation.FlipH);

        if (Input.IsActionJustReleased("Move") && player_animation.Animation == "Walking")
            player_animation.Play("Idle");
            EmitSignal("AnimationChanged", player_animation.Animation, player_animation.FlipH);
    }


    public override void _Process(double delta)
    {
        base._Process(delta);
    }
    

}
