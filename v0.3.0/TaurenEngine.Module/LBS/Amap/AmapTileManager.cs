/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using TaurenEngine.Core;
using TaurenEngine.Mathematics;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// https://www.jianshu.com/p/e34f85029fd7
    /// </summary>
    public class AmapTileManager : Singleton<AmapTileManager>
    {
        public AmapTileHeader header = AmapTileHeader.webst;
        public AmapTileLang lang = AmapTileLang.zh_cn;
        public AmapTileStyle style = AmapTileStyle.Sketch;
        public AmapTileScl scl = AmapTileScl.Scl256;
        public AmapTileLType lType;

        public int zoom = 17;

        public string GetTileUrl(int2 tile)
        {
            return
                $"https://{header}01.is.autonavi.com/appmaptile?lang={lang}&x={tile.x}&y={tile.y}&z={zoom}&scl={(int)scl}&style={(int)style}";
        }
    }
}