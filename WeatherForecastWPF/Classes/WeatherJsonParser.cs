﻿using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Linq;

namespace WeatherForecastWPF.Classes
{
	public class WeatherJsonParser
	{
		public WeatherJsonParser(string Json)
		{
			var jsonObject = JObject.Parse(Json);

		}
	}
}
