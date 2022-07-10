/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/


namespace TaurenEngine.LBS
{
    public class TmNaviResponse
    {
        /// <summary>
        /// 状态码，正常为0
        /// </summary>
        public int status;

        /// <summary>
        /// 状态说明
        /// </summary>
        public string message;

        //public virtual void ParseJData(JObject jData)
        //{
        //    status = jData.Value<int>("status");
        //    message = jData.Value<string>("message");
        //}

        public virtual void Parse()
        {
        }
    }

    public class TmLatLng
    {
        /// <summary>
        /// 纬度
        /// </summary>
        public double lat;

        /// <summary>
        /// lng
        /// </summary>
        public double lng;

        //public void ParseJData(JToken jData)
        //{
        //    if (jData == null)
        //        return;

        //    lat = jData.Value<double>("lat");
        //    lng = jData.Value<double>("lng");
        //}
    }
}