/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.1
 *│　Time    ：2022/6/21 15:04:12
 *└────────────────────────┘*/

using System;
using System.Data;
using System.IO;

namespace TaurenEditor.Runtime
{
	public class ExcelReader : IDisposable
	{
		public FileStream Stream { get; private set; }
		//public IExcelDataReader DataReader { get; private set; }
		/// <summary>
		/// 表格数据集合
		/// </summary>
		public DataSet DataSet { get; private set; }

		public void Load(string path)
		{
			Dispose();

			Stream = File.Open(path, FileMode.Open, FileAccess.Read);
			//DataReader = ExcelReaderFactory.CreateOpenXmlReader(Stream);
			//DataSet = DataReader.AsDataSet();
		}

		/// <summary>
		/// 解除IO占用
		/// </summary>
		public void Dispose()
		{
			if (Stream != null)
			{
				Stream.Close();
				Stream.Dispose();
				Stream = null;
			}

			if (DataSet != null)
			{
				DataSet.Dispose();
				DataSet = null;
			}

			//if (DataReader != null)
			//{
			//	DataReader.Close();
			//	DataReader.Dispose();
			//	DataReader = null;
			//}
		}
	}
}