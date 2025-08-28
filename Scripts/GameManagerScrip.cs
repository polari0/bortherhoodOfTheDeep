using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public partial class GameManagerScrip : Node
{
    [Export]
    public Node2D PlayerSpawn;
    [Export]
    public Area2D spawnArea;
    [Export]
    public Node WaveEnemies;
    [Export]
    public Shop shop;
    [Export]
    public Node2D mapNodes;
    [Export]
    public Timer waveTimer;

    [Export]
    public float waveTime = 10;

    //For now this should work but Maybe for mod support change to this fill from database
    //instead
    [Export]
    public Godot.Collections.Dictionary<String, PackedScene> enemiesDictionary;

    [Export]
    public Camera2D camera;

    //"res://Assets/Scenes/Characters/Player.tscn"
    Godot.Collections.Array waves;


    private int currentWave = 0;
    private bool waveActive;

    private Player_controller _Player;

    private Rect2 _area;
    private CollisionShape2D _spawnArea;

    public override void _Ready()
    {
        SpawnPlayer();
        CollisionShape2D _spawnArea = spawnArea.GetChild<CollisionShape2D>(0);
        Rect2 _area = _spawnArea.GetShape().GetRect();
        currentWave = 1;
        getImportantNodeReferences();
        getWavesData();
    }

    public override void _Process(double delta)
    {
        if (_Player != null)
        {
            camera.SetPosition(_Player.getPosition());
        }
    }

    private void SpawnPlayer()
    {
        string query = "SELECT a.Path FROM Characters a WHERE a.ID = ?";
        Godot.Collections.Dictionary a = (Godot.Collections.Dictionary)DataBase.query_with_bindings(query, [Global.ChosenCharacterID])[0];
        string characterPath = (String)a["Path"];
        PackedScene playerCharacter = GD.Load<PackedScene>(characterPath);
        var Player = playerCharacter.Instantiate();
        PlayerSpawn.AddChild(Player);
        _Player = (Player_controller)PlayerSpawn.GetChild(0);
    }

    private void getWavesData()
    {
        String query = "SELECT * FROM Waves";
        waves = DataBase.query(query);
        createSpawnQueue(currentWave);
    }

    private void createSpawnQueue(int currentWaveNumber)
    {
        GD.Print(currentWaveNumber);
        if (currentWaveNumber > waves.Count)
            currentWaveNumber = (int)GD.RandRange(10, 20);
        Godot.Collections.Dictionary currentWaveEnemies = (Godot.Collections.Dictionary)waves[currentWaveNumber - 1];
        spawnEnemies(currentWaveEnemies);
        waveActive = true;
        waveTimer.Start(waveTime);
    }

    private async void spawnEnemies(Godot.Collections.Dictionary currentWaveEnemies)
    {

        foreach (String key in currentWaveEnemies.Keys)
        {
            if (enemiesDictionary.ContainsKey(key))
            {
                Godot.Collections.Dictionary<String, Godot.Variant> _enemyStats = getEnemyStats(key);
                Vector2 spawnPos = pickSpawnLocation();
                for (int i = 0; i < (int)currentWaveEnemies[key]; i++)
                {
                    Vector2 spawnposRandomizer = spawnPosVariation(spawnPos);
                    BaseEnemy enemy = (BaseEnemy)enemiesDictionary[key].Instantiate();
                    WaveEnemies.AddChild(enemy);
                    enemy.setPosition(spawnposRandomizer);
                    enemy.setUpEnemy(_enemyStats);
                }
            }
            else
                GD.Print("Enemy not present in wave");
        }
        if (waveActive)
        {
            await ToSignal(GetTree().CreateTimer(2), SceneTreeTimer.SignalName.Timeout);
            spawnEnemies(currentWaveEnemies);
        }
    }


    private Godot.Collections.Dictionary<String, Godot.Variant> getEnemyStats(String enemyKey)
    {
        Godot.Collections.Dictionary<String, Godot.Variant> enemyStats;
        string query = "SELECT a.* FROM Enemies a WHERE a.EnemyName = ?";
        Godot.Collections.Dictionary<String, Godot.Variant> a = (Godot.Collections.Dictionary<String, Godot.Variant>)DataBase.query_with_bindings(query, [enemyKey])[0];
        enemyStats = a;
        return enemyStats;
    }

    private Vector2 pickSpawnLocation()
    {
        float x_coordinate = (float)GD.RandRange(0, 800);
        float y_coordinate = (float)GD.RandRange(0, 800);
        return new Vector2(x_coordinate, y_coordinate);
    }
    private Vector2 spawnPosVariation(Vector2 originalPos)
    {
        Vector2 newPos;
        newPos = originalPos + new Vector2(GD.RandRange(10, 40), GD.RandRange(0, 10));

        return newPos;
    }

    private void getImportantNodeReferences()
    {
        shop = GetNode<Shop>("%Shop");
        mapNodes = GetNode<Node2D>("%mapNodes");
    }

    private void openShop()
    {
        mapNodes.Visible = false;
        shop.Visible = true;
        shop.OpenShop(_Player);
    }

    private void closeShop()
    {
        mapNodes.Visible = true;
        shop.Visible = false;
    }

    public void onSpawnTimerTimeout()
    {
        waveActive = false;
        deleteChildren();
        currentWave++;
        openShop();
    }


    private void deleteChildren()
    {
        Godot.Collections.Array<Node> children = WaveEnemies.GetChildren();
        foreach (Node child in children)
        {
            child.QueueFree();
        }
    }

    public void NextWaveButtonPressed()
    {
        closeShop();
        createSpawnQueue(currentWave);
    }
}
