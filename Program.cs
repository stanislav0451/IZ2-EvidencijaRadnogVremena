using System;
using Gtk;

namespace projekt
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Application.Init();
			baza.Baza();
			MainWindow win = new MainWindow();
			win.Show();
			Application.Run();
		}
	}
}
