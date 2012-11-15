﻿using System;
using System.Collections.Generic;

namespace YAMP.Help
{
	class HelpFunctionSection : HelpSection
	{
		public HelpFunctionSection()
		{
			Usages = new List<HelpFunctionUsage>();
		}

		public List<HelpFunctionUsage> Usages { get; private set; }
	}
}
