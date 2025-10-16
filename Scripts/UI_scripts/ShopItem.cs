using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class ShopItem : Control
{


    [Export]
    public Label priceLable, NameLable;
    [Export]
    public TextureRect ItemTexture;


    private bool isHovering = false;

    public override void _Ready()
    {
        SetProcess(false);
    }
    
    public override void _Process(double delta)
    {
        if (isHovering)
        {
            if (Input.IsActionJustPressed("LeftMouse"))
            {
                GD.Print("Test");
            }
        }
    }

    public void SetUpShopItem(Dictionary<string, int> statUpgrade)
    {
        string statName = statUpgrade.First().Key;
        priceLable.Text = statUpgrade[statName].ToString();
        NameLable.Text = statName;
        
    }

    public void SetUpShopAbility()
    {
        
    }

    public void OnMouseEntered()
    {
        isHovering = true;
        SetProcess(true);
    }

    public void OnMouseExited()
    {
        isHovering = false;
        SetProcess(false);
    }

}
