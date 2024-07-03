using Godot;
using System;

public partial class main : Node
{
	[Export]
	public PackedScene MobScene { get; set; }

	private int _score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
	}

	public void NewGame() {
		_score = 0;

		var player = GetNode<player>("Player");
		var playerStart = GetNode<Marker2D>("StartPosition");
		player.Start(playerStart.Position);

		GetNode<Timer>("StartTimer").Start();
	}

	// Handle score increments
	public void OnScoreTimerTimeout() {
		_score++;
	}

	// Delay before spawning mobs
	public void OnStartTimerTimeout() {
		GetNode<Timer>("MobTimer").Start();
    	GetNode<Timer>("ScoreTimer").Start();
	}
}
