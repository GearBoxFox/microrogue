using Godot;

public partial class Main : Node
{
	private int _score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var player = GetNode<Character>("Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
