using Godot;

namespace TkRsGodotroot.Scripts;

public partial class OxgyenTube : Item
{

    [Export] protected float OxgyMul = 1F;
    
    public override void Use(Node2D actor)
    {
        GD.Print("Using OxgyenTube");
        
        foreach (Node child in actor.GetChildren())
        {
            if (child is IOxgyUsable oxygen)
            {
                oxygen.OxgyReload(count,OxgyMul);
            }
        }
    }
}