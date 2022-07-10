/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:15:32
 *└────────────────────────┘*/

namespace TaurenEngine.AI
{
	public interface INeuralInfo
	{
		/// <summary>
		/// 刺激反馈类型（唯一识别码）
		/// </summary>
		string Type { get; }
	}
}