/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.10.0
 *│　Time    ：2023/4/23 20:14:56
 *└────────────────────────┘*/

using UnityEngine;

namespace Tauren.Core.Runtime
{
	public class LogItem
	{
		public int index;
		public string time;
		public LogType type;
		public string message;
		public int count;
	}
}