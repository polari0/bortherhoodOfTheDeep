using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using System;
using System.ComponentModel;
using System.Net;
using System.Numerics;

public partial class Player_controller : CharacterBody2D
{

    [Export]
    public float player_speed = 200;

    [Export]
    public PackedScene little_helper_scene = GD.Load<PackedScene>("res://Assets/Scenes/little_helper.tscn");

    [Export]
    public Godot.Collections.Dictionary<String, PackedScene> weapon_types;
    private Godot.Collections.Dictionary<String, Variant> player_stats;

    public Godot.Vector2 velocity;

    public Array<LittleHelper> active_helpers = [];

    private int little_helper_count = 0;
    internal AnimatedSprite2D player_animation;

    private Array<Node> helper_positions;

    public Godot.Vector2 move_direction;

    [Signal]
    public delegate void AnimationChangedEventHandler(string newAnimation, bool direction);

    public override void _Ready()
    {
        player_animation = GetNode<AnimatedSprite2D>("%Animation_player");
        player_animation.Play("Idle");
        //test_lilhepler_buying();
        //player_stats = (Godot.Collections.Dictionary<String, Variant>)DataBase.query_with_bindings(query, [1])[0];
        //base._Ready();
    }


    public override void _PhysicsProcess(double delta)
    {
        velocity = Input.GetVector("Move_Left", "Move_Right", "Move_Up", "Move_Down");
        move_direction = (velocity * player_speed * (float)delta);
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

    internal virtual void Attack()
    {
        
    }

    // public void buy_little_helper(Godot.Collections.Dictionary<String, Variant> helper_data)
    // {
    //     if (little_helper_count == 8)
    //     {
    //         foreach (LittleHelper helper in active_helpers)
    //         {
    //             if (helper.helper_data["ID"].Equals(helper_data["ID"]) && helper.helper_data["Weapon_Tier"].Equals(helper_data["Weapon_Tier"]))
    //             {
    //                 helper.upgrade((int)(helper_data["Weapon_Tier"]), (float)(player_stats["Masterwork_level"]));
    //             }
    //         }
    //     }
    //     else
    //         place_little_helper(helper_data);
    // }


    // private void place_little_helper(Godot.Collections.Dictionary<String, Variant> helper_data)
    // {
    //     helper_positions = little_helper_positions.GetChildren();
    //     PackedScene helper_type;
    //     helper_type = weapon_types[(string)helper_data["Weapon_type"]];
    //     if (little_helper_count < helper_positions.Count)
    //     {
    //         LittleHelper lilhelper = helper_type.Instantiate<LittleHelper>();
    //         Node2D next_pos;
    //         next_pos = (Node2D)helper_positions[little_helper_count];
    //         lilhelper.setup(helper_data);
    //         next_pos.AddChild(lilhelper);
    //         active_helpers.Add(lilhelper);
    //         little_helper_count += 1;
    //     }
    // }

    // private void test_lilhepler_buying()
    // {
    //     Godot.Collections.Array weapons = DataBase.query("SELECT a.* FROM Weapons a ");

    //     for (int i = 0; i < 8; i++)
    //     {
    //         int random = GD.RandRange(0, weapons.Count - 1);
    //         buy_little_helper((Godot.Collections.Dictionary<String, Variant>)weapons[random]);
    //     }
    // }

}
