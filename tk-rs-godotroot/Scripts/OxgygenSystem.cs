using Godot;
using TkRsGodotroot.Scripts;


public partial class OxgygenSystem : Node, IOxgyUsable
{

	[Signal]
	public delegate void OnStartTubeEventHandler(float OxyMul = 1);
	
	[Signal]
	public delegate void OnStopTubeEventHandler();
	
	[Signal]
	public  delegate void OnReloadOxgyenEventHandler(float count,float OxyMul = 1);
	
	[Signal]
	public delegate void OnOxygenRunOutEventHandler();
	
	[Signal]
	public delegate void OnOxygenOverloadEventHandler();
	
	[ExportCategory("Configs")]
	[Export]
	private float MaxOxygen;
	
	[Export]
	private float MinOxygen = 0f;
	
	[Export]
	private float OxygenUsageRate;

	[Export]
	private float OxygenUsageMultipler = 1f;
	
	
	[Export]
	private float OxygenReloadRate;

	[Export]
	private float OxygenReloadMultipler = 1f;
	
	
	[Export]
	private float WaitPerSecond = 1f;
		

	private float Oxygen;
	
	private bool IsRunning;

	private bool IsReloading;
	


	public override void _Ready()
	{
		Oxygen = MaxOxygen;
		OnStartTube += Start;
		OnReloadOxgyen += ReloadOxgyen;
		OnStopTube += (() =>  {
			IsRunning = false;
			IsReloading = false;
		});
	}

	private bool IsLooping = false;
	
	protected async void Start(float oxyMul = 1)
	{
		if (IsLooping && IsReloading) return;
		
		if(oxyMul != 1)
			OxygenUsageMultipler = oxyMul;
		
		IsLooping = true;
		
		IsRunning = true;

		while (IsRunning)
		{
			await ToSignal(GetTree().CreateTimer(WaitPerSecond), "timeout");

			Oxygen -= OxygenUsageRate * OxygenUsageMultipler ;

			if (Oxygen <= MinOxygen)
			{
				Oxygen = MinOxygen;
				EmitSignal("OxygenRunOutEvent");
				IsRunning = false;
			}

			GD.Print(Oxygen);
		}

		IsLooping = false;
	}

	protected async void ReloadOxgyen(float OxyCount,float OxyMul = 1)
	{
		if (IsRunning && IsLooping) return;

		if (OxyMul != 1)
		{
			
			OxygenReloadMultipler = OxyMul;
		}

		IsLooping = true;
		
		IsReloading = true;

		while (IsReloading)
		{
			await ToSignal(GetTree().CreateTimer(WaitPerSecond), "timeout");
			
			Oxygen += OxygenReloadRate * OxygenReloadMultipler ;

			if (Oxygen >= MaxOxygen && Oxygen <= OxyCount)
			{
				Oxygen = MaxOxygen;
				EmitSignal(nameof(OnOxygenOverload));
				IsReloading = false;
			}
		}

		IsLooping = false;

	}
	
	
	//IOxgyUsable

	public void OxgyUse()
	{
		Start();
	}

	public void OxgyStop()
	{
		IsRunning = false;
	}

	public void OxgyReload(float count,float OxyMul = 1)
	{
		OxgyReload(count,OxyMul);
	}
}
