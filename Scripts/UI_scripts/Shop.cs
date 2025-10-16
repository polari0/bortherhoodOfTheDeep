using Godot;
using System;
using System.Collections.Generic;

public partial class Shop : Control
{

    private Player_controller _playerRef;

    public void OpenShop(Player_controller playerRef)
    {
        _playerRef = playerRef;
        SetAbilities(_playerRef.playerAbilities);
        SetShopItems();
    }

    private void SetAbilities(Godot.Collections.Array<CharacterAbilityBase> abilities)
    {
        // foreach (CharacterAbilityBase ability in abilities)
        // {
        //     GetNode<HBoxContainer>("%Abilities").GetChild();
        // }
        HBoxContainer abilityContainer = GetNode<HBoxContainer>("%Abilities");
        abilities.Each((ability, n) =>
        {
            GD.Print(n);
            ShopAbilityIcon a = abilityContainer.GetChild<ShopAbilityIcon>(n);
            a.setupIcon(ability);

        });
    }

    private void SetShopItems()
    {
        VBoxContainer itemContainer = GetNode<VBoxContainer>("%ShopItems");
        for (int i = 0; i < 4; i++)
        {
            GD.Print("Setting shop item");
            ShopItem a = itemContainer.GetChild<ShopItem>(i);
            bool itemType = RollItemType();
            if (itemType == false)
            {
                Dictionary<String, int> statUpgrade = new Dictionary<String, int>
                { { GetRandomStat(), GD.RandRange(0, 10) } }; 
                a.SetUpShopItem(statUpgrade);
            }
            else
                a.SetUpShopAbility();

            
        }
    }


    private string GetRandomStat()
    {
        int random = GD.RandRange(0, 5);
        string statName = Global.statNames[random];
        return statName;
    }

    private bool RollItemType()
    {
        int random = GD.RandRange(0, 4);
        if (random == 0)
        {
            return true;
        }
        else
            return false;

    }
}
