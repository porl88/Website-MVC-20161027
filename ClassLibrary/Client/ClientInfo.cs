namespace ClassLibrary.Client
{
	using System;
	using System.Text.RegularExpressions;
	using System.Web;

	// USER AGENT EXAMPLES:
	// CHROME:  Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.4 (KHTML, like Gecko) Chrome/22.0.1229.92 Safari/537.4
	// FIREFOX: Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20100101 Firefox/15.0.1
	// IE:		Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Win64; x64; Trident/5.0)
	// OPERA:	Opera/9.80 (Windows NT 6.1; U; en) Presto/2.9.168 Version/11.51
	// SAFARI:	Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/534.57.2 (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2

	//MAC/SAFARI:		Mozilla/5.0 (Macintosh; Intel Mac OS X 10_6_8) AppleWebKit/537.13+ (KHTML, like Gecko) Version/5.1.7 Safari/534.57.2
	//IPAD:				Mozilla/5.0 (iPad; CPU OS 5_1 like Mac OS X; en-us) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9B176 Safari/7534.48.3
	//Windows Phone:	Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0; HTC; Radar 4G)

	public enum Browser
	{
		Unknown,
		Chrome,
		Firefox,
		IE,
		Opera,
		Safari
	}

	public enum RenderingEngine
	{
		Unknown,
		WebKit,
		Gecko,
		Trident,
		Presto,
		Blink
	}

	public enum Platform
	{
		Unknown,
		Windows,
		Mac,
		Android,
		Linux
	}

	public enum Device
	{
		Unknown,
		iPad,
		iPhone
	}

	public enum DeviceType
	{
		Unknown,
		Phone,
		Tablet,
		Desktop
	}

	public class ClientInfo
	{
		private readonly string userAgent;

		public ClientInfo(HttpContextBase context)
		{
			this.userAgent = context.Request.UserAgent;
		}

		public ClientInfo(string userAgent)
		{
			this.userAgent = userAgent;
		}

		public bool IsCrawler
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.userAgent))
				{
					const string pattern = @"([Ss]pider|[Cc]rawler|Bot|robot|bot\b)";
					return Regex.IsMatch(this.userAgent, pattern);
				}

				return false;
			}
		}

		#region Browser

		public Browser Browser
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.userAgent)) return Browser.Unknown;
				if (this.userAgent.Contains("OPR") || this.userAgent.Contains("Opera")) return Browser.Opera;
				if (this.userAgent.Contains("Chrome")) return Browser.Chrome;
				if (this.userAgent.Contains("Firefox")) return Browser.Firefox;
				if (this.userAgent.Contains("MSIE") || this.userAgent.Contains("Trident")) return Browser.IE;
				if (this.userAgent.Contains("Safari")) return Browser.Safari;
				return Browser.Unknown;
			}
		}

		public float BrowserVersion
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.userAgent)) return 0;

				Match versionMatch = null;
				if (this.userAgent.Contains("OPR"))
				{
					versionMatch = Regex.Match(this.userAgent, @"OPR/(\d+\.\d+)");
				}
				else if (this.userAgent.Contains("Chrome"))
				{
					versionMatch = Regex.Match(this.userAgent, @"Chrome/(\d+\.\d+)");
				}
				else if (this.userAgent.Contains("Firefox"))
				{
					versionMatch = Regex.Match(this.userAgent, @"Firefox/(\d+\.\d+)");
				}
				else if (this.userAgent.Contains("Opera"))
				{
					versionMatch = Regex.Match(this.userAgent, @"Opera/9\.80.*Version/(\d+\.\d+)");
					if (!versionMatch.Success)
					{
						versionMatch = Regex.Match(this.userAgent, @"Opera/(\d+\.\d+)");
					}
				}
				else if (this.userAgent.Contains("MSIE"))
				{
					versionMatch = Regex.Match(this.userAgent, @"MSIE (\d+\.\d+)");
				}
				else if (this.userAgent.Contains("Trident"))
				{
					versionMatch = Regex.Match(this.userAgent, @"rv:(\d+(\.\d+)?)");
				}
				else if (this.userAgent.Contains("Safari"))
				{
					versionMatch = Regex.Match(this.userAgent, @"Version/(\d+\.\d+)");
				}

				// get browser version details
				if (versionMatch != null && versionMatch.Success)
				{
					float versionNo;
					if (float.TryParse(versionMatch.Groups[1].Value, out versionNo))
					{
						return versionNo;
					}
				}

				return 0;
			}
		}

		public int BrowserMajorVersion
		{
			get
			{
				return (int)Math.Floor(this.BrowserVersion);
			}
		}

		#endregion

		#region RenderingEngine

		public RenderingEngine RenderingEngine
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.userAgent)) return RenderingEngine.Unknown;

				Match match = Regex.Match(this.userAgent, @"(WebKit|Gecko|Trident|Presto)");

				switch (match.Groups[1].Value)
				{
					case "WebKit":
						return RenderingEngine.WebKit;
					case "Gecko":
						return RenderingEngine.Gecko;
					case "Trident":
						return RenderingEngine.Trident;
					case "Presto":
						return RenderingEngine.Presto;
				}

				return RenderingEngine.Unknown;
			}
		}

		public string RenderingEngineVersion
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.userAgent)) return string.Empty;

				Match match = Regex.Match(this.userAgent, @"(WebKit|Gecko|Trident|Presto)");
				match = Regex.Match(this.userAgent, @"(WebKit|Trident|Presto)/(?<version>\d+(\.\d+)*)");

				if (!match.Success)
				{
					match = Regex.Match(this.userAgent, @"rv:(?<version>\d+(\.\d+)*)\) Gecko/");
				}

				if (match != null && match.Success)
				{
					return match.Groups["version"].Value;
				}

				return string.Empty;
			}
		}

		#endregion

		#region OS

		public Platform Platform
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.userAgent)) return Platform.Unknown;

				if (this.userAgent.Contains("Windows")) return Platform.Windows;
				if (this.userAgent.Contains("Mac OS X")) return Platform.Mac;
				if (this.userAgent.Contains("Android")) return Platform.Android;
				if (this.userAgent.Contains("Linux")) return Platform.Linux;

				return Platform.Unknown;
			}
		}

		public string OS
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.userAgent)) return string.Empty;

				if (this.userAgent.Contains("Windows"))
				{

					// Windows Desktop
					if (this.userAgent.Contains("Windows NT 6.3")) return "Windows 8.1";
					if (this.userAgent.Contains("Windows NT 6.2")) return "Windows 8";
					if (this.userAgent.Contains("Windows NT 6.1")) return "Windows 7";
					if (this.userAgent.Contains("Windows NT 6.0")) return "Windows Vista";
					if (this.userAgent.Contains("Windows NT 5.2")) return "Windows Server 2003";
					if (this.userAgent.Contains("Windows NT 5.1")) return "Windows XP";
					if (this.userAgent.Contains("Windows NT 5.01")) return "Windows 2000 (SP1)";
					if (this.userAgent.Contains("Windows NT 5.0")) return "Windows 2000";
					if (this.userAgent.Contains("Windows NT 4.0")) return "Windows NT 4.0";
					if (this.userAgent.Contains("Windows 98; Win 9x 4.90")) return "Windows Me";
					if (this.userAgent.Contains("Windows 98")) return "Windows 98";
					if (this.userAgent.Contains("Windows 95")) return "Windows 95";
					if (this.userAgent.Contains("Windows CE")) return "Windows CE";

					// Windows Phone
					Match match = Regex.Match(this.userAgent, @"(Windows Phone( OS)? \d+\.\d+)");
					if (match.Success) return match.Groups[1].Value;
				}
				else if (this.userAgent.Contains("Mac OS X"))
				{
					//Version 10.0: "Cheetah"
					//Version 10.1: "Puma"
					//Version 10.2: "Jaguar"
					//Version 10.3: "Panther"
					//Version 10.4: "Tiger"
					//Version 10.5: "Leopard"
					//Version 10.6: "Snow Leopard"
					//Version 10.7: "Lion"
					//Version 10.8: "Mountain Lion"

					Match match = Regex.Match(this.userAgent, @"(Mac OS X \d+_\d+)");

					if (match.Success)
					{
						return match.Groups[1].Value.Replace("_", ".");
					}
					else
					{
						match = Regex.Match(this.userAgent, @"OS (\d+_\d+)(_\d+)? like Mac OS X");
						return "iOS " + match.Groups[1].Value.Replace("_", ".");
					}
				}
				else if (this.userAgent.Contains("Android"))
				{
					Match match = Regex.Match(this.userAgent, @"Android \d+\.\d+");
					if (match.Success)
					{
						return match.Value;
					}
					else
					{
						return "Android";
					}
				}

				return string.Empty;
			}
		}

		#endregion

		#region Device

		public string Device
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.userAgent)) return "Unknown";

				var match = Regex.Match(this.userAgent, "(iPad|iPhone|Nexus|Kindle Fire|Hudl|Lumia|Xbox)");

				if (match.Success)
				{
					return match.Groups[1].Value;
				}
				else
				{
					return "Unknown";
				}
			}
		}

		public DeviceType DeviceType
		{
			get
			{
				if (string.IsNullOrWhiteSpace(this.userAgent)) return DeviceType.Unknown;

				if (this.userAgent.Contains("Phone")) return DeviceType.Phone;
				if (this.userAgent.Contains("Tablet")) return DeviceType.Tablet;

				// Android
				if (this.userAgent.Contains("Android"))
				{
					if (this.userAgent.Contains("Mobile"))
					{
						return DeviceType.Phone;
					}
					else
					{
						return DeviceType.Tablet;
					}
				}

				// Macs
				else if (this.userAgent.Contains("Mac OS X"))
				{
					if (this.userAgent.Contains("iPad"))
					{
						return DeviceType.Tablet;
					}
					else if (this.userAgent.Contains("iPhone"))
					{
						return DeviceType.Phone;
					}
				}

				// Windows
				else if (this.userAgent.Contains("Windows"))
				{
					if (this.userAgent.Contains("Tablet PC"))
					{
						return DeviceType.Tablet;
					}
				}

				return DeviceType.Desktop;
			}
		}

		#endregion
	}
}
