// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/map_building_unlit"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader
	{
		Tags { "Queue"="Transparent-10" "IgnoreProjector"="True" "RenderType"="Transparent" "ForceNoShadowCasting" = "False" "DisableBatching" = "True"}

		Blend SrcAlpha OneMinusSrcAlpha

		ZTest LEqual
		ZWrite On

		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
//				float2 uv2: TEXCOORD1;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : TEXCOORD1;
				float2 uv : TEXCOORD0;
//				UNITY_FOG_COORDS(1)
//				float2 normalPos : TEXCOORD2;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;


			v2f vert (appdata v)
			{
				v2f o;
				//Unity4.7.2做法; 
//				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);      //o.vertex = UnityObjectToClipPos(v.vertex);
                //Unity5.+做法; 
 				o.vertex = UnityObjectToClipPos(v.vertex);       //o.vertex = UnityObjectToClipPos(v.vertex);
//				o.normalPos = (v.vertex.xz + 128.0) / 256.0;
//				o.normalPos = (v.uv2 + 128.0) / 256.0;

				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
//				UNITY_TRANSFER_FOG(o,o.vertex);
				o.color = v.color;
				return o;
			}
			
			//calculate pixel color;
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				half4 tex = tex2D(_MainTex, i.uv);

				// apply fog
//				UNITY_APPLY_FOG(i.fogCoord, col);

//				return col;
				return tex;
			}

				ENDCG
		}
	}
}
