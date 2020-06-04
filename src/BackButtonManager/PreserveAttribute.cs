﻿#pragma warning disable IDE1006 // Naming Styles
using System;
using System.Collections.Generic;
using System.Text;

namespace Chinook.BackButtonManager
{
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class PreserveAttribute : Attribute
	{
		public bool AllMembers;
	}
}
#pragma warning restore IDE1006 // Naming Styles
