using HarmonyLib;
using ModAPI.Abstractions;
using ModAPI.Abstractions.Builders;
using ModAPI.Abstractions.Logging;
using ModAPI.Abstractions.Registries;
using ModAPI.Core.Registries;
using System.Reflection;
using ModAPI.Core.Helpers;


namespace ModAPI.Core
{
    public class ModApi : IModApi
    {
        private readonly ItemRegistry _items = new();

        public IItemRegistry Items => _items;

        private readonly TranslationRegistry _translations = new();
        public ITranslationRegistry Translations => _translations;

        public ILoggerFactory LoggerFactory => throw new NotImplementedException();

        private readonly ItemBuilder _itemBuilder = new ItemBuilder();
        public ItemBuilder ItemBuilder => _itemBuilder;

        public ModApi()
        {
            _translations.Initialize(); 
            AtlasHelper.InitItemTextures();

            var harmony = new Harmony("modmeria.modapi.core");

            harmony.PatchAll(Assembly.GetExecutingAssembly());

            var example = ItemBuilder.Build("example-item").SetTexture(0, 0).DisplayName("Example Item");

            Items.Register(example);
        }
    }
}
