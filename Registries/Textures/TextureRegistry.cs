using System.Numerics;
using Allumeria.Rendering;
using StbImageSharp;

namespace ModAPI.Core.Registry.Textures;

public class ItemTextureRegistry
{
    private const int SlotSize = 16;
    private const int AtlasSize = 1024;
    private const int SlotsPerRow = AtlasSize / SlotSize;
    private const int MaxSlots = SlotsPerRow * SlotsPerRow;

    private int currentAtlas = 2;
    private int currentSlot = 0;

    private readonly Dictionary<int, Texture> atlases = new();
    
    public Vector3 Register(ImageResult texture)
    {
        
        // Make 'res/textures/items{currentAtlas}' if it doesn't exist. Make sure texture is 16x16.
        // Draw texture in next available X and Y(1 unit = 16 pixels)
        
        // Return Vec3(TextureX(in atlas), TextureY(in atlas), Atlas number)
        return new Vector3(0f, 0f, 0f);
    }
}