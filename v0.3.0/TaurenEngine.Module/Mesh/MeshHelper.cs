/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/10/8 20:06:48
 *└────────────────────────┘*/

using System.Collections.Generic;
using UnityEngine;

namespace TaurenEngine.MeshEx
{
    public static class MeshHelper
    {
		/// <summary>
		/// 创建平面网格
		/// </summary>
		/// <returns></returns>
		public static Mesh CreateMeshQuad()
		{
			var mesh = new Mesh();
			// vertices
			mesh.vertices = new Vector3[]
			{
				new Vector3(-0.5f, -0.5f, 0.0f),
				new Vector3(-0.5f, 0.5f, 0.0f),
				new Vector3(0.5f, 0.5f, 0.0f),
				new Vector3(0.5f, -0.5f, 0.0f),
			};
			// uv
			mesh.uv = new Vector2[]
			{
				Vector2.zero,
				Vector2.up,
				Vector2.one,
				Vector2.right,
			};
			// triangles
			mesh.triangles = new[] { 0, 1, 2, 0, 2, 3 };

			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			return mesh;
		}

		/// <summary>
		/// 创建矩形网格
		/// </summary>
		/// <param name="weight"></param>
		/// <param name="height"></param>
		/// <returns></returns>
		public static Mesh CreateMeshRectangle(float weight, float height)
		{
			weight *= 0.5f;
			height *= 0.5f;

			var mesh = new Mesh();
			// vertices
			mesh.vertices = new Vector3[]
			{
				new Vector3(-weight, 0.0f, -height),
				new Vector3(-weight, 0.0f,height),
				new Vector3(weight, 0.0f,height),
				new Vector3(weight, 0.0f,-height),
			};
			// uv
			mesh.uv = new Vector2[]
			{
				Vector2.zero,
				Vector2.up,
				Vector2.one,
				Vector2.right,
			};
			// triangles
			mesh.triangles = new[] { 0, 1, 2, 0, 2, 3 };

			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			return mesh;
		}

		/// <summary>
		/// 创建圆形网格
		/// </summary>
		/// <param name="radius"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public static Mesh CreateMeshCircular(float radius, int segments)
		{
			// vertices
			int vCount = segments + 1;
			Vector3[] vertices = new Vector3[vCount];
			vertices[0] = Vector3.zero;
			float angleDelta = Mathf.Deg2Rad * 360.0f / segments;
			float angleCur = 0.0f;
			for (int i = 1; i < vCount; ++i)
			{
				vertices[i] = new Vector3(Mathf.Cos(angleCur) * radius, 0.0f, Mathf.Sin(angleCur) * radius);
				angleCur += angleDelta;
			}

			// triangles
			int tCount = segments * 3;
			int[] triangles = new int[tCount];
			tCount -= 1;
			for (int i = 0, vi = 1; i <= tCount; i += 3, vi++)
			{
				triangles[i] = 0;
				triangles[i + 1] = vi + 1;
				triangles[i + 2] = vi;
			}
			triangles[tCount - 2] = 0;
			triangles[tCount - 1] = 1;
			triangles[tCount] = segments;

			// uv
			Vector2[] uvs = new Vector2[vCount];
			for (int i = 0; i < vCount; ++i)
			{
				uvs[i] = new Vector2(vertices[i].x / radius * 0.5f + 0.5f, vertices[i].z / radius * 0.5f + 0.5f);
			}

			var mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.uv = uvs;

			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			return mesh;
		}

		/// <summary>
		/// 设置多边形
		///
		/// https://blog.csdn.net/linxinfa/article/details/78816362
		/// https://www.cnblogs.com/lan-yt/p/9200621.html
		/// </summary>
		/// <param name="vertices">顺时针排序的点集</param>
		/// <returns></returns>
		public static Mesh CreateMeshPolygon(Vector3[] vertices)
		{
			// triangles
			List<int> indices = new List<int>();

			int n = vertices.Length;

			int[] V = new int[n];
			if (PolygonArea(vertices) > 0)
			{
				for (int v = 0; v < n; v++)
					V[v] = v;
			}
			else
			{
				for (int v = 0; v < n; v++)
					V[v] = (n - 1) - v;
			}

			int nv = n;
			int count = 2 * nv;
			for (int m = 0, v = nv - 1; nv > 2;)
			{
				if ((count--) <= 0)
					break;

				int u = v;
				if (nv <= u)
					u = 0;
				v = u + 1;
				if (nv <= v)
					v = 0;
				int w = v + 1;
				if (nv <= w)
					w = 0;

				if (PolygonSnip(vertices, u, v, w, nv, V))
				{
					int a, b, c, s, t;
					a = V[u];
					b = V[v];
					c = V[w];
					indices.Add(a);
					indices.Add(b);
					indices.Add(c);
					m++;
					for (s = v, t = v + 1; t < nv; s++, t++)
						V[s] = V[t];
					nv--;
					count = 2 * nv;
				}
			}

			indices.Reverse();

			var mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.triangles = indices.ToArray();

			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			return mesh;
		}

		private static float PolygonArea(Vector3[] vertices)
		{
			int n = vertices.Length;
			float ret = 0.0f;
			for (int p = n - 1, q = 0; q < n; p = q++)
			{
				Vector3 pv = vertices[p];
				Vector3 qv = vertices[q];
				ret += pv.x * qv.z - qv.x * pv.z;
			}
			return ret * 0.5f;
		}

		private static bool PolygonSnip(Vector3[] vertices, int u, int v, int w, int n, int[] V)
		{
			int p;
			Vector3 A = vertices[V[u]];
			Vector3 B = vertices[V[v]];
			Vector3 C = vertices[V[w]];
			if (Mathf.Epsilon > (((B.x - A.x) * (C.z - A.z)) - ((B.z - A.z) * (C.x - A.x))))
				return false;

			for (p = 0; p < n; p++)
			{
				if ((p == u) || (p == v) || (p == w))
					continue;
				Vector3 P = vertices[V[p]];
				if (PolygonInsideTriangle(A, B, C, P))
					return false;
			}

			return true;
		}

		private static bool PolygonInsideTriangle(Vector3 a, Vector3 b, Vector3 c, Vector3 p)
		{
			float ax, az, bx, bz, cx, cz, apx, apz, bpx, bpz, cpx, cpz;
			float cCROSSap, bCROSScp, aCROSSbp;

			ax = c.x - b.x; az = c.z - b.z;
			bx = a.x - c.x; bz = a.z - c.z;
			cx = b.x - a.x; cz = b.z - a.z;
			apx = p.x - a.x; apz = p.z - a.z;
			bpx = p.x - b.x; bpz = p.z - b.z;
			cpx = p.x - c.x; cpz = p.z - c.z;

			aCROSSbp = ax * bpz - az * bpx;
			cCROSSap = cx * apz - cz * apx;
			bCROSScp = bx * cpz - bz * cpx;

			return ((aCROSSbp >= 0.0f) && (bCROSScp >= 0.0f) && (cCROSSap >= 0.0f));
		}
	}
}