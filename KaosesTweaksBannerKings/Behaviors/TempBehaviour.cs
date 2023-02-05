using KaosesCommon.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

/**
 * call with 
 campaignGameStarter.CampaignBehaviors.Add(new TempBehaviour());
in the OnGameStart
 */
namespace KaosesTweaksBannerKings.Behaviors
{

    class TempBehaviour : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.OnDailyTick));
        }

        public void OnDailyTick()
        {

            Logger.Lm(
                "OnDailyTick: Now: " + CampaignTime.Now.ToString() + "\n" +

                "OnDailyTick: DaysFromNow(-1f): " + CampaignTime.DaysFromNow(-1f) + "\n" +                  // Autumn 12, 1084
                "OnDailyTick: DaysFromNow(0f): " + CampaignTime.DaysFromNow(0f) + "\n" +                    // Autumn 13, 1084        
                "OnDailyTick: DaysFromNow(1f) : " + CampaignTime.DaysFromNow(1f) + "\n" +                   // Autumn 14, 1084
                "OnDailyTick: DaysFromNow(5f): " + CampaignTime.DaysFromNow(5f) + "\n" +                    // Autumn 18, 1084
                "OnDailyTick: CampaignTime.Now.ToDays: " + CampaignTime.Now.ToDays + "\n" +                 // 91110.1244840556
                "OnDailyTick: DaysFromNow(4f).ToDays: " + CampaignTime.DaysFromNow(4f).ToDays + "\n"        // 91114.1244840556

                );
            //SubModule.LastAttempts[Hero.OneToOneConversationHero] = CampaignTime.Now;
            //SubModule.LastAttempts[Hero.OneToOneConversationHero] = CampaignTime.DaysFromNow(CampaignTime.Now.ElapsedDaysUntilNow + 2);
            //SubModule.LastAttempts[Hero.OneToOneConversationHero] = CampaignTime.DaysFromNow(2f);
        }

        public override void SyncData(IDataStore dataStore)
        {
            //throw new NotImplementedException();
        }
    }
}
