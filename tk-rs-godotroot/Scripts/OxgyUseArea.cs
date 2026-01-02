using Godot;
using System;
using TkRsGodotroot.Scripts;

public partial class OxgyUseArea : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node body)
	{
		foreach (Node child in body.GetChildren())
		{
			if (child is IOxgyUsable oxygen)
			{
				oxygen.OxgyUse();
				break;
			}
		}
	}

	private void OnBodyExited(Node body)
	{
		foreach (Node child in body.GetChildren())
		{
			if (child is IOxgyUsable oxygen)
			{
				oxygen.OxgyStop();
				break;
			}
		}
	}
}
