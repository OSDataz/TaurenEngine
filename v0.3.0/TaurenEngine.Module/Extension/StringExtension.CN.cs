/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/21 22:11:43
 *└────────────────────────┘*/

namespace TaurenEngine.Core
{
    public static partial class StringExpand
    {
        /// <summary>
        /// 中文数字转化为阿拉伯数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int CnToInt(this string value)
        {
            int toInt = 0;
            int tInt = 0;
            int temp;

            foreach (var c in value)
            {
                if (c == '一' || c == '壹') temp = 1;
                else if (c == '二' || c == '贰') temp = 2;
                else if (c == '三' || c == '叁') temp = 3;
                else if (c == '四' || c == '肆') temp = 4;
                else if (c == '五' || c == '伍') temp = 5;
                else if (c == '六' || c == '陆') temp = 6;
                else if (c == '七' || c == '柒') temp = 7;
                else if (c == '八' || c == '捌') temp = 8;
                else if (c == '九' || c == '玖') temp = 9;
                else if (c == '零' || c == '〇') temp = 0;
                else
                {
                    if (c == '十' || c == '拾') temp = 10;
                    else if (c == '百' || c == '佰') temp = 100;
                    else if (c == '千' || c == '仟') temp = 1000;
                    else if (c == '万') temp = 10000;
                    else if (c == '亿') temp = 100000000;
                    else
                    {
                        TDebug.Error($"StringExtension.CnToInt字符串解析失败：{value} {c}");
                        return 0;
                    }

                    if (tInt == 0)
                        tInt = temp;
                    else
                        tInt *= temp;
                    continue;
                }

                toInt += tInt;
                tInt = temp;
            }

            toInt += tInt;
            return toInt;
        }
    }
}