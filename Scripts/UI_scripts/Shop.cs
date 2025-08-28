using Godot;
using System;

public partial class Shop : Control
{

    private Player_controller _playerRef;

    public void OpenShop(Player_controller playerRef)
    {
        _playerRef = playerRef;
        setAbilities(_playerRef.playerAbilities);
    }

    private void setAbilities(Godot.Collections.Array<CharacterAbilityBase> abilities)
    {
        // foreach (CharacterAbilityBase ability in abilities)
        // {
        //     GetNode<HBoxContainer>("%Abilities").GetChild();
        // }
        HBoxContainer abilityContainer = GetNode<HBoxContainer>("%Abilities");
        abilities.Each((ability, n) =>
        {
            ShopAbilityIcon a = abilityContainer.GetChild<ShopAbilityIcon>(n);
            a.setupIcon(ability);

        });
    }


}
