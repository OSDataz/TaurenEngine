/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/4 18:56:09
 *└────────────────────────┘*/

namespace Tools.JobAnalysis
{
	public class ParseBase
	{
		protected bool GetStr(ref string[] list, ref int i, out string str)
		{
			if (i >= list.Length)
			{
				str = string.Empty;
				return false;
			}

			str = list[i];

			while (string.IsNullOrEmpty(str))
			{
				if (++i >= list.Length)
					return false;

				str = list[i];
			}

			return true;
		}

		protected bool GetNextStr(ref string[] list, ref int i, out string str)
		{
			i += 1;
			return GetStr(ref list, ref i, out str);
		}

		protected bool ParseCompanySize(string str,
			out float min, out float max)
		{
			var maxStr = "人以上";
			if (str.EndsWith(maxStr))
			{
				min = -1;
				max = float.Parse(str.Replace(maxStr, ""));
				return true;
			}

			str = str.Substring(0, str.Length - 1);

			var index = str.IndexOf('-');
			if (index == -1)
			{
				min = 0;
				max = 0;
				return false;
			}

			min = float.Parse(str.Substring(0, index));
			max = float.Parse(str.Substring(index + 1));
			return true;
		}
	}
}