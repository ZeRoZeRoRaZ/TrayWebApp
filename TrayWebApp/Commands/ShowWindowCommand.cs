using System;
using System.Windows;
using System.Windows.Input;

namespace TrayWebApp.Commands
{
	class ShowWindowCommand : ICommand
	{
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			var window = (Window)parameter;
			var wState = window.WindowState;
			if (wState == WindowState.Minimized)
			{
				window.ShowInTaskbar = true;
				window.WindowState = WindowState.Maximized;
				window.Activate();
			}
			else
			{
				window.ShowInTaskbar = false;
				window.WindowState = WindowState.Minimized;
			}
		}
	}
}
