using System.Collections.Generic;
using Terraria.ModLoader.Config;

namespace ItemControl
{
    class ConfigSpagetthi
    {

        public List<ItemDefinition> BannedItems { get; set; } = new List<ItemDefinition>();


        public ConfigSpagetthi(List<ItemDefinition> BannedItems)
        {
            this.BannedItems = BannedItems;
        }
        public ConfigSpagetthi()
        {

        }
    }
}
