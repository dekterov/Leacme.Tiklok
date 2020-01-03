// Copyright (c) 2017 Leacme (http://leac.me). View LICENSE.md for more information.
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Leacme.Lib.Tiklok {

	public class Library {

		public ReadOnlyCollection<TimeZoneInfo> TimeZones { get; } = TimeZoneInfo.GetSystemTimeZones();

		public Library() {
		}

		/// <summary>
		/// Get the formatted time string in "h:mm:ss tt" format.
		/// /// </summary>
		/// <param name="optionalTimeZoneId">Will return time based on the time zone identifier.</param>
		/// <returns></returns>
		public string GetCurrTime12HrWithSeconds(string optionalTimeZoneId = null) {
			if (optionalTimeZoneId == null) {
				return DateTime.Now.ToString("h:mm:ss tt");
			} else {
				return ConvertCurrentDateTimeToTimeZone(TimeZoneInfo.FindSystemTimeZoneById(optionalTimeZoneId)).ToString("h:mm:ss tt");
			}
		}

		/// <summary>
		/// Get the formatted time string in "HH:mm:ss" format.
		/// /// </summary>
		/// <param name="optionalTimeZoneId">Will return time based on the time zone identifier.</param>
		/// <returns></returns>
		public string GetCurrTime24HrWithSeconds(string optionalTimeZoneId = null) {
			if (optionalTimeZoneId == null) {
				return DateTime.Now.ToString("HH:mm:ss");
			} else {
				return ConvertCurrentDateTimeToTimeZone(TimeZoneInfo.FindSystemTimeZoneById(optionalTimeZoneId)).ToString("HH:mm:ss");
			}
		}

		/// <summary>
		/// Get the formatted time string in "h:mm tt" format.
		/// /// </summary>
		/// <param name="optionalTimeZoneId">Will return time based on the time zone identifier.</param>
		/// <returns></returns>
		public string GetCurrTime12HrNoSeconds(string optionalTimeZoneId = null) {
			if (optionalTimeZoneId == null) {
				return DateTime.Now.ToString("h:mm tt");
			} else {
				return ConvertCurrentDateTimeToTimeZone(TimeZoneInfo.FindSystemTimeZoneById(optionalTimeZoneId)).ToString("h:mm tt");
			}
		}

		/// <summary>
		/// Get the formatted time string in "HH:mm" format.
		/// /// </summary>
		/// <param name="optionalTimeZoneId">Will return time based on the time zone identifier.</param>
		/// <returns></returns>
		public string GetCurrTime24HrNoSeconds(string optionalTimeZoneId = null) {
			if (optionalTimeZoneId == null) {
				return DateTime.Now.ToString("HH:mm");
			} else {
				return ConvertCurrentDateTimeToTimeZone(TimeZoneInfo.FindSystemTimeZoneById(optionalTimeZoneId)).ToString("HH:mm");
			}
		}

		/// <summary>
		/// Get the formatted date string in "dddd, MMMM d, yyyy" format.
		/// /// </summary>
		/// <param name="optionalTimeZoneId">Will return date based on the time zone identifier.</param>
		/// <returns></returns>
		public string GetCurrDate(string optionalTimeZoneId = null) {
			if (optionalTimeZoneId == null) {
				return DateTime.Today.ToString("dddd, MMMM d, yyyy");
			} else {
				return ConvertCurrentDateTimeToTimeZone(TimeZoneInfo.FindSystemTimeZoneById(optionalTimeZoneId)).ToString("dddd, MMMM d, yyyy");
			}
		}

		/// <summary>
		/// Return DateTime in the specified TimeZoneInfo.
		/// /// </summary>
		/// <param name="toTimeZone"></param>
		/// <returns></returns>
		public DateTime ConvertCurrentDateTimeToTimeZone(TimeZoneInfo toTimeZone) {
			return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), toTimeZone);
		}

		/// <summary>
		///  Get the elapsed formatted time string in "h':'mm':'ss'.'fff" format.
		/// /// </summary>
		/// <param name="stopwatch">The Stopwatch instance from which to read the elapsed time.</param>
		/// <returns></returns>
		public string GetStopwatchElapsedTime(Stopwatch stopwatch) {
			return stopwatch.Elapsed.ToString("h':'mm':'ss'.'fff");
		}
	}
}