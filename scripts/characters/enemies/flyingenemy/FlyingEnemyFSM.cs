using Godot;
using System;

public partial class FlyingEnemyFSM : FSM
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		AddState("chase");
		SetState(States["chase"]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void StateLogic(double delta)
    {
        if (State == States["chase"])
		{
			Parent.GetInput();
			Parent.Move();
		}
    }

    public override int GetTransition()
    {
        return States["chase"];
    }

    public override void EnterState(int state)
    {
        base.EnterState(state);
    }
}
