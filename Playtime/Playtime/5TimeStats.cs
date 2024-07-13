using Sims3.SimIFace;

namespace Arro
{
	public class TimeStats
	{
		[PersistableStatic(true)]
		public static float TotalSeconds = 0;
		
		[PersistableStatic(true)]
		public static float LiveTotalSeconds = 0;
		
		[PersistableStatic(true)]
		public static float  BBTotalSeconds = 0;
		
		[PersistableStatic(true)]
		public static float  TotalPlaycount = 0;
		
		[PersistableStatic(true)]
		public static bool  ShowDetailedStats = true;
		
		[PersistableStatic(true)]
		public static bool  ShowPlaycount = true;	
	
		[PersistableStatic(true)]
		public static bool  HasRunSetup = false;			
	}
}

