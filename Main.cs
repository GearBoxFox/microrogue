using Godot;

public partial class Main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private int _score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		return;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Called when the player gets hit
	public void GameOver() 
	{
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<Timer>("MobTimer").Stop();

		GetNode<Hud>("HUD").ShowGameOver();
	}

	public void NewGame() {
		_score = 0;

		var hud = GetNode<Hud>("HUD");
		hud.UpdateScore(_score);
		hud.ShowMessage("Get Ready!");

		var player = GetNode<Player>("Player");
		var playerStart = GetNode<Marker2D>("StartPosition");
		player.Start(playerStart.Position);

		GetNode<Timer>("StartTimer").Start();
	}

	// Handle score increments
	public void OnScoreTimerTimeout() {
		_score++;
		GetNode<Hud>("HUD").UpdateScore(_score);
	}

	// Delay before spawning mobs
	public void OnStartTimerTimeout() {
		GetNode<Timer>("MobTimer").Start();
    	GetNode<Timer>("ScoreTimer").Start();
	}

	// Handle mob spawning
	private void OnMobTimerTimeout()
	{
		// Note: Normally it is best to use explicit types rather than the `var`
		// keyword. However, var is acceptable to use here because the types are
		// obviously Mob and PathFollow2D, since they appear later on the line.

		// Create a new instance of the Mob scene.
		Mob mob = MobScene.Instantiate<Mob>();

		// Choose a random location on Path2D.
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		// Set the mob's direction perpendicular to the path direction.
		float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

		// Set the mob's position to a random location.
		mob.Position = mobSpawnLocation.Position;

		// Add some randomness to the direction.
		direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mob.Rotation = direction;

		// Choose the velocity.
		var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		// Spawn the mob by adding it to the Main scene.
		AddChild(mob);
	}
}
