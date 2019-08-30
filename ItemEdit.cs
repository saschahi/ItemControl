using Terraria.ModLoader;
using Terraria;
using Terraria.ModLoader.Config;
using Microsoft.Xna.Framework;
using Terraria.ID;


namespace ItemControl
{
    class ItemEdit : ModPlayer
    {
        //public static int Timer = 3001;
        public static int Timer2 = 0;
        public static ItemConfig Karl = new ItemConfig();

        public override void PreUpdate()
        {
            if (Timer2 >= 30)
            {
                
                /*if (Timer >= 3000)
                {
                    Karl = mod.GetConfig<ItemConfig>();
                }*/

                ItemDefinition test = new ItemDefinition();

                foreach (var item in player.inventory)
                {
                    test = new ItemDefinition(item.type);
                    if (Karl.BannedItems.Contains(test))
                    {
                        Main.NewText(item.Name + " is banned", Color.Red);
                        item.TurnToAir();
                        
                    }
                }

                test = new ItemDefinition(Main.mouseItem.type);

                if(Karl.BannedItems.Contains(test))
                {
                    Main.NewText(Main.mouseItem.Name + " is banned", Color.Red);
                    Main.mouseItem.TurnToAir();
                }

                foreach (var item in player.armor)
                {
                    test = new ItemDefinition(item.type);
                    if (Karl.BannedItems.Contains(test))
                    {
                        Main.NewText(item.Name + " is banned", Color.Red);
                        item.TurnToAir();
                        
                    }
                }
                //Timer = 0;
                
                
            }
            Timer2++;
            //Timer++;
        }


        
    }
}
