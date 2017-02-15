using System;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

class Script
{
	[STAThread]
	static public void Main(string[] args)
	{
		List<string> largs = args.ToList();
		List<string> dlargs = largs.ConvertAll(a => a.ToLower());
		
		if (dlargs.Contains("-h") || dlargs.Contains("--help"))
		{
			PrintHelp();
		}
		else
		{
			string sXml = "";
			string lastArg = largs.Count > 0 ? largs.Last() : "";
			string beforeLastArg = largs.Count > 1 ? largs[largs.Count - 2] : "";
			
			try
			{
				if (!beforeLastArg.Equals("-s") && (lastArg.Trim().StartsWith("<")))
				{
					sXml = lastArg;
				}
				else if (!beforeLastArg.Equals("-s") && File.Exists(lastArg))
				{
					sXml = File.ReadAllText(largs.Last());
				}
				else
				{
					string line;

					while ((line = Console.ReadLine()) != null)
					{
						sXml += line + "\r\n";
					}
				}
			}
			catch { }
			
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(sXml);
			List<XmlNode> xmlNodes = new List<XmlNode>();

			if (dlargs.Contains("-s"))
			{
				int index = dlargs.IndexOf("-s");
				string path = largs[index + 1];

				xmlNodes = doc.SelectNodes(path).Cast<XmlNode>().ToList();
			}
			else
			{
				xmlNodes.Add(doc);
			}
			
			if(dlargs.Contains("-c"))
			{
				Console.Write(xmlNodes.Count);
			}
			else
			{
				if(xmlNodes.Count == 0)
				{
					Console.Write("null");
				}
				else
				{
					if(dlargs.Contains("-f"))
					{
						xmlNodes = xmlNodes.GetRange(0, 1);
					}
					else if(dlargs.Contains("-l"))
					{
						xmlNodes = xmlNodes.GetRange(xmlNodes.Count - 1, 1);
					}
					
					xmlNodes.ForEach(delegate(XmlNode xmlNode)
					{
						string xmlToReturn = dlargs.Contains("-o") ? xmlNode.OuterXml : xmlNode.InnerXml;
						
						if (dlargs.Contains("-b") || dlargs.Contains("-i"))
						{
							Console.WriteLine(Beautify(xmlToReturn));
						}
						else
						{
							Console.WriteLine(xmlToReturn);
						}
					});
				}
			}	
		}
	}
	
	public static void PrintHelp()
	{
			Console.WriteLine("xml 1.0");
			Console.WriteLine("Args :");
			Console.WriteLine("\t-b or -i     : Beautify or indent the input xml.");
			Console.WriteLine("\t               If not defined xml will be on one line.");
			Console.WriteLine("\t-h or --help : Show this help for the command");
			Console.WriteLine("\t-s [xpath]   : Allow to return a sub part of the xml");
			Console.WriteLine("\t               based on jpath (like xpath for xml)");
			Console.WriteLine("\t-o           : If defined return outerXml");
			Console.WriteLine("\t               (By default if not defined innerXml)");
			Console.WriteLine("\t-c           : If defined count the number of elements in root");
			Console.WriteLine("\t               (after sub part process if one)");
			Console.WriteLine("\t-f -l        : If defined show only respectively the first or the last");
			Console.WriteLine("\t               element found by xpath");
			Console.WriteLine("");
			Console.WriteLine("Xml can be read as xml or filename in the command last arg or from standard input (by pipes).");
	}
	
	public static string Beautify(string xml)
	{
		
		try
		{
			StringBuilder sb = new StringBuilder();
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);
			
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "  ",
				NewLineChars = "\r\n",
				NewLineHandling = NewLineHandling.Replace,
				OmitXmlDeclaration = true
			};
			
			using (XmlWriter writer = XmlWriter.Create(sb, settings)) {
				doc.Save(writer);
			}
			return sb.ToString();
		}
		catch
		{
			return xml;
		}
		
	}
}

