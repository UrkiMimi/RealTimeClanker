using IPA;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;
using HarmonyLib;
using System.Reflection;

namespace RealTimeClanker
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }


        private Assembly executingAssembly = Assembly.GetExecutingAssembly();


        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;
        }

        [OnStart]
        public void OnApplicationStart()
        {
            Log.Debug("Trying to arrive here...");
            new GameObject("RTController").AddComponent<RTController>();
            
            HarmonyLib.Harmony harmony = new HarmonyLib.Harmony("com.urkimimi.rtclanker");
            Log.Debug("Applying Harmony patches");
            harmony.PatchAll();
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            Log.Debug("OnApplicationQuit");
            //harmony.UnpatchSelf();
        }
    }
}
