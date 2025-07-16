using Godot;
using System;

public partial class SoftCollision : Area2D
{


    public bool isColliding()
    {
        Godot.Collections.Array<Area2D> areas = GetOverlappingAreas();
        return areas.Count > 0;
    }

    public Vector2 getPushVector()
    {
        Godot.Collections.Array<Area2D> areas = GetOverlappingAreas();
        Vector2 pushVector = Vector2.Zero;
        Area2D area = areas[0];
        float randomDirChange = GD.Randf() < 0.5f ? 1 : -1;
        pushVector = area.GlobalPosition.DirectionTo(GlobalPosition).Rotated(randomDirChange * Mathf.Pi/4);   
        return pushVector;
    }

}
