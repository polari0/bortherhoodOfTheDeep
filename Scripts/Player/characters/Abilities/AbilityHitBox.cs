using Godot;
using System;
using System.Timers;

public partial class AbilityHitBox : Node
{

    private System.Timers.Timer selfImolationTimer;
    private float _damage;

    /// <summary>
    /// sets up the ability hitbox
    /// </summary>
    /// <param name="lifeTime">Time before ability disapears from the screen in miliseconds</param>
    /// <param name="damage">Damage The abilit does</param>
    public void setUp(float lifeTime, float damage)
    {
        _damage = damage;
        destroyAbility(lifeTime);
    }

    private void destroyAbility(float lifeTime)
    {
        selfImolationTimer = new System.Timers.Timer(lifeTime * 1000);
        QueueFree();
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
