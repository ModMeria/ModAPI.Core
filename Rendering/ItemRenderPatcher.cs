using HarmonyLib;
using ModAPI.Abstractions.Items;
using ModAPI.Core.Registry.Items;
using PocketBlocks;
using PocketBlocks.ChunkManagement;
using PocketBlocks.EntitySystem.Components;
using PocketBlocks.Input;
using PocketBlocks.Items;
using PocketBlocks.UI;
using PocketBlocks.UI.Menus;
using PocketBlocks.Rendering;

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