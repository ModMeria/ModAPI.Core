using ModAPI.Abstractions.Config;
using PocketBlocks.Items;
using PocketBlocks.Items.Crafting;

namespace ModAPI.Config
{
    public class CraftingRecipeConfig : ICraftingRecipeConfig
    {
        public ItemStack? Result { get; private set; }
        public CraftingStation CraftingStation { get; private set; } = CraftingStation.work_bench;
        public List<RecipeEntry> Recipe { get; set; } = new List<RecipeEntry>();

        public ICraftingRecipeConfig SetResult(Item item, int amount)
        {
            this.Result = new ItemStack(item, amount);
            return this;
        }

        public ICraftingRecipeConfig SetResult(ItemStack item)
        {
            this.Result = item;
            return this;
        }

        public ICraftingRecipeConfig SetResult(string itemId, int amount)
        {
            if (ModApi.Items.ContainsKey(itemId))
            {
                Item item = ModApi.Items[itemId];
                SetResult(item, amount);
            }
            else
            {
                Console.WriteLine("[ModMeria] Tried to add a result for an item with nonexistent id. HINT: If you're trying to add a Result for a built-in Item don't use string IDs.");
            }
            return this;
        }

        public ICraftingRecipeConfig SetCraftingStation(CraftingStation craftingStation)
        {
            this.CraftingStation = craftingStation;
            return this;
        }

        public ICraftingRecipeConfig AddRecipeEntry(RecipeEntry recipe)
        {
            this.Recipe.Add(recipe);
            return this;
        }

        public ICraftingRecipeConfig AddRecipeEntry(Item item, int amount)
        {
            this.Recipe.Add(new RecipeEntry(item, amount));
            return this;
        }

        public ICraftingRecipeConfig AddRecipeEntry(string itemId, int amount)
        {
            if (ModApi.Items.ContainsKey(itemId))
            {
                Item item = ModApi.Items[itemId];
                this.AddRecipeEntry(item, amount);
            }
            else
            {
                Console.WriteLine("[ModMeria] Tried to add a crafting recipe for an item with nonexistent id. HINT: If you're trying to add a RecipeEntry for a built-in Item don't use string IDs.");
            }
            return this;
        }
    }
}
