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
        [Tunable]
        public static float SessionSeconds = 0;
        [Tunable]
        public static float LiveSessionSeconds = 0;
        [Tunable]
        public static float BBSessionSeconds = 0;

        public static bool HasShownTotalPlaytime = false;
        public static bool HasCreatedTimer = false;
        public static StopWatch Livetimer;
        public static StopWatch BBtimer;

        public static void OnGameStateChanged(Sims3.UI.Responder.GameSubState previousState, Sims3.UI.Responder.GameSubState newState)
        {
            try
            {
                if (newState == Sims3.UI.Responder.GameSubState.BuildMode || newState == Sims3.UI.Responder.GameSubState.BuyMode)
                {

                    if (Livetimer.IsRunning())
                    {
                        Livetimer.Stop();
                        if (PlayTime.Debugging)
                        {
                            StyledNotification.Format format = new StyledNotification.Format("stopped live timer", StyledNotification.NotificationStyle.kGameMessagePositive);
                            StyledNotification.Show(format, "arro_playtime_icon");                        
                        }
                    }

                    if (!BBtimer.IsRunning())
                    {
                        BBtimer.Start();
                        if (PlayTime.Debugging)
                        {
                            StyledNotification.Format format = new StyledNotification.Format("bb timer running", StyledNotification.NotificationStyle.kGameMessagePositive);
                            StyledNotification.Show(format, "arro_playtime_icon");                        
                        }
                    }
                }

                if (newState == Sims3.UI.Responder.GameSubState.LiveMode)
                {
                    if (BBtimer.IsRunning())
                    {
                        BBtimer.Stop();
                        if (PlayTime.Debugging)
                        {
                            StyledNotification.Format format = new StyledNotification.Format("stopped bb timer", StyledNotification.NotificationStyle.kGameMessagePositive);
                            StyledNotification.Show(format, "arro_playtime_icon");
                        }
                    }

                    if (!Livetimer.IsRunning())
                    {
                        Livetimer.Start();
                        if (!HasShownTotalPlaytime)
                        {
                            Simulator.AddObject(new OneShotFunctionTask(Arro.NotificationSystem.ShowTotalNotification, StopWatch.TickStyles.Seconds, 10f));
                            HasShownTotalPlaytime = true;
                        }
                        if (PlayTime.Debugging)
                        {
                            StyledNotification.Format format = new StyledNotification.Format("live timer running", StyledNotification.NotificationStyle.kGameMessagePositive);
                            StyledNotification.Show(format, "arro_playtime_icon");
                        }
                    }
                }
                if (newState == Sims3.UI.Responder.GameSubState.EditTown)
                {
                    if (Livetimer.IsRunning())
                    {
                        Livetimer.Stop();
                    }
                    if (BBtimer.IsRunning())
                    {
                        BBtimer.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "OnGameStateChanged");
            }
        }

        public static void InitTimers()
        {
            try
            {
                if (!HasCreatedTimer) // assuming new save/load instance and not travel
                {
                    Livetimer = StopWatch.Create(StopWatch.TickStyles.Seconds);
                    BBtimer = StopWatch.Create(StopWatch.TickStyles.Seconds);
                    HasCreatedTimer = true;
                    TimeStats.TotalPlaycount += 1;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "InitTimers");
            }
        }

        public static void DisposeTimers()
        {
            try
            {
                Livetimer.Dispose();
                BBtimer.Dispose();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "DisposeTimers");
            }
        }

        public static void StopTimers()
        {
            try
            {
                Livetimer.Stop();
                BBtimer.Stop();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "StopTimers");
            }
        }
    }
}
