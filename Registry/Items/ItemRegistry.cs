using ModAPI.Abstractions.Items;
using ModAPI.Abstractions.Registry.Items;
using Allumeria.Items;

namespace ModAPI.Core.Registry.Items;

public class ItemRegistry :IItemRegistry 
{
    private Dictionary<string, ModItem> _items = new Dictionary<string, ModItem>();
    private Dictionary<Item, ModItem> _modItems = new Dictionary<Item, ModItem>();
    private Dictionary<int, ModItem> _itemIDs = new Dictionary<int, ModItem>();

    public static ItemRegistry Registry = new ItemRegistry();
    
    public IReadOnlyDictionary<string, ModItem> GetAll()
    {
        return _items;
    }

    public void Register(string id, ModItem item)
    {
        _items[id] = item;
        
        var config = item.config;
        
        var gameItem = new Item(config.TextureX, config.TextureY, config.Id);

        item.id = gameItem.itemID;
        
        _modItems[gameItem] = item;
    }

    public bool TryGet(string id, out ModItem item)
    {
        return _items.TryGetValue(id, out item);
    }

    internal bool TryGetModFromGame(Item item, out ModItem modItem)
    {
        return _modItems.TryGetValue(item, out modItem);
    }
    
}