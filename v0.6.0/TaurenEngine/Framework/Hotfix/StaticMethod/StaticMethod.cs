/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/6 12:10:51
 *└────────────────────────┘*/

namespace TaurenEngine.Framework
{
	public abstract class StaticMethod
	{
		protected object[] paramters;

		public virtual void Run(params object[] paramList)
		{
			// 设置参数
			int i;
			for (i = 0; i < paramters.Length && i < paramList.Length; ++i)
			{
				paramters[i] = paramList[i];
			}

			for (; i < paramters.Length; ++i)
			{
				paramters[i] = default;
			}
		}
	}
}