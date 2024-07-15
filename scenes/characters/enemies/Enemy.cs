using System;
using Godot;

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
		NavigationAgent = GetNode<NavigationAgent2D>("NavigationAgent");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}	

	public override void GetInput()
	{
		if (!NavigationAgent.IsTargetReached() && NavigationAgent != null)
		{
			var VecToNextPoint = NavigationAgent.GetNextPathPosition() - GlobalPosition;
			MoveDirection = VecToNextPoint;

			// if (VecToNextPoint.X > 0.0 && AnimatedSprite.FlipH)
			// {
			// 	AnimatedSprite.FlipH = false;
			// } else if (VecToNextPoint.X < 0.0 && !AnimatedSprite.FlipH) {
			// 	AnimatedSprite.FlipH = true;
			// }
		}
	}

	public void OnPathTimerTimeout()
	{
		if (Player != null)
		{
				GetPathToPlayer();
		} else {
			PathTimer.Stop();
			MoveDirection = Vector2.Zero;
		}
	}

	public void GetPathToPlayer() 
	{
		NavigationAgent.TargetPosition = Player.Position;
	}
}
