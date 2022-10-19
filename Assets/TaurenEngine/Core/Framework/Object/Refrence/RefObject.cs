/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/19 23:59:55
 *└────────────────────────┘*/

namespace TaurenEngine
{
	/// <summary>
	/// 引用计数型对象基类
	/// </summary>
	public class RefObject : DObject, IRefObject
	{
		public int RefCount { get; set; }
	}
}