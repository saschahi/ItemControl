using System.Collections.Generic;
using Terraria.ModLoader.Config;
using Terraria.ModLoader;
using System.ComponentModel;

namespace ItemControl
{
    class ItemConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // tModLoader will automatically populate a public static field named Instance with the active instance of this ModConfig. (It will unload it too.)
        // This reduces code from "mod.GetConfig<ExampleConfigServer>().DisableExampleWings" to "ExampleConfigServer.Instance.DisableExampleWings". It's just a style choice.
        //public static ItemConfig Instance;

        [Label("Send Item Banned message?")]
        [Tooltip("Enable/Disable the 'This Item is Banned' Message.")]
        public bool sendMessages { get; set; } = new bool();

        [Label("Should we only deny the usage of banned items?")]
        [Tooltip("Beware: some modded items might work around this feature")]
        [DefaultValue(false)]
        public bool allowPossession { get; set; } = new bool();

        [Label("Check Items on ground too?")]
        [Tooltip("If Items on the ground should also be checked and removed if banned?")]
        [DefaultValue(false)]
        public bool GroundCheck { get; set; } = new bool();

        [Label("Check Items on Pickup?")]
        [Tooltip("Deletes banned items if picked up from the ground")]
        [DefaultValue(false)]
        public bool checkPickup { get; set; } = new bool();

        [Label("Check Intervall")]
        [Tooltip("The Intervall in Ticks that inventories are checked in. A intervall of 0 defaults to 30.")]
        [Range(1, 500)]
        [Slider]
        [DefaultValue(20)]
        public int intervall { get; set; } = new int();

        [Label("Whitelist Administrators?")]
        [Tooltip("Allows Itemcontrol-Admins to bypass the check")]
        public bool Whitelist { get; set; } = new bool();

        [Label("BEWARE")]
        [Tooltip("Before a Administrator logs into herosmod, he is considered to be NOT an Admin. Items can still be removed this way.")]
        public bool whitelistbeware { get; } = false;

        [Label("Banned Items")]
        [Tooltip("All Items in this list will be banned from possesion.")]

        public List<ItemDefinition> BannedItems { get; set; } = new List<ItemDefinition>();

        public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
        {
            if (ItemControl.instance.herosmod != null)
            {
                //find a better alternative?
                if (ItemControl.instance.herosmod.Call("HasPermission", whoAmI, ItemControl.heropermission) is bool result && result)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
            return false;
        }

        public override void OnChanged()
        {
            ItemEdit.Karl = ModContent.GetInstance<ItemConfig>();
            int safetysave = 30;
            if (intervall <= 0)
            {
                ItemEdit.intervall = safetysave;
            }
            else
            {
                ItemEdit.intervall = intervall;
            }
        }
    }
}
