using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public partial class ShopItem : Control
{

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
                
            }
        }
    }

    public void SetUpShopItem()
    {
        
    }

    public void SetUpShopAbility()
    {
        
    }

    public void OnMouseEntered()
    {
        isHovering = true;
        GD.Print("Testing enter");
        SetProcess(true);
    }

    public void OnMouseExited()
    {
        isHovering = false;
        GD.Print("Testing exit");
        SetProcess(false);
    }

}
