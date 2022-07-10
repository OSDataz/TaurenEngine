// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/map_region_unlit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue"="Geometry+20" "RenderType"="Opaque" "ForceNoShadowCasting" = "False" "DisableBatching" = "True"}
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
			#pragma multi_compile_fwdbase
			
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 pos : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
//				float2 uv2: TEXCOORD1;

			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 color : TEXCOORD1;
//				UNITY_FOG_COORDS(1)
				float4 pos : SV_POSITION;
				float2 normalPos : TEXCOORD2;
				LIGHTING_COORDS(3, 4)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				
//				o.vertex = UnityObjectToClipPos(v.vertex);
 				o.pos = UnityObjectToClipPos(v.pos);
//				o.normalPos = (v.uv2 + 128.0) / 256.0; 
				o.normalPos = (v.pos + 128.0) / 256.0; 

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//				UNITY_TRANSFER_FOG(o,o.vertex);
				//o.color = v.color; 2022.01.15报错注释了的

				TRANSFER_VERTEX_TO_FRAGMENT(o);

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				half4 tex = tex2D(_MainTex, i.uv);

				// Clip range.
//				float alphaRatio = step(0.0, i.normalPos.x) * step(-1.0, -i.normalPos.x) * step(0.0, i.normalPos.y) * step(-1.0, -i.normalPos.y);

				if (i.normalPos.x < 0.0 || i.normalPos.x > 1.0 || i.normalPos.y < 0.0 || i.normalPos.y > 1.0)
				{
					discard;
				}

//				half4 col = half4(i.color.rgb, tex.a * alphaRatio);
				half4 col = half4(i.color.rgb, tex.a);

				float attenuation = LIGHT_ATTENUATION(i);
				col.rgb *= attenuation;

				// apply fog
//				UNITY_APPLY_FOG(i.fogCoord, col);

				return col;
			}
			
			
			ENDCG
		}
	}
		
	Fallback "VertexLit"
}
