using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using System;
using System.ComponentModel;
using System.Net;
using System.Numerics;
using System.Threading.Tasks;

/// <summary>
/// Handles the basic controlls of player character as well as basic animation handling
/// Setups the overidable functions for different character types to use.
/// </summary>
public partial class Player_controller : CharacterBody2D
{

    [Export]
    public float player_speed = 200;

    [Export]
    public Godot.Collections.Dictionary<String, Godot.Variant> player_stats;

    public Godot.Vector2 velocity;
    private int little_helper_count = 0;
    internal AnimatedSprite2D player_animation;
    private Array<Node> helper_positions;
    public Godot.Vector2 move_direction;
    [Signal]
    public delegate void AnimationChangedEventHandler(string newAnimation, bool direction);
    [Signal]
    public delegate void DamageTakenEventHandler();

    internal bool canTakeDamage = true;

    public override void _Ready()
    {
        player_animation = GetNode<AnimatedSprite2D>("%Animation_player");
        player_animation.Play("Idle");
        Setup_player();
    }


    public override void _PhysicsProcess(double delta)
    {
        velocity = Input.GetVector("Move_Left", "Move_Right", "Move_Up", "Move_Down");
        move_direction = (velocity * (float)player_stats["Speed"] * (float)delta);
        if (Input.IsActionPressed("Move"))
            MoveAndCollide(move_direction);
        if (Input.IsActionJustPressed("Attack"))
            Attack();
        if (velocity.X < -0.01)
            player_animation.FlipH = true;
        else if (velocity.X > 0.01)
            player_animation.FlipH = false;
        else
            player_animation.FlipH = player_animation.FlipH;
    }

    public override void _Input(InputEvent @event)
    {
        if (player_animation.Animation != "Walking" && player_animation.Animation != "Idle")
            return;
        else
        {
            if (Input.IsActionPressed("Move") && player_animation.Animation != "Walking")
                player_animation.Play("Walking");
            if (Input.IsActionJustReleased("Move") && player_animation.Animation == "Walking")
                player_animation.Play("Idle");
        }
    }


    public override void _Process(double delta)
    {
        if (player_animation.IsPlaying())
            return;
        else if (!player_animation.IsPlaying())
            player_animation.Play("Idle");
    }

    internal virtual async Task Iframes()
    {
        canTakeDamage = false;
        await ToSignal(GetTree().CreateTimer(1), SceneTreeTimer.SignalName.Timeout);
        canTakeDamage = true;
    }

    internal virtual void Attack(){ }

    internal virtual void die()
    {
        GD.Print("Im dead");
    }

    internal virtual void Setup_player()
    { 
        GetCharacterStats(Global.ChosenCharacterID);
    }

    internal virtual void GetCharacterStats(int characterID)
    { 
        string query = "SELECT a.* FROM Characters a WHERE a.ID = ?";
        player_stats = (Godot.Collections.Dictionary<String, Variant>)DataBase.query_with_bindings(query, [characterID])[0];
    }

    public virtual void Take_damage(float damage) { }

}
