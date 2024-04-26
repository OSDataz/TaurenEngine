/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/8 14:48:49
 *└────────────────────────┘*/

namespace Tauren.Core.Runtime
{
	public interface IRefrenceContainer
	{
		void Add(IRefrenceObject refObject);

		bool Remove(IRefrenceObject refObject);
	}
}