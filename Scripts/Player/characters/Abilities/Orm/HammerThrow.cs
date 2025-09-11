using Godot;


public partial class HammerThrow : CharacterAbilityBase
{


    [Export]
    public PackedScene throwingHammer;

    public override void _Ready()
    {
        base._Ready();
        abilityTimer.Start(2);
    }

    internal override void useAbility()
    {
        for (int i = 0; i <= 5; i++)
        {
            AbilityHitBox hammer = (AbilityHitBox)throwingHammer.Instantiate();
            Vector2 dir = new Vector2(Mathf.Cos(0 + (45 * i)), Mathf.Sin(0 + (45 * i)));
            hammer.setUp(5000, abilityDamage, 300, dir);
            hammer.GlobalPosition = parent.Position;
            AddChild(hammer);
        }
    }

    public void OnAbilityTimerTimeOut()
    {
        useAbility();
    }
}
