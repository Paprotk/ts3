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
	public class Command
	{
		public static void ShowDialogSessionTime()
        {
            SimpleMessageDialog.Show("Playtime stats", "Session time: ");
        }
		
		public static void ShowDialogTotalTime()
		{
			SimpleMessageDialog.Show("Playtime stats", "Session time: ");
			StyledNotification.Show(new StyledNotification.Format("Live state", StyledNotification.NotificationStyle.kSystemMessage));
		}

	}
}
