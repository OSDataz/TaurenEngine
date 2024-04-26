/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 17:05:59
 *└────────────────────────┘*/

/*
Android闪退：

1.Unity5.6以下，LocationServer存在一定的缺陷，实测android5.0以上机型，部分闪退。官方于5.6.1修复该问题。建议使用Unity5.6.1以上版本。
2.部分android机即使用5.6.x以上打测试包APK，依然闪退。
    a>当手机弹出警告框“是否允许使用定位服务”时，一定选择允许。
    b>检查设置中隐私中，是否对应用的定位服务开启。
    b>有些手机允许了定位服务，隐私中也确实开启了定位服务。但是依然闪退，原因：以小米手机为例，有一个模式叫：对未知应用的信任，如果选择否，那么手机是只信任正式渠道的签名APK，我们的测试APK包都属于“未知应用”，因此即使在隐私的定位中开启了对该应用的定位服务，但是由于手机就不信任“未知应用”，所以依然会闪退。

IOS闪退：

1.添加Info.plist中的权限就OK了；
    a>NSLocationAlwaysUsageDescription
    b>NSLocationWhenInUseUsageDescription
*/

using Tauren.Core.Runtime;
using UnityEngine;
using UnityEngine.Android;

namespace Tauren.Module.Runtime
{
	/// <summary>
	/// GPS管理器
	/// </summary>
	public class LocationDevice : DeviceControl<LocationDevice>
	{
		/// <summary>
		/// 可接受的位置测量的最低精度，以米为单位。
		/// 这里的精度是指设备位置不确定性的半径
		/// 定义一个可能在其中找到的圆圈。
		/// </summary>
		public float desiredAccuracyInMeters = 10f;
		/// <summary>
		/// 连续两次位置更新之间的最小距离以米为单位。
		/// </summary>
		public float updateDistanceInMeters = 10f;

		public LocationDevice()
		{
			LocationService = Input.location;
		}

		protected override void UpdateEnabled()
		{
			if (Enabled)
			{
				Log.Info($"启动定位：测量精度{desiredAccuracyInMeters}米 更新距离{updateDistanceInMeters}米");
				LocationService.Start(desiredAccuracyInMeters, updateDistanceInMeters);
			}
			else
			{
				Log.Info("关闭定位");
				LocationService.Stop();
			}
		}

		/// <summary>
		/// 请求定位许可
		/// </summary>
		public void RequestPermission()
		{
			// 用于访问GPS定位
			Permission.RequestUserPermission("android.permission.ACCESS_FINE_LOCATION");
			// 用于提高GPS定位速度
			Permission.RequestUserPermission("android.permission.ACCESS_LOCATION_EXTRA_COMMANDS");
			// 允许启用禁止位置更新提示从无线模块
			Permission.RequestUserPermission("android.permission.CONTROL_LOCATION_UPDATES");
		}

		public LocationService LocationService { get; private set; }

		public bool IsAvailable => SystemInfo.supportsLocationService;
	}
}