using HarmonyLib;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace FullBarFishing
{
    public class ModEntry : Mod
    {
        private static ModConfig Config=new();
        public override void Entry(IModHelper helper)
        
        {
            Config = Helper.ReadConfig<ModConfig>();
            var harmony = new Harmony(ModManifest.UniqueID);

            harmony.Patch(
                original: AccessTools.Method(typeof(BobberBar), "update"),
                postfix: new HarmonyMethod(typeof(ModEntry), nameof(BobberBarUpdate))
            );
        }

        public static void BobberBarUpdate(BobberBar __instance)
        {
            try
            {
                var field = AccessTools.Field(typeof(BobberBar), "bobberBarHeight");

                if (field != null)
                {
                    field.SetValue(__instance, Config.BarSize);
                }
            }
            catch
            {
            }
        }
    }
}