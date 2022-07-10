/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v4.0
 *│　Time    ：2022/1/20 20:10:28
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 默认资源类型
	/// </summary>
	internal class ObjectRes : LoadRes<object>, IRecycle
	{
		public override void Release()
		{

		}

		public void Clear()
		{

		}

		public void Destroy()
		{

		}
	}
}