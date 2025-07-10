using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public partial class GameManagerScrip : Node
{
    [Export]
    public Node2D PlayerSpawn;
    [Export]
    public Area2D spawnArea;

    //For now this should work but Maybe for mod support change to this fill from database
    //instead
    [Export]
    public Godot.Collections.Dictionary<String, PackedScene> enemiesDictionary;

    //"res://Assets/Scenes/Characters/Player.tscn"
    Godot.Collections.Array waves;


    private Rect2 _area;
    private CollisionShape2D _spawnArea;

    public override void _Ready()
    {
        SpawnPlayer();
        CollisionShape2D _spawnArea = spawnArea.GetChild<CollisionShape2D>(0);
        Rect2 _area = _spawnArea.GetShape().GetRect();
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

    private void getWavesData()
    {
        String query = "";
        waves = DataBase.query(query);

    }

    private void createSpawnQueue(int currentWaveNumber)
    {
        Godot.Collections.Dictionary currentWaveEnemies = (Godot.Collections.Dictionary)waves[currentWaveNumber];
    }

    private async void spawnEnemies(Godot.Collections.Dictionary currentWaveEnemies)
    {
        foreach (String key in currentWaveEnemies.Keys)
        {
            Vector2 spawnPos = pickSpawnLocation();
            for (int i = 0; i < (int)currentWaveEnemies[key]; i++)
            {
                Vector2 spawnposRandomizer = spawnPosVariation(spawnPos);
                BaseEnemy enemy = (BaseEnemy)enemiesDictionary[key].Instantiate();
                spawnArea.AddChild(enemy);
                enemy.setPosition(spawnposRandomizer);
            }
        }
        await ToSignal(GetTree().CreateTimer(10), SceneTreeTimer.SignalName.Timeout);
        spawnEnemies(currentWaveEnemies);
    }


    private Vector2 pickSpawnLocation()
    {
        float x_coordinate = (float)GD.RandRange(_area.Position.X, _area.Size.X);
        float y_coordinate = (float)GD.RandRange(_area.Position.Y, _area.Size.Y);
        return new Vector2(x_coordinate, y_coordinate);
    }
    private Vector2 spawnPosVariation(Vector2 originalPos)
    {
        Vector2 newPos;
        newPos = originalPos + new Vector2(GD.RandRange(0 , 10), GD.RandRange(0 , 10));

        return newPos;
    }

    public void onSpawnTimerTimeout()
    {

    }

}
