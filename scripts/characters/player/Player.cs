using System;
using Godot;

public partial class Player : Character 
{
    [Export]
    public float IndicatorDistance { set; get; } = 50;

    public Node2D Sword;
    public AnimationPlayer SwordAnimationPlayer;
    public Sprite2D AttackIndicator;

    public override void _Ready()
    {
        base._Ready();
        Sword = GetNode<Node2D>("Sword");
        SwordAnimationPlayer = Sword.GetNode<AnimationPlayer>("SwordAnimationPlayer");
        AttackIndicator = GetNode<Sprite2D>("AttackIndicator");

        Sword.Hide();
    }

    public override void _Process(double delta)
    {
        Vector2 mouseDirection = GetGlobalMousePosition() - GlobalPosition;

        // attempt to let the cursor move closer than the set radius
        if (Math.Abs(mouseDirection.Length()) > 1.0) {
            mouseDirection = mouseDirection.Normalized();
        }

        Sword.Rotation = mouseDirection.Angle();

        AttackIndicator.Position = mouseDirection * IndicatorDistance;

        // Handle attack animation
        if (Input.IsActionPressed("attack") && !SwordAnimationPlayer.IsPlaying()) {
            Sword.Show();
            SwordAnimationPlayer.Play("attack");
        }
    }

    public override void GetInput()
    {
        MoveDirection = Input.GetVector("move_left", "move_right", "move_up", "move_down");
    }

    public void OnAnimationFinished(string animationName) {
        if (animationName == "attack") {
            Sword.Hide();
            Sword.GetNode<Node2D>("Node").RotationDegrees = 0;
        }
    }
}