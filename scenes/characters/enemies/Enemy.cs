using Godot;
using System;

public partial class Enemy : Character
{
	public CharacterBody2D Player;
	public NavigationAgent2D NavigationAgent;
	public Timer PathTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player = GetTree().CurrentScene.GetNode<CharacterBody2D>("Player");
		PathTimer = GetNode<Timer>("PathTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
