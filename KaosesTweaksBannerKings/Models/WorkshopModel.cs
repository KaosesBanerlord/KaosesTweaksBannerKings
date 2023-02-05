using BannerKings.Models.Vanilla;
using KaosesTweaksBannerKings.Objects;
using KaosesTweaksBannerKings.Settings;
using System.Runtime;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameComponents;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Library;

namespace KaosesTweaksBannerKings.Models
{

    class WorkshopModel : BKWorkshopModel
    {
        // Token: 0x17000B27 RID: 2855
        // (get) Token: 0x06002CBA RID: 11450 RVA: 0x000AECEC File Offset: 0x000ACEEC
        public override int DaysForPlayerSaveWorkshopFromBankruptcy
        {
            get
            {
                if (Factory.Settings.WorkShopBankruptcyModifiers)
                {
                    return Factory.Settings.WorkShopBankruptcyValue;
                }
                return 3;
            }
        }

        public override int GetMaxWorkshopCountForTier(int tier)
        {
            if (Factory.Settings is { } settings && settings.MaxWorkshopCountTweakEnabled)
                return settings.BaseWorkshopCount + Clan.PlayerClan.Tier * settings.BonusWorkshopsPerClanTier;
            else
                return base.GetMaxWorkshopCountForTier(tier);
        }

        public override int GetBuyingCostForPlayer(Workshop workshop)
        {
            float result = 0.0f;
            if (Factory.Settings is { } settings && settings.WorkshopBuyingCostTweakEnabled && workshop != null)
            {
                result = workshop.WorkshopType.EquipmentCost + (int)workshop.Settlement.Prosperity / 2 + settings.WorkshopBaseCost;
            }
            else
            {
                result = base.GetBuyingCostForPlayer(workshop);
            }

            result += (int)result * 15f * CampaignTime.DaysInYear;

            if (workshop.Owner.OwnedWorkshops.Count == 1)
            {
                result *= 1.15f;
            }

            if (workshop.Owner.IsNotable && workshop.Owner.OwnedCommonAreas.Count == 0)
            {
                result *= 1.15f;
            }

            result *= 1f + (Hero.MainHero.OwnedWorkshops.Count * 0.05f);

            return (int)result;

        }

        public override int GetDailyExpense(int level)
        {
            if (Factory.Settings is { } settings && settings.WorkshopEffectivnessEnabled)
                return MathF.Round(base.GetDailyExpense(level) * settings.WorkshopEffectivnessv2Factor);
            else
                return base.GetDailyExpense(level);
        }
    }
}
