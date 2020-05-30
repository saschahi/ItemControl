using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;
using System;

namespace ItemControl
{
    class ItemEdit : ModPlayer
    {
        public static int Timer2 = 0;
        public static ItemConfig Karl = new ItemConfig();
        public static int intervall = 30;
        public static int whitelistintervall = 10;
        public static DateTime lastupdate;
        public bool isAdmin;

        public override void PreUpdate()
        {
            if (Karl == null)
            {
                Karl = ModContent.GetInstance<ItemConfig>();
                lastupdate = DateTime.UtcNow;
                intervall = Karl.intervall;
            }

            if (ItemControl.instance.herosmod != null)
            {
                if (Karl.Whitelist)
                {
                    if (lastupdate == null)
                    {
                        lastupdate = DateTime.UtcNow;
                        try
                        {
                            isAdmin = ItemControl.instance.herosmod.Call("HasPermission", Main.myPlayer, ItemControl.heropermission) is bool result && result;
                        }
                        catch
                        {
                            Console.WriteLine("test");
                        }
                    }
                    if (lastupdate.AddSeconds(whitelistintervall) < DateTime.UtcNow)
                    {
                        lastupdate = DateTime.UtcNow;
                        isAdmin = ItemControl.instance.herosmod.Call("HasPermission", Main.myPlayer, ItemControl.heropermission) is bool result && result;
                    }
                }
                else
                {
                    isAdmin = false;
                }
            }

            if (!isAdmin)
            {
                if (Timer2 >= intervall)
                {
                    if (Karl == null)
                    {
                        Karl = ModContent.GetInstance<ItemConfig>();
                    }

                    ItemDefinition test = new ItemDefinition();

                    foreach (var item in player.inventory)
                    {
                        test = new ItemDefinition(item.type);
                        if (Karl.BannedItems.Contains(test))
                        {
                            if (Karl.sendMessages)
                            {
                                Main.NewText(item.Name + " is banned", Color.Red);
                            }
                            item.TurnToAir();
                        }
                    }

                    test = new ItemDefinition(Main.mouseItem.type);

                    if (Karl.BannedItems.Contains(test))
                    {
                        if (Karl.sendMessages)
                        {
                            Main.NewText(Main.mouseItem.Name + " is banned", Color.Red);
                        }
                        Main.mouseItem.TurnToAir();
                    }

                    foreach (var item in player.armor)
                    {
                        test = new ItemDefinition(item.type);
                        if (Karl.BannedItems.Contains(test))
                        {
                            if (Karl.sendMessages)
                            {
                                Main.NewText(item.Name + " is banned", Color.Red);
                            }
                            item.TurnToAir();
                        }
                    }

                    if (Karl.GroundCheck)
                    {

                        for (int i = 0; i < Main.maxItems; i++)
                        {
                            if (Main.item[i].active)
                            {
                                test = new ItemDefinition(Main.item[i].type);
                                if (Karl.BannedItems.Contains(test))
                                {
                                    Main.item[i].active = false;
                                }
                            }
                        }
                        

                    }
                    Timer2 = 0;
                }
                Timer2++;
            }
        }
    }
}
