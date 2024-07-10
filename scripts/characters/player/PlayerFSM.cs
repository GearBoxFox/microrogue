using Godot;

public partial class PlayerFSM : FSM
{
    public override void _Ready()
    {
        base._Ready();

        AddState("idle");
        AddState("move");
        AddState("hurt");
        AddState("dead");

        SetState(States["idle"]);
    }

    public override void StateLogic(double delta)
    {
        if (State == States["idle"] || State == States["move"]) {
            Parent.GetInput();
            Parent.Move();
        }
    }

    public override int GetTransition()
    {
        if (State == States["idle"])
        {
            if (Parent.Velocity.Length() > 10.0) 
            {
                return States["move"];
            }
        } 
        else if (State == States["move"])
        {
            if (Parent.Velocity.Length() < 10.0) 
            {
                return States["idle"];
            }
        }
        else if (State == States["hurt"])
        {
            if (!AnimationPlayer.IsPlaying())
            {
                return States["idle"];
            }
        }
        
        return -1;
    }

    public override void EnterState(int state)
    {
        if (State == States["idle"])
        {
            AnimationPlayer.Play("idle");
        } 
        else if (State == States["move"])
        {
            AnimationPlayer.Play("idle");
        }
        else if (State == States["hurt"])
        {
            AnimationPlayer.Play("hurt");
        }
    }
}