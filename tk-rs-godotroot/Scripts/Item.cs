using Godot;

using TkRsGodotroot.Scripts;

public abstract partial class Item : Node, IInteractable,IItemUsable
{
	[ExportCategory("Item Config")]
	[Export] protected string name;
	[Export] protected string description;
	[Export] protected int count;
	[Export] protected UsageItem UsageItem;
	[Export] protected Texture2D Texture;
	[Export] protected Texture2D Image;

	protected ItemStruct ItemStruct;
	
	public override void _Ready()
	{
		ItemStruct = new ItemStruct(name, description, count, Texture, Image, UsageItem);
	}
	
	//Interactable

	public void Interact(Node2D actor)
	{
		GD.Print(actor);
	}
	
	//ItemUsable

	public virtual void Use(Node2D actor)
	{
		GD.Print("Using " + name);
	}

}
