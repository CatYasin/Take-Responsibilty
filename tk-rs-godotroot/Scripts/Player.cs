using Godot;


public partial class Player : CharacterBody2D
{
	[Export] public OxgygenSystem OxgygenSystem;
	public const float Speed = 500.0f;
	public const float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		
		 if(Input.IsActionJustPressed("Debug1"))
			OxgygenSystem.EmitSignal("OnStartTube", 2);
		 if(Input.IsActionJustPressed("Debug2"))
			 OxgygenSystem.EmitSignal("OnStopTube");

		

		
		Vector2 direction = Input.GetVector("Move_Left", "Move_Right", "Move_Up", "Move_Down");
		if (direction != Vector2.Zero)
		{
			velocity = direction * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
		
	}
}
