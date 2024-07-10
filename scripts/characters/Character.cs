using Godot;

public partial class Character : CharacterBody2D
{
	private const float FRICTION = 0.15f;

	[Export]
	public int MaxHp {get; set;} = 2;
	[Export]
	public int Hp {get; set;} = 2;

	[Signal]
	public delegate void hpChangedEventHandler(int newHp);

	[Export]
	public int MaxSpeed { get; set; } = 30;
	[Export]
	public int Acceleration { get; set; } = 100;

	public Vector2 MoveDirection = Vector2.Zero;

	public Node StateMachine;
	public AnimatedSprite2D AnimatedSprite;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        StateMachine = GetNode<Node>("FiniteStateMachine");
		AnimatedSprite = GetNode<AnimatedSprite2D>("AnimationPlayer");
    }

    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
	{
		MoveAndSlide();
		Velocity = Velocity.Normalized() * Mathf.Lerp(Velocity.Length(), Vector2.Zero.Length(), FRICTION);    	
	}

	public virtual void GetInput()
	{

	}

	public void Move() {
		MoveDirection = MoveDirection.Normalized();
		Velocity += MoveDirection * Acceleration;
		Velocity = Velocity.LimitLength(MaxSpeed);
	}
}
