﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Chinook.BackButtonManager
{
	/// <summary>
	/// This class exposes the configuration for the <see cref="Chinook.BackButtonManager"/> namespace.
	/// </summary>
	public static class BackButtonManagerConfiguration
	{
		/// <summary>
		/// Gets or sets the <see cref="ILoggerFactory"/> used by all classes under the <see cref="Chinook.BackButtonManager"/> namespace.
		/// The default value is a <see cref="NullLoggerFactory"/> instance.
		/// </summary>
		public static ILoggerFactory LoggerFactory { get; set; } = new NullLoggerFactory();

		internal static ILogger<T> Log<T>(this T _)
		{
			return LoggerFactory.CreateLogger<T>();
		}

		internal static ILogger Log(this Type type)
		{
			return LoggerFactory.CreateLogger(type);
		}
	}
}
