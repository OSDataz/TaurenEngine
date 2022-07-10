// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/MapTransparent"
{
    Properties
    {
        _MainTex ("Albedo Texture", 2D) = "white" {}

        _Color ("Main Color", Color) = (0,0,0,1)
    }

    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        ZTest Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            sampler2D _MainTex;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;

                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                // 为了支持alpha动画.
                col.a *= _Color.a; 

                return col;
            }
            ENDCG
        }
    }
}
