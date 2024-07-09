using System;
using Sims3.Gameplay;
using Sims3.Gameplay.Core;
using Sims3.Gameplay.Utilities;
using Sims3.SimIFace;
using Sims3.UI;
using Sims3.UI.Hud;
using OneShotFunctionTask = Sims3.Gameplay.OneShotFunctionTask;
using System.Collections.Generic;
using System.Text;

namespace Arro
{
	
    public class PlayTime
    {
        [Tunable]
        protected static bool kInstantiator = false;
        [Tunable]
        protected static bool kDebugging = true;
        [Tunable]
        protected static float AlereadyAddedSeconds = 0;

        private static string formattedTime; 

        static PlayTime()
        {
            World.sOnWorldLoadFinishedEventHandler += new EventHandler(OnWorldLoadFinished);
            World.sOnStartupAppEventHandler += new EventHandler(OnStartupApp);
        }
         public static void OnStartupApp(object sender, EventArgs e)
         {
         	Commands.sGameCommands.Register("stt", "Usage: showinfo.", Commands.CommandType.General, new CommandHandler(core_ShowDialogTotalTime));
            Commands.sGameCommands.Register("sst", "Usage: showinfo.", Commands.CommandType.General, new CommandHandler(core_ShowDialogSessionTime));
         }

        public static void OnWorldLoadFinished(object sender, EventArgs e)
        {
            bool flag = Sims3.Gameplay.UI.Responder.Instance != null;
			if (flag)
			{
				Sims3.Gameplay.UI.Responder instance = Sims3.Gameplay.UI.Responder.Instance;
				instance.GameStateChanging = (GameStateChangingDelegate)Delegate.Remove(instance.GameStateChanging, new GameStateChangingDelegate(AllTimers.OnGameStateChanged));
				Sims3.Gameplay.UI.Responder instance2 = Sims3.Gameplay.UI.Responder.Instance;
				instance2.GameStateChanging = (GameStateChangingDelegate)Delegate.Combine(instance2.GameStateChanging, new GameStateChangingDelegate(AllTimers.OnGameStateChanged));
			}
            core_TimerInit();
            
        }
        
        public static int core_ShowDialogTotalTime(object[] parameters)
        {
        	Arro.Command.ShowDialogTotalTime();
        	return 1;
        }
        
        public static int core_ShowDialogSessionTime(object[] parameters)
        {
        	Arro.Command.ShowDialogSessionTime();
            return 1;
        }
       
        
        public static void core_TimerInit()
        {
        	Arro.AllTimers.InitTimers();
        }
    }
}
