using HarmonyLib;
using Il2Cpp;

using MelonLoader;


[assembly: MelonInfo(typeof(Demon_Bluff_Unofficial_Demo_Fixes.Core), "Demon Bluff Demo Fixes", "0.1.0", "tulxoro", null)]
[assembly: MelonGame("UmiArt", "Demon Bluff")]

namespace Demon_Bluff_Unofficial_Demo_Fixes
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            // Apply the harmony patch
            var harmony = new HarmonyLib.Harmony("tozeon.Demon.Bluff.Patch");
            harmony.PatchAll();
        }
    }

    [HarmonyPatch(typeof(Health), "Damage")]
    public static class HealthDamagePatch
    {

        [HarmonyPostfix]
        public static void DamagePostfix(Health __instance, int value)
        {

            if (PlayerController.PlayerInfo != null && __instance == PlayerController.PlayerInfo.health)
            {
                if (__instance.value.GetValue() <= 0)
                {
                    var winConditions = UnityEngine.Object.FindObjectOfType<WinConditions>();

                    if (winConditions != null)
                        winConditions.LoseNormal();
                    Gameplay.ChangeGameplayState(EGameplayState.Summary);

                    Il2CppSystem.Collections.Generic.List<Character> allChars = new Il2CppSystem.Collections.Generic.List<Character>(Gameplay.CurrentCharacters.Pointer);

                    for (int i = 0; i < allChars.Count; i++)
                    {
                        allChars[i].Reveal();
                    }

                }
            }
        }
    }



}