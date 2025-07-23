using Godot;
using Godot.Collections;
using System;
using System.Threading.Tasks;

public partial class Charger : BaseEnemy
{

    [Export]
    public Timer chargeTimer;

    private bool Charge;
    private Godot.Vector2 playerpos;

    public override void setUpEnemy(Dictionary<string, Variant> _enemyStats)
    {
        base.setUpEnemy(_enemyStats);
        playerpos = player_node.GlobalPosition;
        chargeTimer.Start(1);
        GD.Print("spawened snapper");
    }

    public override void _PhysicsProcess(double delta)
    {
        LookAt(-playerpos);
        Velocity = GlobalPosition.DirectionTo(playerpos) * (float)enemy_speed;
        MoveAndSlide();
    }


    public void _on_charge_timer_timeout()
    {
        playerpos = player_node.GlobalPosition;
        
    }

}
