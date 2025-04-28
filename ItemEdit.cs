using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;
using System;

namespace ItemControl
{
    class ItemEdit : ModPlayer
    {
        private static int Timer2;
        public static ItemConfig Karl;
        public static int intervall;
        private static int whitelistintervall;
        private static DateTime lastupdate;
        private static bool isAdmin;
        private static bool firsttime;
        private static DateTime stopbannedspam;

        public static void init()
        {
            Timer2 = 0;
            intervall = 30;
            whitelistintervall = 10;
            lastupdate = new DateTime();
            isAdmin = false;
            firsttime = true;
            stopbannedspam = new DateTime();
        }

        public static void unload()
        {
            Timer2 = new int();
            Karl = null;
            intervall = new int();
            whitelistintervall = new int();
            lastupdate = new DateTime();
            isAdmin = new bool();
            firsttime = new bool();
            stopbannedspam = new DateTime();
        }


        public override bool CanUseItem(Item item)
        {
            if (Karl == null)
            {
                Karl = ModContent.GetInstance<ItemConfig>();
                if (Karl == null)
                {
                    Main.NewText("A Severe Error has occured withing ItemControl", Color.Red);
                    return false;
                }
            }

            if(Karl.Whitelist && isAdmin)
            {
                return base.CanUseItem(item);
            }

            //ItemDefinition Helper = new ItemDefinition(item.type);
            if (Karl.BannedItems.Find(x => x.Type == item.type) != null)
            {
                if(Karl.sendMessages && stopbannedspam.AddSeconds(2) < DateTime.UtcNow)
                {
                    stopbannedspam = DateTime.UtcNow;
                    Main.NewText(item.Name + " is banned", Color.Red);
                }
                return false;
            }
            return base.CanUseItem(item);
        }

        public override void PreUpdate()
        {
            if (firsttime)
            {
                Karl = ModContent.GetInstance<ItemConfig>();
                lastupdate = DateTime.UtcNow;
                intervall = Karl.intervall;
                //I fucking hate how inconsistens some of this can be
                firsttime = false;
            }

            if (ItemControl.instance.herosmod != null)
            {
                if (Karl.Whitelist)
                {
                    /*lastupdate = DateTime.UtcNow;
                    try
                    {
                        isAdmin = ItemControl.instance.herosmod.Call("HasPermission", Main.myPlayer, ItemControl.heropermission) is bool result && result;
                    }
                    catch
                    {
                        Console.WriteLine("test");
                    }*/
                    
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
                if (!Karl.allowPossession)
                {
                    if (Timer2 >= intervall)
                    {
                        if (Karl == null)
                        {
                            //yes, getting the config is THAT inconsistent sometimes
                            Karl = ModContent.GetInstance<ItemConfig>();
                        }

                        //ItemDefinition test = new ItemDefinition();
                        foreach (var item in Player.inventory)
                        //foreach (var item in player.inventory)
                        {
                            //test = new ItemDefinition(item.type);
                            if (Karl.BannedItems.Find(x => x.Type == item.type) != null)
                            {
                                if (Karl.sendMessages)
                                {
                                    Main.NewText(item.Name + " is banned", Color.Red);
                                }
                                item.TurnToAir();
                            }
                        }

                        //test = new ItemDefinition(Main.mouseItem.type);

                        if (Karl.BannedItems.Find(x => x.Type == Main.mouseItem.type) != null)
                        {
                            if (Karl.sendMessages)
                            {
                                Main.NewText(Main.mouseItem.Name + " is banned", Color.Red);
                            }
                            Main.mouseItem.TurnToAir();
                        }
                        foreach (var item in Player.armor)
                        //foreach (var item in player.armor)
                        {
                            //test = new ItemDefinition(item.type);
                            if (Karl.BannedItems.Find(x => x.Type == item.type) != null)
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
                                    //test = new ItemDefinition(Main.item[i].type);
                                    if (Karl.BannedItems.Find(x => x.Type == Main.item[i].type) != null)
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
        public override void OnEnterWorld()
        {
            Karl = ModContent.GetInstance<ItemConfig>();
            intervall = Karl.intervall;
        }

        public override void Unload()
        {
            Karl = null;
            intervall = 30;
            Timer2 = 0;
            whitelistintervall = 10;
            lastupdate = new DateTime();
        }
    }
}
