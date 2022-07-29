/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.7.0
 *│　Time    ：2022/7/26 23:20:15
 *└────────────────────────┘*/

namespace TaurenEngine
{
	public partial class Timer : PoolObject<Timer>, IRefObject
	{
		public int RefCount { get; set; }



		public override void Clear()
		{
			
		}
	}
}