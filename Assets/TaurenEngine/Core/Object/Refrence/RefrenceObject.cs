/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.9.0
 *│　Time    ：2022/11/23 21:28:36
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
	/// <summary>
	/// 引用计数型对象基类
	/// </summary>
	public class RefrenceObject : DObject, IRefrenceObject
	{
		public int RefCount { get; set; }
	}
}