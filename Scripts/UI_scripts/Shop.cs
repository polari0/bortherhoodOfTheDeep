using Godot;
using System;

public partial class Shop : Control
{

    private Player_controller _playerRef;

    public void OpenShop(Player_controller playerRef)
    {
        _playerRef = playerRef;
        SetAbilities(_playerRef.playerAbilities);
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
        HBoxContainer itemContainer = GetNode<HBoxContainer>("ShopItems");
        for (int i = 0; i < 4; i++)
        {
            ShopItem a = itemContainer.GetChild<ShopItem>(i);
            bool itemType = RollItemType();
            if (itemType == false)
            {
                a.SetUpShopItem();
            }
            else
                a.SetUpShopAbility();

            
        }
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
