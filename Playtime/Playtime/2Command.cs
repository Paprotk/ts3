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
            try
            {
                NotificationSystem.ShowSessionTime();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ShowDialogSessionTime");
            }
        }

        public static void ShowDialogTotalTime()
        {
            try
            {
                NotificationSystem.ShowTotalNotification();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ShowDialogTotalTime");
            }
        }

        public static void AddSessionToTotalNow()
        {
            try
            {
                TimerMaths.UpdateSessionTimes();
                TimerMaths.CalculateTotalTimes();
                ShowDialogTotalTime();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "AddSessionToTotalNow");
            }
        }

        public static void SetTotalTime()
        {
            try
            {
                Command.ResetStats();
                string SetLiveTotalSeconds = StringInputDialog.Show(LS.settotaltime, "Set total seconds in live mode:", "", true);
                int Liveresult;
                int.TryParse(SetLiveTotalSeconds, out Liveresult);
                TimeStats.LiveTotalSeconds = Liveresult;

                string SetBBTotalSeconds = StringInputDialog.Show(LS.settotaltime, "Set total seconds in build/buy mode:", "", true);
                int BBresult;
                int.TryParse(SetBBTotalSeconds, out BBresult);
                TimeStats.BBTotalSeconds = BBresult;

                AllTimers.BBtimer.SetElapsedTime(0);
                AllTimers.Livetimer.SetElapsedTime(0);
                TimeStats.TotalSeconds = Liveresult + BBresult;
                TimerMaths.CalculateTotalTimes();

                AllTimers.Livetimer.Reset();
                AllTimers.BBtimer.Reset();
                AllTimers.Livetimer.Start();

                NotificationSystem.ShowTotalNotification();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetTotalTime");
            }
        }

        public static void ResetStats()
        {
            try
            {
                TimerMaths.ClearAlereadyAddedSessionSeconds();
                TimerMaths.ClearElapsedSeconds();
                TimerMaths.ClearSessionSeconds();
                TimerMaths.ClearTotalTimes();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ResetStats");
            }
        }

        public static void OnSave()
        {
            try
            {
                TimerMaths.UpdateSessionTimes();
                TimerMaths.CalculateTotalTimes();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnSave");
            }
        }
        public static void TriggerArtificialException()
        {
            try
            {
                // Artificially create a null reference exception
                object obj = null;
                obj.ToString();
            }
            catch (Exception ex)
            {
                // Handle the exception using the ExceptionHandler class
                ExceptionHandler.HandleException(ex, "TriggerArtificialException");
            }
        }
    }
}