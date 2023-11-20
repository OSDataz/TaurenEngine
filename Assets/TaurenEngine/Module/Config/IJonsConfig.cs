/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2023/11/20 21:18:47
 *└────────────────────────┘*/

namespace TaurenEngine.ModConfig
{
	public interface IJonsConfig : IConfig
	{
		void SetData(object jsonData);
	}
}