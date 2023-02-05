using BannerKings.Managers.Education.Lifestyles;
using BannerKings.Managers.Skills;
using BannerKings;
using BannerKings.Models.Vanilla;
using Helpers;
using KaosesTweaksBannerKings.Objects;
using System;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;

namespace KaosesTweaksBannerKings.Models
{

    class MobilePartyFoodConsumptionModel : BKPartyConsumptionModel
    {
        // Token: 0x06002DFD RID: 11773 RVA: 0x000BB4A4 File Offset: 0x000B96A4
        public override ExplainedNumber CalculateDailyFoodConsumptionf(MobileParty party, ExplainedNumber baseConsumption)
        {
            int num = 0;
            for (int i = 0; i < party.MemberRoster.Count; i++)
            {
                if (party.MemberRoster.GetCharacterAtIndex(i).Culture.IsBandit)
                {
                    num += party.MemberRoster.GetElementNumber(i);
                }
            }
            for (int j = 0; j < party.PrisonRoster.Count; j++)
            {
                if (party.PrisonRoster.GetCharacterAtIndex(j).Culture.IsBandit)
                {
                    num += party.PrisonRoster.GetElementNumber(j);
                }
            }
            int num2 = party.Party.NumberOfAllMembers + party.Party.NumberOfPrisoners / 2;
            if (party.LeaderHero != null && party.LeaderHero.CharacterObject.GetPerkValue(DefaultPerks.Roguery.Promises) && num != 0)
            {
                num2 += (int)(num * DefaultPerks.Roguery.Promises.PrimaryBonus * 0.01f);
            }
            num2 = (num2 < 1) ? 1 : num2;
            float baseNumber = -num2 / 20f;
            //~ KT
            if (Factory.Settings.PartyFoodConsumptionEnabled)
            {
                //IM.MessageDebug("PartyFoodConsumption: original: " + baseNumber.ToString());
                baseNumber *= Factory.Settings.PartyFoodConsumptionMultiplier;
                //IM.MessageDebug("PartyFoodConsumption: modified: " + baseNumber.ToString());
            }
            //~ KT
            ExplainedNumber result = new ExplainedNumber(baseNumber, false);
            CalculatePerkEffects(party, ref result);
            var leader = party.LeaderHero;

            if (leader != null)
            {
                var data = BannerKingsConfig.Instance.EducationManager.GetHeroEducation(leader);
                var faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(party.CurrentNavigationFace);

                if (data.HasPerk(BKPerks.Instance.KheshigRaider) && faceTerrainType == TerrainType.Plain ||
                       faceTerrainType == TerrainType.Steppe)
                {
                    var cow = Game.Current.ObjectManager.GetObject<ItemObject>("cow");
                    int cattleHeads = party.ItemRoster.GetItemNumber(cow);

                    result.Add(cattleHeads * -0.06f, BKPerks.Instance.KheshigRaider.Name);
                }

                if (party.Army != null && party.SiegeEvent != null)
                {
                    var armyLeader = party.Army.LeaderParty.LeaderHero;
                    var armyEducation = BannerKingsConfig.Instance.EducationManager.GetHeroEducation(armyLeader);
                    if (armyEducation.HasPerk(BKPerks.Instance.SiegeOverseer))
                    {
                        result.AddFactor(-0.15f, BKPerks.Instance.SiegeOverseer.Name);
                    }

                    if (faceTerrainType == TerrainType.Desert && armyEducation.Lifestyle != null &&
                        armyEducation.Lifestyle.Equals(DefaultLifestyles.Instance.Jawwal))
                    {
                        result.AddFactor(-0.3f, DefaultLifestyles.Instance.Jawwal.Name);
                    }
                }
            }

            return result;
        }


        // Token: 0x06002DFE RID: 11774 RVA: 0x000BB5B0 File Offset: 0x000B97B0
        private void CalculatePerkEffects(MobileParty party, ref ExplainedNumber result)
        {
            PerkHelper.AddPerkBonusForParty(DefaultPerks.Athletics.Spartan, party, false, ref result);
            PerkHelper.AddPerkBonusForParty(DefaultPerks.Steward.Spartan, party, true, ref result);
            if (party.EffectiveQuartermaster != null)
            {
                PerkHelper.AddEpicPerkBonusForCharacter(DefaultPerks.Steward.PriceOfLoyalty, party.EffectiveQuartermaster.CharacterObject, DefaultSkills.Steward, true, ref result, 200);
            }
            TerrainType faceTerrainType = Campaign.Current.MapSceneWrapper.GetFaceTerrainType(party.CurrentNavigationFace);
            if (faceTerrainType == TerrainType.Forest || faceTerrainType == TerrainType.Steppe)
            {
                PerkHelper.AddPerkBonusForParty(DefaultPerks.Scouting.Foragers, party, true, ref result);
            }
            if (party.IsGarrison && party.CurrentSettlement != null && party.CurrentSettlement.Town.IsUnderSiege)
            {
                PerkHelper.AddPerkBonusForTown(DefaultPerks.Athletics.StrongLegs, party.CurrentSettlement.Town, ref result);
            }
            if (party.Army != null)
            {
                PerkHelper.AddPerkBonusForParty(DefaultPerks.Steward.StiffUpperLip, party, true, ref result);
            }
            SiegeEvent siegeEvent = party.SiegeEvent;
            if (((siegeEvent != null) ? siegeEvent.BesiegerCamp : null) != null && party.SiegeEvent.BesiegerCamp.BesiegerParty == party && party.HasPerk(DefaultPerks.Steward.SoundReserves, true))
            {
                PerkHelper.AddPerkBonusForParty(DefaultPerks.Steward.SoundReserves, party, false, ref result);
            }
        }




    }
}
