using System;
using Godot;

public partial class Player : Character 
{
    [Export]
    public float IndicatorDistance { set; get; } = 50;

    public Node2D Sword;
    public Sprite2D AttackIndicator;

    public override void _Ready()
    {
        base._Ready();
        Sword = GetNode<Node2D>("Sword");
        AttackIndicator = GetNode<Sprite2D>("AttackIndicator");
    }

    public override void _Process(double delta)
    {
        Vector2 mouseDirection = GetGlobalMousePosition() - GlobalPosition;

        // attempt to let the cursor move closer than the set radius
        if (Math.Abs(mouseDirection.Length()) > 1.0) {
            mouseDirection = mouseDirection.Normalized();
        }

        AttackIndicator.Position = mouseDirection * IndicatorDistance;
    }

    public override void GetInput()
    {
        MoveDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    }
}