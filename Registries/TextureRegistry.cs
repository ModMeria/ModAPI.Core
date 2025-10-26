using Allumeria;
using ModAPI.Abstractions.Registries;
using ModAPI.Abstractions.Textures;
using ModAPI.Core.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ModAPI.Core.Registries;

public class TextureRegistry : ITextureRegistry
{
    public static TextureRegistry Registry = new TextureRegistry();
    
    public ModTexture Register(string? item)
    {
        if (string.IsNullOrWhiteSpace(item))
        {  
            Logger.Error("Texture path is null or empty");
            return new ModTexture();
        }

        if (item.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
        {
            Logger.Error($"Path '{item}' contains invalid characters.");
            return new ModTexture();  
        }

        var fullPath = Path.GetFullPath(item);
        if (!File.Exists(fullPath))
        {
            Logger.Error($"Texture file not found: {fullPath}");
            return new ModTexture();   
        }
        
        using var img = Image.Load<Rgba32>(fullPath);

        if (img.Width != 16 || img.Height != 16)
        {
            Logger.Error($"Texture at '{fullPath}' must be 16x16 but is  {img.Width}x{img.Height}.");
            return new ModTexture();  
        }
        
        var slot = AtlasHelper.RegisterItemTexture(img);
         
        return new ModTexture(slot.X, slot.Y);
    }

    public bool TryGet(string key, out ModTexture item)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyDictionary<string, ModTexture> GetAll()
    {
        throw new NotImplementedException();
    }
}