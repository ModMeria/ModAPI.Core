using ModAPI.Abstractions.Items;
using ModAPI.Abstractions.Registries;
using Allumeria.Items;
using ModAPI.Abstractions.Builders;
using Allumeria;

namespace ModAPI.Core.Registries;

internal class ItemRegistry : IItemRegistry 
{
    private Dictionary<ItemConfig, ModItem> _modConfigItems = new Dictionary<ItemConfig, ModItem>();
    private Dictionary<string, ModItem> _items = new Dictionary<string, ModItem>();
    private Dictionary<Item, ModItem> _modItems = new Dictionary<Item, ModItem>();

    public static ItemRegistry Registry = new ItemRegistry();
    
    public IReadOnlyDictionary<ItemConfig, ModItem> GetAll()
    {
        return _modConfigItems;
    }
    
    public bool TryGet(ItemConfig id, out ModItem item)
    {
        return _modConfigItems.TryGetValue(id, out item);
    }

    public bool TryGet(string id, out ModItem item)
    {
        return _items.TryGetValue(id, out item);
    }

    internal bool TryGetModFromGame(Item item, out ModItem modItem)
    {
        return _modItems.TryGetValue(item, out modItem);
    }


   public ModItem Register(ItemConfig config)
    {
        ModItem item = new(config.Id, config);
        _modConfigItems[config] = item;
        _items[config.Id] = item;
        
        TextureRegistry.Registry.Register(config.TexturePath);
        
        var gameItem = new Item(config.TextureX, config.TextureY, config.Id); // Registers itself

        _modItems[gameItem] = item;

        foreach (var (locale, pair) in config.Translations)
        {
            TranslationRegistry.registry.Register(locale, $"item.{config.Id}", pair.Name);

            if (!string.IsNullOrWhiteSpace(pair.Description))
            {
                TranslationRegistry.registry.Register(locale, $"item.{config.Id}.desc", pair.Description);
            }
        }

        Logger.Info($"[ModMeria] Registered item '{config.Id}'");
        
        return item;
    }
}