using KaosesCommon.Objects;
using KaosesCommon.Utils;
using KaosesTweaksBannerKings.Objects.PartySpeeds;
using KaosesTweaksBannerKings.Settings;
using System.Collections.Concurrent;
using System.Reflection;

namespace KaosesTweaksBannerKings.Objects
{
    /// <summary>
    /// KaosesTweaksBannerKings Factory Object
    /// </summary>
    public static class Factory
    {
        /// <summary>
        /// Variable to hold the MCM settings object
        /// </summary>
        private static Config? _settings = null;

        /// <summary>
        /// Bool indicates if MCM is a loaded mod
        /// </summary>
        public static bool MCMModuleLoaded { get; set; } = false;

        //~ KT Party Speeds
        /* KaosesPartySpeeds */
        private static ConcurrentDictionary<string, KaosesFleeingPartySpeed> _KaosesFleeingPartiesList = new ConcurrentDictionary<string, KaosesFleeingPartySpeed>();
        private static FleeingPartiesManager _fleeingPartiesdManager = new FleeingPartiesManager();
        //~ KT Party Speeds
        /* KaosesPartySpeeds */


        private static InfoMgr? _im = null;

        public static InfoMgr IM
        {
            get
            {
                return _im;
            }
            set
            {
                _im = value;
            }
        }

        /// <summary>
        /// MCM Settings Object Instance
        /// </summary>
        public static Config Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = Config.Instance;
                    if (_settings is null)
                    {
                        Factory.IM.ShowMessageBox("Kaoses Tweaks BannerKings Failed to load MCM config provider", "Kaoses Tweaks BannerKings MCM Error");
                    }
                }
                return _settings;
            }
            set
            {
                _settings = value;
            }
        }

        /// <summary>
        /// Mod version
        /// </summary>
        public static string ModVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Unused mod config file path
        /// </summary>
        private static string ConfigFilePath
        {

            get
            {
                return @"..\\..\\Modules\\" + SubModule.ModuleId + "\\config.json";
            }
            //set {  = value; }

        }
        //~ KT Party Speeds
        /* KaosesPartySpeeds */
        public static ConcurrentDictionary<string, KaosesFleeingPartySpeed> KaosesFleeingPartiesList
        {
            get
            {
                if (_KaosesFleeingPartiesList != null)
                {
                    return _KaosesFleeingPartiesList;
                }
                else
                {
                    _KaosesFleeingPartiesList = new ConcurrentDictionary<string, KaosesFleeingPartySpeed>();
                    return _KaosesFleeingPartiesList;
                }
            }
            set => _KaosesFleeingPartiesList = value;

        }

        public static FleeingPartiesManager FleeingPartiesMgr
        {
            get
            {
                if (_fleeingPartiesdManager != null)
                {
                    return _fleeingPartiesdManager;
                }
                else
                {
                    _fleeingPartiesdManager = new FleeingPartiesManager();
                    return _fleeingPartiesdManager;
                }
            }
            set => _fleeingPartiesdManager = value;

        }
        //~ KT Party Speeds
        /* KaosesPartySpeeds */

    }
}
