using Terraria.ModLoader;
using Terraria;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace ItemControl
{
	public class ItemControl : Mod
	{
        public Mod herosmod;
        public static ItemControl instance;
        public const string heropermission = "ItemControl";
        public const string heropermissiondisplayname = "Item Control Mod";
        public bool hasPermission;

        public ItemControl()
		{
            
		}

        public override void Unload()
        {
            instance = null;
            herosmod = null;
            hasPermission = new bool();
            ItemEdit.unload();
        }


        public override void PostAddRecipes()/* tModPorter Note: Removed. Use ModSystem.PostAddRecipes */
        {
            SetupHerosMod();

        }

        public override void Load()
        {
            instance = this;

            if (ModLoader.TryGetMod("HEROsMod", out Mod herosMod))
            {
                herosmod = ModLoader.GetMod("HEROsMod");
            }
            ItemEdit.init();
        }

        public ItemControl GetInstance()
        {
            return instance;
        }

        public void SetupHerosMod()
        {
            if (herosmod != null)
            {
                herosmod.Call(
                    // Special string
                    "AddPermission",
                    // Permission Name
                    heropermission,
                    // Permission Display Name
                    heropermissiondisplayname);
                /*
                if (!Main.dedServ)
                {
                    herosmod.Call(
                        // Special string
                        "AddSimpleButton",
                        // Name of Permission governing the availability of the button/tool
                        heropermission,
                        // Texture of the button. 38x38 is recommended for HERO's Mod. Also, a white outline on the icon similar to the other icons will look good.
                        //GetTexture("ItemControl"),
                        Assets.Request<Texture2D>("ItemControl"),
                        // A method that will be called when the button is clicked
                        (Action)NPCControlButtonPressed,
                        // A method that will be called when the player's permissions have changed
                        (Action<bool>)NPCControlPermissionChanged,
                        // A method that will be called when the button is hovered, returning the Tooltip
                        (Func<string>)NPCControlTooltip
                    );
                }*/
            }
        }

        public void NPCControlButtonPressed()
        {
            //If herosmod button is pressed
        }

        public void NPCControlPermissionChanged(bool Permission)
        {
            hasPermission = Permission;
        }

        public string NPCControlTooltip()
        {
            return "This button doesn't have a function yet";
        }

        public bool getPermission()
        {
            return hasPermission;
        }
    }
}