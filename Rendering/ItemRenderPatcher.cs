using HarmonyLib;
using ModAPI.Abstractions.Items;
using ModAPI.Core.Registry.Items;
using Allumeria;
using Allumeria.ChunkManagement;
using Allumeria.EntitySystem.Components;
using Allumeria.Input;
using Allumeria.Items;
using Allumeria.UI;
using Allumeria.UI.Menus;
using Allumeria.Rendering;

namespace ModAPI.Core.Rendering;

public class ItemRenderPatcher
{
    [HarmonyPatch(typeof(ItemStack))]
    [HarmonyPatch(nameof(ItemStack.RenderSlowItemOnly))]
    public static class ItemStackRenderSlowPatch
    {
        static bool Prefix(ItemStack __instance, int x, int y)
        {
            Item item = __instance.GetItem();
            bool modItemExists = ItemRegistry.Registry.TryGetModFromGame(item, out var modItem);

            if (!modItemExists)
            {
                return true;
            }

            return false;
        }
    }   
}