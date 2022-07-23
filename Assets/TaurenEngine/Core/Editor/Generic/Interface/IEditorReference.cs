/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/10/17 17:04:31
 *└────────────────────────┘*/

namespace TaurenEditor
{
	/// <summary>
	/// 引用类型的属性
	/// </summary>
	public interface IEditorReference : IEditorProperty
	{
		void Remove(IEditorProperty item);
	}
}