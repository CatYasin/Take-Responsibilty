using Godot;

namespace TkRsGodotroot.Scripts;

public struct ItemStruct
{
    public string Name;
    public string Description;
    public int Count;
    public Texture2D Texture;
    public Texture2D Image;
    public UsageItem UsageItem;

    public ItemStruct(string name, string description, int count, Texture2D texture, Texture2D image, UsageItem usageItem)
    {
        Name = name;
        Description = description;
        Count = count;
        Texture = texture;
        Image = image;
        UsageItem = usageItem;
        
    }
}