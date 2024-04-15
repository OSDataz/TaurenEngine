/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.12.1
 *│　Time    ：2024/4/7 11:29:14
 *└────────────────────────┘*/

using Tauren.Core.Runtime;
using Tauren.Engine.Runtime;

namespace GameConnectFour
{
	public class Main : SingletonComponent<Main>
	{
		/// <summary> 流程管理器 </summary>
		protected ProcedureManager ProcedureMgr { get; private set; }

		protected void Awake()
		{
			ProcedureMgr = new ProcedureManager();
		}

		protected void Start()
		{
			Launcher.Instance.InitService(gameObject);

			ProcedureMgr.Change<MenuProcedure>();// 切换到菜单UI
		}
	}
}