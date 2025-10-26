using Allumeria;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ModAPI.Core.Helpers;

internal static class AtlasHelper
{
    private static readonly string TexturePath = Path.Combine("res", "textures");
    private static readonly int SlotSize = 16;
    private static Queue<AtlasSlot> _freeSlots = new Queue<AtlasSlot>();

    public class AtlasSlot
    {
        public int X { get; set; } = 64;
        public int Y { get; set; } = 0;
    }
    
    public static void InitItemTextures()
    {
        string itemAtlasPath = Path.Combine(TexturePath, "items.png");
        
        using var atlas = Image.Load<Rgba32>(itemAtlasPath);
        
        int tilesX = atlas.Width / SlotSize;
        int tilesY = atlas.Height / SlotSize;
        
        var result = new Queue<AtlasSlot>();

        for (int ty = 0; ty < tilesY; ty++)
        {
            for (int tx = 0; tx < tilesX; tx++)
            {
                int startX = tx * SlotSize;
                int startY = ty * SlotSize;
                
                bool isEmpty = true;

                for (int y = 0; y < SlotSize && isEmpty; y++)
                {
                    for (int x = 0; x < SlotSize && isEmpty; x++)
                    {
                        var pixel = atlas[startX + x, startY + y];
                        
                        if (pixel.A != 0)
                            isEmpty = false;
                    }
                }
                if (isEmpty) result.Enqueue(new AtlasSlot
                {
                    X = tx,
                    Y = ty,
                });
            }
        }
        
        _freeSlots = result;
    } 
    
    public static AtlasSlot RegisterItemTexture(Image image)
    {
        string itemAtlasPath = Path.Combine(TexturePath, "items.png");
        
        using var atlas = Image.Load<Rgba32>(itemAtlasPath);

        if (_freeSlots.Count == 0)
        {
            Logger.Warn("No free slots found");
            return new AtlasSlot();
        }

        var slot = _freeSlots.Dequeue();
        
        int startX = slot.X;
        int startY = slot.Y;
        
        if (image.Width != 16 || image.Height != 16)
            return new AtlasSlot();

        atlas.Mutate(ctx => ctx.DrawImage(image, new Point(startX, startY), 1f));
        
        atlas.Save(itemAtlasPath);

        return slot;
    }    
}