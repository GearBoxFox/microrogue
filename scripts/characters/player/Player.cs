using System;
using Godot;

public partial class Player : Character 
{
    [Export]
    public float IndicatorDistance { set; get; } = 50;

    public Node2D Sword;
    public AnimationPlayer SwordAnimationPlayer;
    public Sprite2D AttackIndicator;

    private bool _useKeyboard = true;
    private Vector2 _prevAimPosition =Vector2.Left;

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
        Vector2 aimVector = Vector2.Zero;
        if (_useKeyboard)
        {
            aimVector = GetGlobalMousePosition() - GlobalPosition;
        } 
        else 
        {
            aimVector = Input.GetVector("aim_left", "aim_right", "aim_up", "aim_down");
        }

        if (aimVector == Vector2.Zero) 
        {
            // use last found aim angle
            aimVector = _prevAimPosition;
        }

        Vector2 indicatorVec = Vector2.FromAngle(aimVector.Angle());
        AttackIndicator.Position = indicatorVec * IndicatorDistance;

        if (!SwordAnimationPlayer.IsPlaying()) 
        {
            Sword.Rotation = aimVector.Angle();
        }

        _prevAimPosition = aimVector;

        // Handle attack animation
        if (Input.IsActionPressed("attack") && !SwordAnimationPlayer.IsPlaying()) {
            Sword.Show();
            SwordAnimationPlayer.Play("attack");
        }
    }

    public override void _Input(InputEvent @event)
    {
        // check if using mouse or controller
        if (@event is InputEventKey or InputEventMouse) 
        {
            AttackIndicator.Hide();
            _useKeyboard = true;
            Input.MouseMode = Input.MouseModeEnum.Visible;
        }
        else if (@event is InputEventJoypadButton or InputEventJoypadMotion) 
        {
            AttackIndicator.Show();
            _useKeyboard = false;
            Input.MouseMode = Input.MouseModeEnum.Hidden;
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