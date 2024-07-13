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
    public class AllTimers
    {
        public static StopWatch Livetimer;
        public static StopWatch BBtimer;
        
        public static void OnGameStateChanged(Sims3.UI.Responder.GameSubState previousState, Sims3.UI.Responder.GameSubState newState)
        {
        	Sims3.UI.Responder.GameSubState gameSubState = Sims3.UI.Responder.GameSubState.BuildMode;
        	Sims3.UI.Responder.GameSubState gameSubState2 = Sims3.UI.Responder.GameSubState.BuyMode;
        	Sims3.UI.Responder.GameSubState gameSubState3 = Sims3.UI.Responder.GameSubState.LiveMode;
        	bool flag = (newState == gameSubState) || (newState == gameSubState2);
        	if (flag)
        	{
        	}
        	
        	bool flag2 = (newState == gameSubState3);
        	if (flag2)
        	{
        	}
        }

        public static void InitTimers()
        {
        }
    }
}
