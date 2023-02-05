using HarmonyLib;
using KaosesTweaksBannerKings.Settings;
using KaosesCommon.Utils;
using TaleWorlds.CampaignSystem.GameComponents;
using KaosesTweaksBannerKings.Objects;

namespace KaosesTweaksBannerKings.Patches
{
    [HarmonyPatch(typeof(DefaultTournamentModel), "GetRenownReward")]
    class DefaultTournamentModelPatch
    {
        static bool Prefix(ref int __result)
        {
            if (!(Factory.Settings is null))
            {
                __result = Factory.Settings.TournamentRenownAmount;
                if (Factory.Settings.TournamentDebug)
                {
                    IM.MessageDebug("Patches TournamentRenownAmount Tweak: " + Factory.Settings.TournamentRenownAmount.ToString());
                }
                return false;
            }
            return true;
        }

        static bool Prepare() => Factory.Settings is { } settings && settings.TournamentRenownIncreaseEnabled;
    }
}
