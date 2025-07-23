using Godot;
using System;

public partial class Spider : BaseEnemy
{


    internal override void movement(double delta)
    {
        base.movement(delta);
        if (softCollision.isColliding())
        {
            Velocity += softCollision.getPushVector() * (float)(delta * 5000);
        }
        MoveAndSlide();
    }
}
