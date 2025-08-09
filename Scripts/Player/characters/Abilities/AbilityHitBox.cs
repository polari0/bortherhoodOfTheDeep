using Godot;
using System;
using System.Timers;

public partial class AbilityHitBox : Area2D
{

    [Export]
    public float attackSpeed;

    private Vector2 _direction;

    private System.Timers.Timer selfImolationTimer;
    private float _damage;

    /// <summary>
    /// sets up the ability hitbox
    /// </summary>
    /// <param name="lifeTime">Time before ability disapears from the screen in miliseconds</param>
    /// <param name="damage">Damage The abilit does</param>
    /// <param name="speed">Ability Speed</param>
    /// <param name="direction">Ability Direction</param>
    public void setUp(float lifeTime, float damage, float speed, Vector2 direction)
    {
        _damage = damage;
        _direction = direction;
        attackSpeed = speed;
        destroyAbility(lifeTime);
        GD.Print("hammer Time");
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * attackSpeed * (float)delta;
    }

    private void destroyAbility(float lifeTime)
    {
        selfImolationTimer = new System.Timers.Timer(lifeTime);
        selfImolationTimer.Start();
        selfImolationTimer.Elapsed += OnSelfImolationTimerEndedEvent;
    }

    private void OnSelfImolationTimerEndedEvent(Object source, ElapsedEventArgs e)
    {
        QueueFree();
        GD.Print("hammer gone");
    }

    public void OnBodyEntered(PhysicsBody2D body)
    {
        if (body is BaseEnemy)
        {
            BaseEnemy target = (BaseEnemy)body;
            target.Take_damage(_damage);
            QueueFree();
        }
    }

}
