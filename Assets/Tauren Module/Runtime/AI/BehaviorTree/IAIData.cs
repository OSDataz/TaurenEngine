/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 17:41:26
 *└────────────────────────┘*/

namespace Tauren.Module.Runtime
{
	public interface IAIData
	{
		void SetData<T>(string key, T value);

		T GetData<T>(string key);
	}
}