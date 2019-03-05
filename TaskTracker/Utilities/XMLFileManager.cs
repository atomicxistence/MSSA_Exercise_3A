using System;
using System.IO;
using System.Xml.Serialization;

namespace TaskTracker
{
	public class XMLFileManager : IFileManager
	{
		private string savePath = AppDomain.CurrentDomain.BaseDirectory + "/Taskr.xml";

		public TaskList Load()
		{
			VerifyXMLFile();

			XmlSerializer serializer = new XmlSerializer(typeof(TaskList));
			using (StreamReader reader = new StreamReader(savePath))
			{
				return (TaskList)serializer.Deserialize(reader);
			}
		}

		public void Save(TaskList taskList)
		{
			var serializer = new XmlSerializer(typeof(TaskList));
			using (StreamWriter writer = new StreamWriter(savePath, false))
			{
				serializer.Serialize(writer.BaseStream, taskList);
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
