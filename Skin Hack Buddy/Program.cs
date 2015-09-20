using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using EloBuddy.SDK.Utils;
using System.Net;
using Skin_Hack_Buddy;
namespace Skin_Hack_Buddy
{
    class Program
    {
        public  string Model = ObjectManager.Player.BaseSkinName;
        public  Menu Skinhackbuddy;
        public void Main(string[] args)
        {
            Loading.OnLoadingComplete += Game_Loaded;
        }
        public bool Checkskin(string champname , int id) 
        {
            System.Net.WebRequest req = default(System.Net.WebRequest);
            System.Net.WebResponse res = default(System.Net.WebResponse);

            req = System.Net.WebRequest.Create("http://ddragon.leagueoflegends.com/cdn/img/champion/splash/" + champname + "_" + id + ".jpg");

            try
            {
                res = req.GetResponse();
                return true;
            }
            catch 
            {
                return false;

              }      
        }

        public void Game_Loaded(EventArgs arg)
        {
            int skincount = 1;
          
            for (int i = 0; i < 15; i++)
            {
                Boolean skin = Checkskin(Player.Instance.ChampionName , i);
                if (skin == true)
                {
                    skincount += 1;
                }


            }
           Skinhackbuddy =  MainMenu.AddMenu("Skin Hack Buddy", "shsaeed");
           Skinhackbuddy.AddGroupLabel("Skin Hack Buddy Version 0.1");
           Skinhackbuddy.AddSeparator();
           Skinhackbuddy.AddLabel("By Saeed Suleiman AKA (Botop)");
           //Menu skinmenu = Skinhackbuddy.AddSubMenu("Skin Options");
           Skinhackbuddy.Add("Skin Index", new Slider("Skin Index", 0, 0, skincount));
                        Game.OnTick += Game_OnTick;

            Chat.Print("Skin Hack Buddy By Botop (Saeed Suleiman) Loaded. Version 0.1");

        }
        private static void Program_OnValueChange(ValueBase<int> sender, ValueBase<int>.ValueChangeArgs args)
        {
            var hero = ObjectManager.Get<AIHeroClient>().Where(x => x.BaseSkinName == sender.DisplayName.Replace("Skin ID ", "")).FirstOrDefault();
            if (hero == null)
                return;
            hero.SetSkinId(args.NewValue);
        }
        int skinidd;
        public  void Game_OnTick(EventArgs args)
        {
                        int skinid = Skinhackbuddy["Skin Index"].Cast<Slider>().CurrentValue;

                        if (skinidd == skinid)
                        {

                        }
                        else
                        {
                                //   public static string Model = ObjectManager.Player.BaseSkinName;
                            Player.SetSkinId(skinid);
                            skinidd = skinid;
                        }
        }
        public static bool Checkmodel(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && RiotAsset.Exists(name);
        
        }
    }
}
