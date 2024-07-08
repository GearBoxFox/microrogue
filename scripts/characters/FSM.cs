using System;
using System.Collections.Generic;
using Godot;

public partial class FSM : Node
{
	
	public Dictionary<string, int> States { get; set; } = new Dictionary<string, int>();
	public int PreviousState = -1;
	public int State = -1;

	public Character Parent;
	public AnimatedSprite2D AnimationPlayer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Console.WriteLine("FSM Ready");
		Parent = GetParent<Player>();
		AnimationPlayer = Parent.GetNode<AnimatedSprite2D>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (State != -1) 
		{
			// process and update state
			StateLogic(delta);
			int transition = GetTransition();

			if (transition != -1) 
			{
				SetState(transition);
			}
		}
	}

	// process state
	public virtual void StateLogic(double delta) 
	{
	}

	// get next state
	public virtual int GetTransition()
	{
		return -1;
	}

	// add state to state dictionary
	public void AddState(string state) 
	{
		States.Add(state, States.Count);
	}

	// set the current state
	public void SetState(int newState) 
	{
		exitState(State);
		PreviousState = State;
		State = newState;
		EnterState(State);
	}

	// enter a new state
	public virtual void EnterState(int state)
	{
		return;
	}

	// exit an old state
	public virtual void exitState(int state) 
	{
		return;
	}
}
