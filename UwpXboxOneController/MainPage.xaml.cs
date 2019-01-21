﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace UwpXboxOneController
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
		private Gamepad _Gamepad = null;

		public MainPage()
        {
            this.InitializeComponent();
        }

		private async void btnConnect_Click(object sender,
										 RoutedEventArgs e)
		{
			Gamepad.GamepadAdded += Gamepad_GamepadAdded;
			Gamepad.GamepadRemoved += Gamepad_GamepadRemoved;

			while (true)
			{
				await Dispatcher.RunAsync(
					CoreDispatcherPriority.Normal, () =>
					{
						if (_Gamepad == null)
						{
							return;
						}

						// Get the current state
						var reading = _Gamepad.GetCurrentReading();

						tbLeftTrigger.Text = reading.LeftTrigger.ToString();
						tbRightTrigger.Text = reading.RightTrigger.ToString();
						tbLeftThumbstickX.Text = reading.LeftThumbstickX.ToString();
						tbLeftThumbstickY.Text = reading.LeftThumbstickY.ToString();
						tbRightThumbstickX.Text = reading.RightThumbstickX.ToString();
						tbRightThumbstickY.Text = reading.RightThumbstickY.ToString();
						tbButtons.Text = string.Empty;
						tbButtons.Text += (reading.Buttons & GamepadButtons.A) == GamepadButtons.A ? "A " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.B) == GamepadButtons.B ? "B " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.X) == GamepadButtons.X ? "X " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.Y) == GamepadButtons.Y ? "Y " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.LeftShoulder) == GamepadButtons.LeftShoulder ? "LeftShoulder " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.RightShoulder) == GamepadButtons.RightShoulder ? "RightShoulder " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.LeftThumbstick) == GamepadButtons.LeftThumbstick ? "LeftThumbstick " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.RightThumbstick) == GamepadButtons.RightThumbstick ? "RightThumbstick " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.DPadLeft) == GamepadButtons.DPadLeft ? "DPadLeft " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.DPadRight) == GamepadButtons.DPadRight ? "DPadRight " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.DPadUp) == GamepadButtons.DPadUp ? "DPadUp " : "";
						tbButtons.Text += (reading.Buttons & GamepadButtons.DPadDown) == GamepadButtons.DPadDown ? "DPadDown " : "";
					});

				await Task.Delay(TimeSpan.FromMilliseconds(5));
			}
		}

		private async void Gamepad_GamepadRemoved(object sender,Gamepad e)
		{
			_Gamepad = null;

			await Dispatcher.RunAsync(
							   CoreDispatcherPriority.Normal, () =>
							   {
								   tbConnected.Text = "Controller removed";
							   });
		}

		private async void Gamepad_GamepadAdded(object sender, Gamepad e)
		{
			_Gamepad = e;

			await Dispatcher.RunAsync(
						 CoreDispatcherPriority.Normal, () =>
						 {
							 tbConnected.Text = "Controller added";
						 });
		}

	}
}
