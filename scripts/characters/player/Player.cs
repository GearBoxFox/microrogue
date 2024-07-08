using Godot;

public partial class Player : Character 
{
    public override void _Process(double delta)
    {

    }

    public override void GetInput()
    {
        MoveDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    }
}