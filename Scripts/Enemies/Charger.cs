using Godot;
using Godot.Collections;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

public partial class Charger : BaseEnemy
{

    [Export]
    public Timer chargeTimer;

    private bool charge;
    private Godot.Vector2 playerpos;

    public override void setUpEnemy(Dictionary<string, Variant> _enemyStats)
    {
        base.setUpEnemy(_enemyStats);
        playerpos = player_node.GlobalPosition;
        charge = false;
        Velocity = randomDirection() * 200;
        chargeTimer.Start(4);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (charge)
        {
            LookAt(-playerpos);
            Velocity = GlobalPosition.DirectionTo(playerpos) * (float)enemy_speed;
            MoveAndSlide();
            if (GlobalPosition.DistanceTo(playerpos) <= 10)
            {
                charge = false;
                Velocity = randomDirection() * 200;
            }
        }
            else
            {
                MoveAndSlide();
            }
    }

    private Vector2 randomDirection()
    {
        float x = (float)generateRandomFloat(-1, 1);
        float y = (float)generateRandomFloat(-1, 1);
        return new Vector2(x, y);
    }

    private float generateRandomFloat(double min, double max)
    {
        Random rnd = new Random();
        float random = (float)(rnd.NextDouble() * (max - min) + min);
        if (random == 0)
            random += 0.1f;
        return random;
    }

    public void _on_charge_timer_timeout()
    {
        playerpos = player_node.GlobalPosition;
        charge = true;

    }

}
