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
		Parent = GetParent<Character>();
		AnimationPlayer = Parent.GetNode<AnimatedSprite2D>("AnimationPlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (State != -1) 
		{
			// process and update state
			stateLogic(delta);
			int transition = getTransition();

			if (transition != -1) 
			{
				setState(transition);
			}
		}
	}

	// process state
	protected void stateLogic(double delta) 
	{
		return;
	}

	// get next state
	protected int getTransition()
	{
		return -1;
	}

	// add state to state dictionary
	protected void addState(string state) 
	{
		States.Add(state, States.Count);
	}

	// set the current state
	public void setState(int newState) 
	{
		exitState(State);
		PreviousState = State;
		State = newState;
		enterState(State);
	}

	// enter a new state
	protected void enterState(int state)
	{
		return;
	}

	// exit an old state
	protected void exitState(int state) 
	{
		return;
	}
}
