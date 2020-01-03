// Copyright (c) 2017 Leacme (http://leac.me). View LICENSE.md for more information.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using Leacme.Lib.Tiklok;

namespace Leacme.App.Tiklok {

	public class AppUI {

		private StackPanel rootPan = (StackPanel)Application.Current.MainWindow.Content;
		private Library lib = new Library();

		public AppUI() {

			var tTbs = App.TabControl;
			tTbs.Height = 500;

			TextBlock dateLb = GetDateLb().tb;
			dateLb.Foreground = Brushes.White;
			TextBlock timeLb = GetTimeLb().tb;
			timeLb.Foreground = Brushes.White;

			Stopwatch s = new Stopwatch();

			var stwLb = App.TextBlock;
			stwLb.TextAlignment = TextAlignment.Center;
			stwLb.FontSize = 120;
			stwLb.Text = lib.GetStopwatchElapsedTime(s);

			var stwBtHolder = App.HorizontalStackPanel;
			stwBtHolder.HorizontalAlignment = HorizontalAlignment.Center;
			var ssBt = App.Button;
			ssBt.Content = "Start";

			IDisposable displayTimer = null;

			ssBt.Click += (z, zz) => {
				if (!s.IsRunning) {
					ssBt.Content = "Stop";
					displayTimer = DispatcherTimer.Run(() => {
						s.Start();
						stwLb.Text = lib.GetStopwatchElapsedTime(s);
						return true;
					}, new TimeSpan(0, 0, 0, 0, 1));
				} else {
					ssBt.Content = "Start";
					s.Stop();
					displayTimer.Dispose();
				}
			};

			var rstBt = App.Button;
			rstBt.Content = "Reset";

			rstBt.Click += (z, zz) => {
				if (displayTimer != null) {
					displayTimer.Dispose();
				}

				if (s.IsRunning) {
					ssBt.Content = "Start";
				}
				s.Reset();
				stwLb.Text = lib.GetStopwatchElapsedTime(s);

			};

			stwBtHolder.Children.Add(ssBt);
			stwBtHolder.Children.Add(rstBt);

			TextBlock fromZoneDateLb = GetDateLb().tb;
			TextBlock fromZoneTimeLb = GetTimeLb().tb;

			var tzSelec = App.ComboBoxWithLabel;
			tzSelec.holder.HorizontalAlignment = HorizontalAlignment.Center;
			tzSelec.label.Text = "To time zone:";
			tzSelec.comboBox.Items = lib.TimeZones.Select(z => "UTC" + z.BaseUtcOffset.TotalHours + " " + z.Id);
			tzSelec.comboBox.SelectedIndex = 0;

			var ctCv = new StackPanel();
			ctCv.HorizontalAlignment = HorizontalAlignment.Stretch;
			ctCv.VerticalAlignment = VerticalAlignment.Stretch;
			ctCv.Background = Brushes.Black;

			ctCv.Children.AddRange(new List<IControl> { new Control() { Height = 130 }, dateLb, timeLb });

			var sCv = new StackPanel();
			sCv.Children.AddRange(new List<IControl> { stwLb, stwBtHolder });

			var ttzCv = new StackPanel();

			var tzdl = GetDateLb(((string)tzSelec.comboBox.SelectedItem).Substring(((string)tzSelec.comboBox.SelectedItem).IndexOf(" ") + " ".Length));
			var tztl = GetTimeLb(((string)tzSelec.comboBox.SelectedItem).Substring(((string)tzSelec.comboBox.SelectedItem).IndexOf(" ") + " ".Length));

			tzSelec.comboBox.SelectionChanged += (z, zz) => {
				tzdl.timer.Dispose();
				tztl.timer.Dispose();

				ttzCv.Children.Remove(tzdl.tb);
				ttzCv.Children.Remove(tztl.tb);

				tzdl = GetDateLb(((string)tzSelec.comboBox.SelectedItem).Substring(((string)tzSelec.comboBox.SelectedItem).IndexOf(" ") + " ".Length));
				tztl = GetTimeLb(((string)tzSelec.comboBox.SelectedItem).Substring(((string)tzSelec.comboBox.SelectedItem).IndexOf(" ") + " ".Length));

				ttzCv.Children.Add(tzdl.tb);
				ttzCv.Children.Add(tztl.tb);
			};

			ttzCv.Children.AddRange(new List<IControl> { fromZoneDateLb, fromZoneTimeLb, tzSelec.holder, tzdl.tb, tztl.tb });

			sCv.HorizontalAlignment = ttzCv.HorizontalAlignment = HorizontalAlignment.Center;
			sCv.VerticalAlignment = ttzCv.VerticalAlignment = VerticalAlignment.Center;

			tTbs.Items = new List<TabItem> {
					new TabItem() { Header = "Current time", Content = ctCv},
					new TabItem() { Header = "Stopwatch", Content = sCv },
					new TabItem() { Header = "To Time Zone", Content = ttzCv },
					};
			tTbs.SelectedIndex = 0;

			rootPan.Children.AddRange(new List<IControl> { tTbs });

		}

		private (TextBlock tb, IDisposable timer) GetTimeLb(string optionalToTimeZone = null) {
			var timeLb = App.TextBlock;
			timeLb.TextAlignment = TextAlignment.Center;
			timeLb.FontSize = 120;
			if (optionalToTimeZone == null) {
				timeLb.Text = lib.GetCurrTime12HrWithSeconds();
			} else {
				timeLb.Text = lib.GetCurrTime12HrWithSeconds(optionalToTimeZone);
			}

			var timer = DispatcherTimer.Run(() => {
				if (optionalToTimeZone == null) {
					timeLb.Text = lib.GetCurrTime12HrWithSeconds();
				} else {
					timeLb.Text = lib.GetCurrTime12HrWithSeconds(optionalToTimeZone);
				}
				return true;
			}, new TimeSpan(0, 0, 0, 1, 0));
			return (timeLb, timer);
		}

		private (TextBlock tb, IDisposable timer) GetDateLb(string optionalToTimeZone = null) {
			var dateLb = App.TextBlock;
			dateLb.TextAlignment = TextAlignment.Center;
			dateLb.FontSize = 30;
			if (optionalToTimeZone == null) {
				dateLb.Text = lib.GetCurrDate();
			} else {
				dateLb.Text = lib.GetCurrDate(optionalToTimeZone);
			}

			var timer = DispatcherTimer.Run(() => {
				if (optionalToTimeZone == null) {
					dateLb.Text = lib.GetCurrDate();
				} else {
					dateLb.Text = lib.GetCurrDate(optionalToTimeZone);
				}
				return true;
			}, new TimeSpan(0, 0, 0, 1, 0));
			return (dateLb, timer);
		}
	}
}