using BannerKings.Managers.Skills;
using BannerKings.Settings;
using BannerKings;
using KaosesTweaksBannerKings.Objects;
using KaosesTweaksBannerKings.Settings;
using System.Runtime;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Siege;
using TaleWorlds.Core;
using BannerKings.Models.Vanilla;

namespace KaosesTweaksBannerKings.Models
{

    class SiegeEventModel : BKSiegeEventModel
    {
        public override float GetConstructionProgressPerHour(SiegeEngineType type, SiegeEvent siegeEvent, ISiegeEventSide side)
        {
            var result = base.GetConstructionProgressPerHour(type, siegeEvent, side);

            if (Factory.Settings is { } settings)
            {
                result = result * settings.SiegeConstructionProgressPerDayMultiplier;
            }

            var effectiveSiegePartyForSide = GetEffectiveSiegePartyForSide(siegeEvent, side.BattleSide);
            if (effectiveSiegePartyForSide is { LeaderHero: { } })
            {
                var data = BannerKingsConfig.Instance.EducationManager.GetHeroEducation(effectiveSiegePartyForSide
                    .LeaderHero);
                if (data.HasPerk(BKPerks.Instance.SiegeOverseer))
                {
                    result *= 1.2f;
                }
            }

            if (BannerKingsSettings.Instance.LongerSieges > 0f)
            {
                result *= (1f - BannerKingsSettings.Instance.LongerSieges);
            }
            return result;
        }

        public override int GetColleteralDamageCasualties(SiegeEngineType siegeEngineType, MobileParty party)
        {
            if (Factory.Settings is { } settings)
                return base.GetColleteralDamageCasualties(siegeEngineType, party) + settings.SiegeCollateralDamageCasualties;
            else
                return base.GetColleteralDamageCasualties(siegeEngineType, party);
        }

        public override int GetSiegeEngineDestructionCasualties(SiegeEvent siegeEvent, BattleSideEnum side, SiegeEngineType destroyedSiegeEngine)
        {
            if (Factory.Settings is { } settings)
                return base.GetSiegeEngineDestructionCasualties(siegeEvent, side, destroyedSiegeEngine) + settings.SiegeDestructionCasualties;
            else
                return base.GetSiegeEngineDestructionCasualties(siegeEvent, side, destroyedSiegeEngine);
        }
    }
}
