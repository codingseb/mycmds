//css_reference Newtonsoft.Json.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Script
{
	[STAThread]
	static public void Main(string[] args)
	{
		List<string> largs = args.ToList();
		List<string> dlargs = largs.ConvertAll(a => a.ToLower());

		if (dlargs.Contains("-h") || dlargs.Contains("--help"))
		{
			Console.WriteLine("json 1.0");
			Console.WriteLine("Args :");
			Console.WriteLine("\t-b or -i     : Beautify or indent the input json.");
			Console.WriteLine("\t               If not defined json will be on one line.");
			Console.WriteLine("\t-h or --help : Show this help for the command");
			Console.WriteLine("\t-s [jpath]   : Allow to return a sub part of the json");
			Console.WriteLine("\t               based on jpath (like xpath for json)");
			Console.WriteLine("Json can be read as json or filename in the command last arg or from standard input (by pipes).");
		}
		else
		{
			string sJson = "";
			string lastArg = largs.Count > 0 ? largs.Last() : "";
			string beforeLastArg = largs.Count > 1 ? largs[largs.Count - 2] : "";

			try
			{
				if (!beforeLastArg.Equals("-s") && (lastArg.Trim().StartsWith("{") || lastArg.Trim().StartsWith("[")))
				{
					sJson = lastArg;
				}
				else if (!beforeLastArg.Equals("-s") && File.Exists(lastArg))
				{
					sJson = File.ReadAllText(largs.Last());
				}
				else
				{
					string line;

					while ((line = Console.ReadLine()) != null)
					{
						sJson += line + "\r\n";
					}
				}
			}
			catch { }

			JToken jToken= JsonConvert.DeserializeObject(sJson) as JToken;
			IEnumerable<JToken> jTokens = null;

			if (dlargs.Contains("-s"))
			{
				int index = dlargs.IndexOf("-s");
				string path = largs[index + 1];

				try
				{
					jToken = jToken.SelectToken(path);
				}
				catch
				{
					jTokens = jToken.SelectTokens(path);
				}
			}
			else
			{
				jTokens = jToken.SelectTokens("$");
			}

			if (dlargs.Contains("-b") || dlargs.Contains("-i"))
			{
				Console.Write(JsonConvert.SerializeObject(jTokens == null ? jToken : jTokens, Formatting.Indented).Trim('"'));
			}
			else
			{
				Console.Write(JsonConvert.SerializeObject(jTokens == null ? jToken : jTokens).Trim('"'));
			}
		}         
	}
}

