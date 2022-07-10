/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

namespace TaurenEngine.LBS
{
    public enum AmapTileHeader
    {
        webst,
        wprd,
        webrd
    }

    public enum AmapTileLang
    {
        /// <summary>
        /// 中文
        /// </summary>
        zh_cn,
        /// <summary>
        /// 英文
        /// </summary>
        en
    }

    public enum AmapTileStyle
    {
        /// <summary>
        /// 卫星（st）
        /// </summary>
        Satellite = 6,
        /// <summary>
        /// 简图（st rd）
        /// </summary>
        Sketch = 7,
        /// <summary>
        /// 详图（不透明rd，透明图st）
        /// </summary>
        Detailed = 8
    }

    public enum AmapTileScl
    {
        /// <summary>
        /// 瓦片尺寸 256
        /// </summary>
        Scl256 = 1,
        /// <summary>
        /// 瓦片尺寸 512
        /// </summary>
        Scl512 = 2
    }

    public enum AmapTileLType
    {
        /// <summary>
        /// 道路纯图
        /// </summary>
        LType2 = 2,
        /// <summary>
        /// 纯地标
        /// </summary>
        LType4 = 4,
        /// <summary>
        /// 纯道路
        /// </summary>
        LType11 = 11
    }
}