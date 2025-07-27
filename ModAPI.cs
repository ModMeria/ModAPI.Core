using System.Numerics;
using HarmonyLib;
using ModAPI.Abstractions;
using ModAPI.Abstractions.Config;
using ModAPI.Abstractions.Items;
using ModAPI.Abstractions.Items.Crafting;
using ModAPI.Core.Registry.Items;
using ModAPI.Core.Registry.Textures;
using PocketBlocks.Items;
using PocketBlocks.Items.Crafting;
using StbImageSharp;
using ItemConfig = ModAPI.Core.Config.ItemConfig;

namespace ModAPI.Core
{
    public class ModApi : IModApi
    {
        private static List<CraftingRecipeConfig> _craftingRecipes = new List<CraftingRecipeConfig>();
        public static ItemRegistry ItemRegistry = ItemRegistry.Registry;
        public static ModApi Api = new ModApi();
        private static ItemTextureRegistry itemTextureRegistry = new ItemTextureRegistry();
        public void RegisterItem(ItemConfig config)
        {
            var item = new ModItem(1234, config); // ID is not real, that's in Register()
            ItemRegistry.Register(config.Id, item);
        }

        public Vector3 RegisterTexture(ImageResult imageResult)
        {
            itemTextureRegistry.Register(imageResult);
            return new Vector3(0f, 0f, 0f);
        }

        public void RegisterItem(IItemConfig config)
        {
            throw new NotImplementedException();
        }

        public void RegisterCraftingRecipe(CraftingRecipeConfig config)
        {
            if (config.Result != null)
            {
                _craftingRecipes.Add(config);
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

        public bool TryGetItem(string id, out ModItem item)
        {
            return ItemRegistry.TryGet(id, out item);
        }

        private static ItemStack MakeItemStackFromMod(ModItemStack stack)
        {
            return new ItemStack(Item.GetItemByID(stack.GetItem().id), stack.amount);
        }

        private static CraftingStation MakeCraftingStationFromMod(ModCraftingStation station)
        {
            switch (station.StrId)
            {
                case nameof(CraftingStation.inventory):
                    return CraftingStation.inventory;
                case nameof(CraftingStation.work_bench):
                    return CraftingStation.work_bench;
                case nameof(CraftingStation.furnace):
                    return CraftingStation.furnace;
                case nameof(CraftingStation.anvil):
                    return CraftingStation.anvil;
                case nameof(CraftingStation.decorations):
                    return CraftingStation.decorations;
                case nameof(CraftingStation.potions):
                    return CraftingStation.potions;
                default:
                    return new CraftingStation(station.StrId);
            }
        }

        private static Item MakeItemFromMod(ModItem item)
        {
            return Item.GetItemByID(item.id);
        }
        private static RecipeEntry MakeRecipeEntryFromMod(ModRecipeEntry entry)
        {
            return new RecipeEntry(MakeItemFromMod(entry.item), entry.amount);
        }
        
        [HarmonyPatch(typeof(CraftingRecipe))]
        [HarmonyPatch("InitCraftingRecipes")]
        public static class InitCraftingRecipesPatch
        {
            public static void Postfix()
            {
                foreach (var recipe in _craftingRecipes)
                {
                    if (recipe.Result != null)
                    {
                        ItemStack result = MakeItemStackFromMod(recipe.Result);
                        CraftingStation station = MakeCraftingStationFromMod(recipe.CraftingStation);
                        var craftingRecipe = new CraftingRecipe(result, station);
                        foreach (var recipeEntry in recipe.Recipe)
                        {
                            craftingRecipe.AddReq(MakeRecipeEntryFromMod(recipeEntry));
                        }
                    }
                }
            }
        }
    }
}
