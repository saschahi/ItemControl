using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.IO;

namespace ItemControl
{
    class ControlItem : GlobalItem
    {
        public static ItemConfig Karl => ItemEdit.Karl;
        public static bool isAdmin => ItemEdit.isAdmin;
        public static bool allowWhitelist => Karl.Whitelist;
        public static DateTime stopbannedspam;

        public override bool CanPickup(Item item, Player player)
        {
            if(Karl == null)
            {
                return false;
            }
            return base.CanPickup(item, player);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (Karl == null)
            {
                return false;
            }
            if (Karl.Whitelist && isAdmin)
            {
                return base.CanUseItem(item, player);
            }

            if (item.IsAir)
            {
                //you might think this is dumb, bis this is actually required
                return false;
            }

            ItemDefinition Helper = new ItemDefinition(item.type);
            if (Karl.BannedItems.Contains(Helper))
            {
                if (Karl.sendMessages && stopbannedspam.AddSeconds(2) < DateTime.UtcNow)
                {
                    stopbannedspam = DateTime.UtcNow;
                    Main.NewText(item.Name + " is banned", Color.Red);
                }
                return false;
            }
            
            return base.CanUseItem(item,player);
        }

        public override bool OnPickup(Item item, Player player)
        {
            //return base.OnPickup(item, player);
            if (Karl.checkPickup)
            {
                if (Karl == null)
                {
                    return false;
                }
                if (Karl.Whitelist && isAdmin)
                {
                    return base.OnPickup(item, player);
                }

                if (item.IsAir)
                {
                    //you might think this is dumb, bis this is actually required
                    return false;
                }

                ItemDefinition Helper = new ItemDefinition(item.type);
                if (Karl.BannedItems.Contains(Helper))
                {
                    if (Karl.sendMessages && stopbannedspam.AddSeconds(2) < DateTime.UtcNow)
                    {
                        stopbannedspam = DateTime.UtcNow;
                        Main.NewText(item.Name + " is banned", Color.Red);
                    }
                    return false;
                }
                return base.OnPickup(item, player);
            }
            return base.OnPickup(item, player);
        }

        public override bool AltFunctionUse(Item item, Player player)
        {
            if (Karl == null)
            {
                return false;
            }
            if (Karl.Whitelist && isAdmin)
            {
                return base.AltFunctionUse(item, player);
            }

            if (item.IsAir)
            {
                //you might think this is dumb, bis this is actually required
                return false;
            }

            ItemDefinition Helper = new ItemDefinition(item.type);
            if (Karl.BannedItems.Contains(Helper))
            {
                if (Karl.sendMessages && stopbannedspam.AddSeconds(2) < DateTime.UtcNow)
                {
                    stopbannedspam = DateTime.UtcNow;
                    Main.NewText(item.Name + " is banned", Color.Red);
                }
                return false;
            }

            return base.AltFunctionUse(item, player);
        }

        public override bool Shoot(Item item, Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Karl == null)
            {
                return false;
            }
            if (Karl.Whitelist && isAdmin)
            {
                return base.Shoot(item,player,ref position,ref speedX,ref speedY,ref type,ref damage,ref knockBack);
            }

            if (item.IsAir)
            {
                //you might think this is dumb, bis this is actually required
                return false;
            }

            ItemDefinition Helper = new ItemDefinition(item.type);
            if (Karl.BannedItems.Contains(Helper))
            {
                if (Karl.sendMessages && stopbannedspam.AddSeconds(2) < DateTime.UtcNow)
                {
                    stopbannedspam = DateTime.UtcNow;
                    Main.NewText(item.Name + " is banned", Color.Red);
                }
                return false;
            }

            return base.Shoot(item, player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}
