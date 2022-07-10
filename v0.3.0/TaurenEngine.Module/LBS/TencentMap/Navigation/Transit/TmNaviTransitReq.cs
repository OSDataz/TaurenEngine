/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System.Collections.Generic;

namespace TaurenEngine.LBS
{
    public class TmNaviTransitReq : TmNaviRequest
    {
        /// <summary>
        /// 出发时间，用于过滤掉非运营时段的线路，
        /// 格式为Unix时间戳，默认使用当前时间
        /// </summary>
        public string departure_time;

        /// <summary>
        /// 路线计算偏好
        /// </summary>
        public TmNaviTransitReqPolicy policy;

        public override string ToUrl()
        {
            var url = base.ToUrl();

            if (!string.IsNullOrEmpty(departure_time))
                url += $"&departure_time={departure_time}";

            if (policy == null)
                url += $"&policy={policy.ToUrl()}";

            return url;
        }
    }

    public class TmNaviTransitReqPolicy
    {
        public TmNaviTransitReqFromPolicy policy;

        public List<TmNaviTransitReqPolicyMore> policyMore;

        public string ToUrl()
        {
            string str = policy.ToString();

            if (policyMore?.Count > 0)
            {
                foreach (var item in policyMore)
                {
                    str += $",{item}";
                }
            }

            return str;
        }
    }
}