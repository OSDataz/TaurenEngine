/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2022/1/10 20:47:45
 *└────────────────────────┘*/

using System.Diagnostics;
using System.IO;
using System.Text;
using TaurenEngine.Unity;
using UnityEngine;

namespace TaurenEngine.Editor
{
	/// <summary>
	/// 运行Cmd Bat批处理命令
	/// </summary>
	public class BatScript
	{
		private StringBuilder _builder = new StringBuilder();

		public void AddCmd(string order)
		{
			_builder.AppendLine(order);
		}

		public void Run()
		{
			var text = _builder.ToString();
			if (string.IsNullOrEmpty(text))
				return;

			var path = $"{Application.dataPath}/SettingConfig/Temp/BatOrder.bat";

			// 保存代码文件
			FileEx.SaveText(path, text);

			// 运行代码文件
			using (var process = new Process())
			{
				process.StartInfo.FileName = path;
				process.StartInfo.UseShellExecute = true; // 不使用操作系统shell启动 true WindowStyle; false CreateNoWindow
				process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;// 隐藏窗口
				process.StartInfo.CreateNoWindow = true;// 不显示程序窗口，静默执行
				process.StartInfo.RedirectStandardError = false;// 从Unity控制台定向标准错误输出
				process.StartInfo.RedirectStandardInput = false;// 接受来自Unity的输入信息
				process.StartInfo.RedirectStandardOutput = false;// 从Unity控制台获取输出信息
				process.Start();
				process.Close();
			}
		}

		public void Clear()
		{
			_builder.Clear();
		}

		#region mklink
		public void MKLink(string fromPath, string toPath)
		{
			fromPath = PathEx.FormatPathEnd(fromPath, false);
			toPath = PathEx.FormatPathEnd(toPath, false); ;

			if (!FileEx.CheckPath(fromPath, toPath, out var fromIsFile, out var toIsFile, out var toIsExist))
				return;

			if (fromIsFile)
			{
				if (!toIsExist)
				{
					if (toIsFile)
						FileEx.CreateSaveFilePath(toPath);
					else
						FileEx.CreateSaveDirectoryPath(toPath);
				}

				if (!toIsFile)
					toPath = $"{PathEx.FormatPathEnd(toPath, true)}{Path.GetFileName(fromPath)}";

				// 文件链接文件
				AddCmd($"mklink /h \"{toPath}\" \"{fromPath}\"");
			}
			else
			{
				if (toIsExist)// 目标文件已存在，暂不处理
					return;

				var path = Path.GetDirectoryName(toPath);
				FileEx.CreateSaveDirectoryPath(path);

				// 文件夹链接文件夹
				AddCmd($"mklink /j \"{toPath}\" \"{fromPath}\"");
			}
		}
		#endregion
	}
}