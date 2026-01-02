using Godot;
using TkRsGodotroot.Scripts;


public partial class Player : CharacterBody2D
{
	
	[Export] private RayCast2D InteractRay;
	
	public const float Speed = 500.0f;
	public const float JumpVelocity = -400.0f;

	private static Vector2 velocity;
	
	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;
		
		HandleMovement();
		
		HandleInput();
		
		Velocity = velocity;
		MoveAndSlide();
		
	}

	private void HandleInput()
	{
		if (Input.IsActionJustPressed("Interact"))
		{
			Interact();
		}
	}
	
	
	
	private void HandleMovement()
	{
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
	}

	private void Interact()
	{
		
		if (InteractRay.IsColliding())
		{
			
			var col = InteractRay.GetCollider();
			GD.Print(col.GetType());
			if (col is Node node)
			{
				if (node.GetParent() is IInteractable target)
				{
					target.Interact((this));
				}
			}
		}
	}
	
}
