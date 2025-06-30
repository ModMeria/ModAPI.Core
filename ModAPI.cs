using HarmonyLib;
using ModAPI.Abstractions;
using ModAPI.Abstractions.Config;
using ModAPI.Core.Config;
using PocketBlocks.Items;
using PocketBlocks.Items.Crafting;

namespace ModAPI.Core
{
    public class ModApi : IModApi
    {
        private static List<ICraftingRecipeConfig> _iCraftingRecipes = new List<ICraftingRecipeConfig>();
        public static Dictionary<string, Item> Items = new Dictionary<string, Item>();
        public static ModApi Api = new ModApi();
        
        public void RegisterItem(IItemConfig config)
        {
            if (config.Id != null)
            {
                var item = new Item(config.TextureX, config.TextureY, config.Id);
                Items[config.Id] = item;
            }
            else
            {
                Console.WriteLine("[ModMeria] WARNING: Tried to register an item with null ID.");
            }
        }

        public void RegisterCraftingRecipe(ICraftingRecipeConfig config)
        {
            if (config.Result != null)
            {
                _iCraftingRecipes.Add(config);
            }
        }
        
        private static string _translationPath = "res/translations/en_au.txt";

        public void AddTranslation(string id, string translatedName)
        {
            var lines = File.ReadAllLines(_translationPath);

            bool itemExists = lines.Any(line => line.StartsWith(id + " "));

            if (itemExists)
            {
                lines = lines.Select(line => line.StartsWith(id + " ") ? $"{id} {translatedName}" : line).ToArray();
            }
            else
            {
                lines.AddToArray($"{id} {translatedName}");
            }
            
            File.WriteAllLines(_translationPath, lines);
            
            Console.WriteLine($"[ModMeria] Added translation for {id}");
        }

        public void AddTranslation(string id, string translatedName, string description)
        {
            var lines = File.ReadAllLines(_translationPath);

            bool itemExists = lines.Any(line => line.StartsWith(id + " "));

            if (itemExists)
            {
                lines = lines.Select(line => line.StartsWith(id + " ") ? $"{id} {translatedName}" : line).ToArray();
            }
            else
            {
                lines = lines.AddToArray($"{id} {translatedName}");
            }

            bool descriptionExists = lines.Any(line => line.StartsWith(id + ".desc"));

            if (descriptionExists)
            {
                lines = lines.Select(line => line.StartsWith(id + ".desc") ? $"{id}.desc {description}" : line).ToArray();
            }
            else
            {
                lines = lines.AddToArray($"{id}.desc {description}");
            }

            File.WriteAllLines(_translationPath, lines);

            Console.WriteLine($"[ModMeria] Added translation for {id}");
        }
        
        [HarmonyPatch(typeof(CraftingRecipe))]
        [HarmonyPatch("InitCraftingRecipes")]
        public static class InitCraftingRecipesPatch
        {
            public static void Postfix()
            {
                foreach (var recipe in _iCraftingRecipes)
                {
                    if (recipe.Result != null)
                    {
                        var craftingRecipe = new CraftingRecipe(recipe.Result, recipe.CraftingStation);
                        foreach (var recipeEntry in recipe.Recipe)
                        {
                            craftingRecipe.AddReq(recipeEntry);
                        }
                    }
                }
            }
        }
    }
}
