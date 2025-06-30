using ModAPI.Abstractions.Config;

namespace ModAPI.Core.Config
{
    public class ItemConfig : IItemConfig
    {
        public string? Id { get; private set; }
        public int TextureX { get; private set; } = 0;
        public int TextureY { get; private set; } = 0;

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

        public IItemConfig SetItemTranslation(string name)
        {
            if (this.Id != null)
            {
                ModApi.Api.AddTranslation($"item.{this.Id}", name);
            }
            else
            {
                Console.WriteLine("[ModMeria] WARN: Tried to add translation to item with no ID.");
            }

            return this;
        }

        public IItemConfig SetItemTranslation(string name, string description)
        {
            if (this.Id != null)
            {
                ModApi.Api.AddTranslation($"item.{this.Id}", name, description);
            }
            else
            {
                Console.WriteLine("[ModMeria] WARN: Tried to add translation to item with no ID.");
            }

            return this;
        }
    }
}