﻿using HarmonyLib;
using KaosesTweaksBannerKings.Objects.Experience;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.Core;

namespace KaosesTweaksBannerKings.Patches
{
    class HeroPatcher
    {
        [HarmonyPatch(typeof(Hero))]//, "AddSkillXp"
        public class Patches
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Hero), "AddSkillXp")]
            public static void Prefix(Hero __instance, SkillObject skill, ref float xpAmount)
            {
                if (__instance != null && skill != null && __instance.HeroDeveloper != null && skill.GetName() != null && Hero.MainHero != null)
                {
                    KaosesAddSkillXp kaosesSkillXp = new KaosesAddSkillXp(__instance, skill, xpAmount);
                    if (kaosesSkillXp.HasModifiedXP())
                    {
                        xpAmount = kaosesSkillXp.GetNewSkillXp();
                    }
                }
            }
        }
    }
}
