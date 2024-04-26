/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.1
 *│　Time    ：2022/1/10 20:47:45
 *└────────────────────────┘*/

using System.Diagnostics;
using System.Text;
using Tauren.Core.Runtime;

namespace Tauren.Framework.Editor
{
	/// <summary>
	/// 运行Cmd Bat批处理命令
	/// </summary>
	public partial class BatScript
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

			var path = $"{EditorHelper.DebugFullPath}/Temp/BatOrder.bat";

			// 保存代码文件
			FileUtils.SaveText(path, text);

			// 运行代码文件
			using (var process = new Process())
			{
				process.StartInfo.FileName = path;
				process.StartInfo.Verb = "runas";
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
	}
}