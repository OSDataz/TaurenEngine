/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/20 21:18:47
 *└────────────────────────┘*/

namespace Tauren.Module.Runtime
{
	public interface IJonsConfig : IConfig
	{
		void SetData(object jsonData);
	}
}