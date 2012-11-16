﻿using System;
using System.Threading;

namespace YAMP
{
	[Description("Enables the possibility to make a short break - computationally. Sets the computation thread on idle for a number of ms.")]
	[Kind(PopularKinds.System)]
	class SleepFunction : SystemFunction
	{
		[Description("Sets the computation thread on idle for the proposed time in milliseconds (ms).")]
		[Example("sleep(150)", "Sleeps for 150ms and outputs the real waiting time in ms.")]
		public ScalarValue Function(ScalarValue a)
		{
			var start = Environment.TickCount;

			using (var blocking = new ManualResetEvent(false))
			{
				blocking.WaitOne(a.IntValue);
			}

			var time = Environment.TickCount - start;
			return new ScalarValue(time);
		}
	}
}
