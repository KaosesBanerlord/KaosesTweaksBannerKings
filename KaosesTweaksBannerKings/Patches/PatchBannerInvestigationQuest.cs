using HarmonyLib;
using KaosesTweaksBannerKings.Objects;
using StoryMode.Quests.FirstPhase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaosesTweaksBannerKings.Patches
{
    [HarmonyPatch(typeof(BannerInvestigationQuest), "OnStartQuest")]
    public class PatchBannerInvestigationQuest
    {
        // Skip the first quest "Investigate Neretzes' Folly".
        private static void Postfix(BannerInvestigationQuest __instance)
        {
            if (!Factory.Settings.AutoCompleteBannerQuest)
            {
                return;
            }
            __instance.CompleteQuestWithSuccess();
        }
        //static bool Prepare => Factory.Settings is { } settings && settings.AutoCompleteBannerQuest;
    }
}
