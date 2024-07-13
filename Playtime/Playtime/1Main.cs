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
       	public static bool kInstantiator = false;
        [Tunable]
        public static bool kDebugging = true;
        [Tunable]
        public static float AlereadyAddedSeconds = 0;
        [Tunable]
    	public static bool Debugging = false;

        static PlayTime()
        {
            World.sOnWorldLoadFinishedEventHandler += new EventHandler(OnWorldLoadFinished);
            World.sOnStartupAppEventHandler += new EventHandler(OnStartupApp);
            World.sOnWorldQuitEventHandler += new EventHandler(OnWorldQuit);
            LoadSaveManager.ObjectGroupSaving += core_OnSave;
        }

        public static void OnStartupApp(object sender, EventArgs e)
        {
            Commands.sGameCommands.Register("stt", "Usage: show total time.", Commands.CommandType.General, new CommandHandler(core_ShowDialogTotalTime));
            Commands.sGameCommands.Register("tae", "Triggers artificaial exception.", Commands.CommandType.General, new CommandHandler(core_TriggerArtificialException));
            Commands.sGameCommands.Register("sst", "Usage: show session.", Commands.CommandType.General, new CommandHandler(core_ShowDialogSessionTime));
            Commands.sGameCommands.Register("SetTotalTime", "Usage: Allows user to set total time.", Commands.CommandType.General, new CommandHandler(core_SetTotalTime));
            Commands.sGameCommands.Register("astt", "Usage: adds current session time to total.", Commands.CommandType.General, new CommandHandler(core_AddSessionToTotalNow));
            Commands.sGameCommands.Register("sds", "Usage: setup detailed stats.", Commands.CommandType.General, new CommandHandler(core_SetupDetailedStats));
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
            new MailboxInteraction();
        }
        public static void OnWorldQuit(object sender, EventArgs e)
        {
        	if (!GameStates.IsTravelling) // if not traveling clear 
        	{
        		AllTimers.DisposeTimers();
        		TimerMaths.ClearTotalTimes();
        		AllTimers.HasCreatedTimer = false;
        		AllTimers.HasShownTotalPlaytime = false;
        		TimeStats.HasRunSetup = false;
        		TimeStats.TotalPlaycount = 0;
        	}
        	if (GameStates.IsTravelling)
        	{
        		AllTimers.StopTimers();
//        		AllTimers.HasShownTotalPlaytime = false;
        	}
        	
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

        public static int core_AddSessionToTotalNow(object[] parameters)
        {
            Arro.Command.AddSessionToTotalNow();
            return 1;
        }
        
        public static int core_SetTotalTime(object[] parameters)
        {
			Simulator.AddObject(new OneShotFunctionTask(Command.SetTotalTime, StopWatch.TickStyles.Seconds, 1f));
        	return 1;
        }
        
        public static int core_SetupDetailedStats(object[] parameters)
        {
        	Simulator.AddObject(new OneShotFunctionTask(NotificationSystem.Setup, StopWatch.TickStyles.Seconds, 1f));
        	return 1;
        }
        public static int core_TriggerArtificialException(object[] parameters)
        {
        	Arro.Command.TriggerArtificialException();
            return 1;
        }

        public static void core_TimerInit()
        {
            Arro.AllTimers.InitTimers();
        }
        
        public static void core_OnSave(IScriptObjectGroup group)
        {
        	Arro.Command.OnSave();
        }
    }
}