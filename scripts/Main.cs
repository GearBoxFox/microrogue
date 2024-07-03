using Godot;

public partial class Main : Node
{
	private int _score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var player = GetNode<Player>("Player");
		var spawn = GetNode<Marker2D>("PlayerSpawn");

		player.Start(spawn.Position);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
