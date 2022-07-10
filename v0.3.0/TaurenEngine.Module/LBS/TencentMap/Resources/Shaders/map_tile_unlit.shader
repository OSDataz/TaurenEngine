// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/map_tile_unlit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_TextureOrVertexColor("IsTextureOrVertexColor", Int) = 1
	}
	SubShader
	{
		Tags { "Queue"="Geometry+15" "RenderType"="Opaque" "ForceNoShadowCasting" = "False" "DisableBatching" = "True"}

		Blend SrcAlpha OneMinusSrcAlpha

		ZTest Off
		ZWrite Off
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 color : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			int _TextureOrVertexColor;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
 				o.vertex = UnityObjectToClipPos(v.vertex);      //o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color;

				return o;
			}
			
			//calculate pixel color;
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				half4 tex = tex2D(_MainTex, i.uv);

				half4 col = half4(i.color.rgb, tex.a);

				if(_TextureOrVertexColor == 0)
				{
				  return col;
				}
				else
				{
				  return tex;
				}

			}

			ENDCG
		}
	}
}