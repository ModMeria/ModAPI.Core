using ModAPI.Abstractions;
using ModAPI.Abstractions.Config;
using PocketBlocks.Rendering;
using StbImageSharp;

namespace ModAPI.Core.Config;

public class ItemConfig : IItemConfig
{
    private readonly IModApi _api;
    public string Id { get; private set; }
    public int TextureX { get; private set; }
    public int TextureY { get; private set; }
    public int TextureAtlas { get; private set; }

    public ItemConfig(IModApi api, string id)
    {
        _api = api;
        Id = id;
    }

    public IItemConfig SetId(string id)
    {
        this.Id = id;
        return this;
    }

    public IItemConfig SetTexture(int x, int y)
    {
        this.TextureX = x;
        this.TextureY = y;
        return this;
    }
    
    public IItemConfig SetTexture(string path)
    {
        using var stream = File.OpenRead(path);
        var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

        var pos = _api.RegisterTexture(image);
        return this;
    }

    public IItemConfig SetItemTranslation(string name)
    {
        _api.AddTranslation($"item.{this.Id}", name);
        return this;
    }

    public IItemConfig SetItemTranslation(string name, string description)
    {
        _api.AddTranslation($"item.{this.Id}", name, description);
        return this;
    }
}