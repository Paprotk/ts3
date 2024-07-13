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
    public class NotificationSystem
    {
        public static void ShowTotalNotification()
        {
            try
            {
                TimerMaths.ConvertSessionTimes();
                TimerMaths.ConvertTotalPlaycount();
                string convertedTotalLiveSeconds = TimerMaths.ConvertLiveTotalSeconds();
                string convertedTotalBBSeconds = TimerMaths.ConvertBBTotalSeconds();
                string convertedTotalSeconds = TimerMaths.ConvertTotalSeconds();
                string convertedPlaycount = TimerMaths.ConvertTotalPlaycount();

                string TotalPlaytimeString = LS.playtime + "\n";

                TotalPlaytimeString += convertedTotalSeconds + LS.intotal;


                if (TimeStats.ShowDetailedStats)
                {
                    TotalPlaytimeString += "\n" + convertedTotalLiveSeconds + LS.inlivemode + "\n" +
                        convertedTotalBBSeconds + LS.inbbmode;
                }
                if (TimeStats.ShowPlaycount)
                {
                	string playcounttext = "\n";
                	if (TimeStats.TotalPlaycount < 2)
                	{
                		playcounttext += String.Format(LS.youveplayed + " {0} " + LS.once, TimeStats.TotalPlaycount);
                	}
                	else
                	{
                		playcounttext += String.Format(LS.youveplayed + " {0} " + LS.times, TimeStats.TotalPlaycount);
                	}
                	TotalPlaytimeString += playcounttext;
                }


                if (Arro.TimeStats.TotalSeconds != 0)
                {
                    StyledNotification.Format format = new StyledNotification.Format(TotalPlaytimeString, StyledNotification.NotificationStyle.kGameMessagePositive);
                    StyledNotification.Show(format, "arro_playtime_icon");
                }
                else
                {
                    if (!TimeStats.HasRunSetup) // this should ensure that playtime stats setup wont show after using set total playtine
                    {
                        SimpleMessageDialog.Show(LS.playtimestatstitle, LS.noplaytimestats);
                        Simulator.AddObject(new OneShotFunctionTask(NotificationSystem.Setup, StopWatch.TickStyles.Seconds, 1f));
                        TimeStats.HasRunSetup = true;
                        StyledNotification.Format format = new StyledNotification.Format(TotalPlaytimeString, StyledNotification.NotificationStyle.kGameMessagePositive);
                        StyledNotification.Show(format, "arro_playtime_icon"); //show new total playtime after using set total time
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ShowTotalNotification");
            }
        }

        public static void Setup()
        {
            try
            {
                bool ShowDetailedStatsTrue = TwoButtonDialog.Show(LS.showdetailedstats, LS.yes, LS.no);
                if (ShowDetailedStatsTrue)
                {
                    TimeStats.ShowDetailedStats = true;
                }
                else
                {
                    TimeStats.ShowDetailedStats = false;
                }
                SetupShowPlaycount();
                return;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "Setup");
            }

        }

        public static void SetupShowPlaycount()
        {
            try
            {
                bool ShowPlaycountTrue = TwoButtonDialog.Show(LS.showplaycount, LS.yes, LS.no);
                if (ShowPlaycountTrue)
                {
                    TimeStats.ShowPlaycount = true;
                }
                else
                {
                    TimeStats.ShowPlaycount = false;
                }
                ReportCurrentSettings();
                return;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "SetupShowPlaycount");
            }
        }

        public static void ReportCurrentSettings()
        {
            try
            {
                string currentSettings = String.Format(LS.currentsettings + "\n" + LS.detailedstats + " = {0}\n" + LS.playcount + " = {1}", TimeStats.ShowDetailedStats, TimeStats.ShowPlaycount);
                StyledNotification.Format format = new StyledNotification.Format(currentSettings, StyledNotification.NotificationStyle.kGameMessagePositive);
                StyledNotification.Show(format, "arro_settings_icon");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ReportCurrentSettings");
            }

        }

        public static void ShowSessionTime()
        {
            try
            {
                TimerMaths.UpdateSessionTimes();
                TimerMaths.ConvertSessionTimes();
                string convertedLiveSession = TimerMaths.ConvertLiveSessionSeconds();
                string convertedBBSession = TimerMaths.ConvertBBSessionSeconds();
                string convertedSession = TimerMaths.ConvertSessionSeconds();

                string sessionnotif = string.Format(LS.sessionplaytime + ":\n" + convertedSession + LS.inthissession + "\n" + convertedLiveSession + LS.inlivemode + "\n" + convertedBBSession + LS.inbbmode);
                StyledNotification.Format format = new StyledNotification.Format(sessionnotif, StyledNotification.NotificationStyle.kGameMessagePositive);
                StyledNotification.Show(format, "arro_playtime_icon");
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ShowSessionTime");
            }
        }

        public static void ShowComprehensiveStats()
        {
            try
            {
                TimerMaths.UpdateSessionTimes();
                TimerMaths.ConvertSessionTimes();
                TimerMaths.ConvertTotalPlaycount();

                float livePercentage = (TimeStats.LiveTotalSeconds / TimeStats.TotalSeconds) * 100;
                float bbPercentage = (TimeStats.BBTotalSeconds / TimeStats.TotalSeconds) * 100;
                float liveSessionPercentage = (AllTimers.LiveSessionSeconds / AllTimers.SessionSeconds) * 100;
                float bbSessionPercentage = (AllTimers.BBSessionSeconds / AllTimers.SessionSeconds) * 100;

                string convertedTotalLiveSeconds = TimerMaths.ConvertLiveTotalSeconds();
                string convertedTotalBBSeconds = TimerMaths.ConvertBBTotalSeconds();
                string convertedTotalSeconds = TimerMaths.ConvertTotalSeconds();
                string convertedPlaycount = TimerMaths.ConvertTotalPlaycount();
                string convertedLiveSession = TimerMaths.ConvertLiveSessionSeconds();
                string convertedBBSession = TimerMaths.ConvertBBSessionSeconds();
                string convertedSession = TimerMaths.ConvertSessionSeconds();

                string totals = String.Format(convertedTotalSeconds + LS.intotal + "\n" + convertedTotalLiveSeconds + LS.inlivemode + "\n" +
                        convertedTotalBBSeconds + LS.inbbmode + "\n");
                string totalpercentages = String.Format(LS.fromtotaltime + " {0:F1}% " + LS.inlivemode2 + LS.and + "{1:F1}% " + LS.inbbmode2 + "\n\n", livePercentage, bbPercentage);
                string thissession = String.Format(convertedSession + LS.inthissession + "\n" + convertedLiveSession + LS.inlivemode + "\n" + convertedBBSession + LS.inbbmode + "\n");
                string sessionpercentages = String.Format(LS.fromsessiontime + " {0:F1}% " + LS.inlivemode2 + LS.and + "{1:F1}% " + LS.inbbmode2 + "\n\n", liveSessionPercentage, bbSessionPercentage);

                string playcounttext = "";

                if (TimeStats.TotalPlaycount < 2)
                {
                    playcounttext = String.Format(LS.youveplayed + " {0} " + LS.once, TimeStats.TotalPlaycount);
                }
                else
                {
                    playcounttext = String.Format(LS.youveplayed + " {0} " + LS.times, TimeStats.TotalPlaycount);
                }

                string comprehensivestats = totals + totalpercentages + thissession + sessionpercentages + playcounttext;
                SimpleMessageDialog.Show(LS.showcomprehensivestats, comprehensivestats);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex, "ShowComprehensiveStats");
            }

        }
    }
}
