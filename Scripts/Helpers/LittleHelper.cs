using Godot;
using System;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

public partial class LittleHelper : Node2D
{
    Variant database_script = GD.Load<GDScript>("res://Assets/DataBase/Data_Base.gd");

    [Export]
    public Godot.Collections.Dictionary<String, Godot.Variant> helper_data;
    [Export]
    public int ID;
    private AnimatedSprite2D helper_animation;

    private Player_controller player_controller;

    private Node2D weapon_pivot;
    private Node2D helper_pos;

    public override void _Ready()
    {
        helper_animation = GetNode<AnimatedSprite2D>("%helper_animation");
        player_controller = (Player_controller)GetParent().GetParent().GetParent();
        player_controller.AnimationChanged += _On_parent_animation_changed;
        
        //Testing
        string query = "SELECT a.* FROM Weapons a  WHERE a.ID = 1";
        Godot.Collections.Dictionary<String, Variant> weapon_data =
        (Godot.Collections.Dictionary<String, Variant>)DataBase.query(query)[0];
        //setup(weapon_data);
    }

    /// <summary>
    /// Setup function for little helper 
    /// weapon, type and everything else of that sort
    /// </summary>
    public virtual void setup(Godot.Collections.Dictionary<String, Variant> data){
        helper_data = data;
        ID = (int)data["ID"];
    }

    public void upgrade(int tier, float masterwork_level)
    {
        string query = @"
        SELECT 
            a.Damage_scale AS Weapon_Damage, 
            a.AttackSpeed_scale AS Weapon_AttackSpeed, 
            a.Range_scale AS Weapon_range
        FROM Weapon_upgrade a  
        WHERE a.WeaponID = ?";

        Godot.Collections.Dictionary<String, Variant> upgradeData =
        (Godot.Collections.Dictionary<String, Variant>)DataBase.query_with_bindings(query, [ID])[0];
        foreach (string key in helper_data.Keys)
        {
            if (CanConvert<float>(helper_data[key]))
            {
                if (key == "Weapon_Tier") helper_data[key] = (int)(helper_data[key]) + 1;
                else if (upgradeData.ContainsKey(key))
                {
                    helper_data[key] = (float)helper_data[key] * (float)upgradeData[key] * (1 + (masterwork_level * 0.01));
                }
                else
                {
                    continue;
                }
            }
        }
    }

    private void _On_parent_animation_changed(String newAnimation, bool direction)
    {
        helper_animation.Play(newAnimation);
        helper_animation.FlipH = direction;
    }

    internal async void Wait(double wait_time)
    {
        await ToSignal(GetTree().CreateTimer(wait_time), "timeout");
    }

    /// <summary>
    /// Checks wether a given value is valid as given type
    /// </summary>
    /// <param name="value">Input value as a string</param>
    /// <param name="type">Type you want to check for</param>
    /// <returns></returns>
    private bool CanConvert<[MustBeVariant] T>(Variant variant)
    {
        try
        {
            T value = variant.As<T>();
            return true;
        }
        catch
        {
            return false;
        }
    }

}
