using Godot;
using System;
using System.Numerics;
using System.Runtime;

public partial class BaseEnemy : CharacterBody2D
{

    [Export]
    public SoftCollision softCollision;

    internal Player_controller player_node;
    internal float enemy_speed = 100;
    internal float enemy_health = 1000f;
    internal float enemy_damage = 5f;

    private Godot.Collections.Dictionary<String, Godot.Variant> enemyStats; 

    public override void _Ready()
    {
        //setUpEnemy();
    }


    public virtual void setUpEnemy(Godot.Collections.Dictionary<String, Godot.Variant> _enemyStats)
    {
        player_node = (Player_controller)GetTree().CurrentScene.GetNode<Node2D>("%PlayerSpawn").GetChild(0);
        enemy_speed = (float)_enemyStats["EnemySpeed"];
        enemy_health = (float)_enemyStats["EnemyHealth"];
        enemy_damage = (float)_enemyStats["EnemyDamage"];
    }
    public override void _Process(double delta)
    {
        // if (player_node == null)
        //     player_node = (Player_controller)GetNode<Node2D>("%PlayerSpawn").GetChild(0);
        //movement(delta);
    }
    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        movement(delta);
    }

    public void _on_body_entered(Node2D body)
    {
        if (body is Player_controller)
        {
            Player_controller target = (Player_controller)body;
            target.Take_damage(enemy_damage);
        }
    }

    internal virtual void movement(double delta)
    {
        Godot.Vector2 playerpos = player_node.GlobalPosition;
        Velocity = GlobalPosition.DirectionTo(playerpos) * enemy_speed;
    }

    internal virtual void Take_damage(float damage_taken)
    {
        enemy_health -= damage_taken;
        if (enemy_health < 0)
        {
            QueueFree();
        }
    }

    public void setPosition(Godot.Vector2 position) => Position = position;
}
