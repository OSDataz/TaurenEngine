/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/28 17:41:26
 *└────────────────────────┘*/

namespace TaurenEngine.AI
{
	public interface IAIData
	{
		void SetData<T>(string key, T value);

		T GetData<T>(string key);
	}
}