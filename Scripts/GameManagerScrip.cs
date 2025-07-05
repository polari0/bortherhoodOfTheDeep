using Godot;
using System;

public partial class GameManagerScrip : Node
{
    [Export]
    public Node2D PlayerSpawn;
    [Export]
    public Area2D spawnArea;

    //"res://Assets/Scenes/Characters/Player.tscn"

    public override void _Ready()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        string query = "SELECT a.Path FROM Characters a WHERE a.ID = ?";
        Godot.Collections.Dictionary a = (Godot.Collections.Dictionary)DataBase.query_with_bindings(query, [Global.ChosenCharacterID])[0];
        string characterPath = (String)a["Path"];
        PackedScene playerCharacter = GD.Load<PackedScene>(characterPath);
        var Player = playerCharacter.Instantiate();
        PlayerSpawn.AddChild(Player);
    }
}
