/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 腾讯地图扩展使用
    ///
    /// 坐标拾取器：https://lbs.qq.com/tool/getpoint/index.html
    /// </summary>
    public class TencentMapEx : LBSEngineBase
    {
        public TencentMapMap tencentMapMap { get; private set; }
        public TencentMapNavi tencentMapNavi { get; private set; }
        public TencentMapRGC tencentMapRgc { get; private set; }

        internal override void Awake()
        {
            new TencentMapComponent().InitComponent(this);

            tencentMapMap = new TencentMapMap();
            tencentMapNavi = new TencentMapNavi();
            tencentMapRgc = new TencentMapRGC();
        }

        internal override void OnEnable()
        {
        }

        internal override void Start()
        {

        }

        internal override void Update()
        {
	        tencentMapMap.Update();
        }

        internal override void OnDisable()
        {
        }

        internal override void OnDestroy()
        {
            if (IsDestroyed)
                return;



            base.OnDestroy();
        }

        public override LBSEngineType EngineType => LBSEngineType.TencentMap;
    }

    internal class TencentMapComponent : LBSComponent
    {
        public void InitComponent(TencentMapEx tencentMap)
        {
            TDebug.Log("Init TencentMap Component");

            var setting = LBSEngineSetting.Instance.tencentMapSetting;
            if (setting == null)
            {
                TDebug.Error("请先使用LBSEngineSetting设置数据");
                return;
            }

            if (setting.mapController == null || setting.cameraPivot == null)
            {
                TDebug.Error("LBSEngineSetting腾讯地图数据未设置完整");
                return;
            }
        }

        public void Destroy(TencentMapEx tencentMap)
        {


        }
    }
}