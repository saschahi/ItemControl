using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;
using System;
using Terraria.ID;

namespace ItemControl
{
    class ItemEdit : ModPlayer
    {
        public static int Timer2 = 0;
        public static ItemConfig Karl = new ItemConfig();
        public static int intervall = 30;
        public static int whitelistintervall = 10;
        public static DateTime lastupdate = new DateTime();
        public static bool isAdmin;


        public override void PreUpdate()
        {
            if (Karl == null)
            {
                Karl = ModContent.GetInstance<ItemConfig>();
                lastupdate = DateTime.UtcNow;
                intervall = Karl.intervall;
            }

            if (player.inventory.Length == 0)
            {
                return;
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
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                isAdmin = ItemControl.instance.herosmod.Call("HasPermission", Main.myPlayer, ItemControl.heropermission) is bool result && result;
                            }
                            else
                            {
                                isAdmin = true;
                            }
                        }
                        catch
                        {
                            //Console.WriteLine("test");
                        }
                    }
                    if (lastupdate.AddSeconds(whitelistintervall) < DateTime.UtcNow)
                    {
                        lastupdate = DateTime.UtcNow;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            isAdmin = ItemControl.instance.herosmod.Call("HasPermission", Main.myPlayer, ItemControl.heropermission) is bool result && result;
                        }
                        else
                        {
                            isAdmin = true;
                        }
                    }
                }
                else
                {
                    isAdmin = false;
                }
            }

            

            ItemDefinition helper = new ItemDefinition();

            if (Timer2 >= intervall)
            {
                if (!isAdmin || !Karl.Whitelist)
                {
                    if (!Karl.allowPossession)
                    {
                        foreach (var item in player.inventory)
                        {
                            helper = new ItemDefinition(item.type);
                            if (Karl.BannedItems.Contains(helper))
                            {
                                if (item.IsAir)
                                {
                                    ItemDefinition helper2 = new ItemDefinition();
                                    foreach (var item2 in Karl.BannedItems.FindAll(x => x.name == ""))
                                    {
                                        Karl.BannedItems.Remove(item2);
                                    }                                 
                                    Karl.BannedItems.Remove(helper2);
                                    continue;
                                }
                                if (Karl.sendMessages)
                                {
                                    Main.NewText(item.Name + " is banned", Color.Red);
                                }
                                item.TurnToAir();
                            }
                        }

                        helper = new ItemDefinition(Main.mouseItem.type);

                        if (Karl.BannedItems.Contains(helper))
                        {
                            if (Karl.sendMessages)
                            {
                                Main.NewText(Main.mouseItem.Name + " is banned", Color.Red);
                            }
                            Main.mouseItem.TurnToAir();
                        }

                        foreach (var item in player.armor)
                        {
                            helper = new ItemDefinition(item.type);
                            if (Karl.BannedItems.Contains(helper))
                            {
                                if (Karl.sendMessages)
                                {
                                    Main.NewText(item.Name + " is banned", Color.Red);
                                }
                                item.TurnToAir();
                            }
                        }
                    }
                }

                if (Karl.GroundCheck)
                {
                    for (int i = 0; i < Main.maxItems; i++)
                    {
                        if (Main.item[i].active)
                        {
                            helper = new ItemDefinition(Main.item[i].type);
                            if (Karl.BannedItems.Contains(helper))
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

        public static void Unload()
        {
            Karl = null;
            intervall = 30;
            Timer2 = 0;
            whitelistintervall = 10;
            lastupdate = new DateTime();
        }
    }
}
