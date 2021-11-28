using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Brubeck;
using Brubeck.Core;

namespace Brubeck.Peripheral
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static Monitor MonitorInstance { get; private set; } = new();
		public static App Instance { get; private set; } = new();

		[STAThread]
		public static void Start()
		{
			Instance.Run(MonitorInstance);
		}
	}
}
