/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/3/22 18:11:19
 *└────────────────────────┘*/

namespace Tauren.Core.Runtime
{
	public interface IPool
	{
		IRecycle GetItem();

		bool Recycle(IRecycle item);

		void Clear();

		void Destroy();

		int Maximum { get; set; }
	}
}