using Terraria.ModLoader;

namespace ItemControl
{
    class ControllWorld : ModWorld
    {
        public override void Initialize()
        {
            ItemEdit.Karl = ModContent.GetInstance<ItemConfig>();
        }
    }
}
