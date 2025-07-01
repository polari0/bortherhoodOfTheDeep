using Godot;
using System;
using System.Runtime;

public partial class BaseEnemy : Area2D
{

    [Export]
    internal Player_controller player_node;
    double enemy_speed = 50;

    public override void _Ready()
    {
        player_node = (Player_controller)GetNode<Node2D>("%Player_spawn").GetChild(0);
    }

    public override void _Process(double delta)
    {
        Vector2 playerpos = player_node.GlobalPosition;
        GlobalPosition = GlobalPosition.MoveToward(playerpos, (float)(enemy_speed * delta));
    }

    public void _on_body_entered(Node2D body)
    {
        if (body is Player_controller)
        {
            Player_controller target = (Player_controller)body;
            target.Take_damage();
        }
    }
}
