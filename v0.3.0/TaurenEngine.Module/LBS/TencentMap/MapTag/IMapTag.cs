/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using TaurenEngine.LocationEx;
using UnityEngine;

namespace TaurenEngine.LBS
{
	public interface IMapTag
	{
		GameObject gameObject { get; }
		Location Location { get; }
	}
}