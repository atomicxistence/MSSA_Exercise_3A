using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using TaskrLibrary.Models;

namespace TaskrLibrary
{
	public class XMLFileManager : IFileManager
	{
		private string savePath = AppDomain.CurrentDomain.BaseDirectory + "Taskr.xml";

		public List<Page> Load()
		{
			VerifyXMLFile();

			XmlSerializer serializer = new XmlSerializer(typeof(List<Page>));
			using (StreamReader reader = new StreamReader(savePath))
			{
				return (List<Page>)serializer.Deserialize(reader);
			}
		}

		public void Save(List<Page> pages)
		{
			var serializer = new XmlSerializer(typeof(List<Page>));
			using (StreamWriter writer = new StreamWriter(savePath, false))
			{
				serializer.Serialize(writer.BaseStream, pages);
			}
		}

		private void VerifyXMLFile()
		{
			if (!File.Exists(savePath))
			{
				throw new FileNotFoundException("The Task List XML save file can not be found.");
			}
		}
	}
}
