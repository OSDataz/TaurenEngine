﻿Shader "Custom/map_building_surface_shader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	
	SubShader {

		Tags { "Queue"="Transparent-10" "IgnoreProjector"="True" "RenderType"="Transparent" "ForceNoShadowCasting" = "False" "DisableBatching" = "True"}
		Blend SrcAlpha OneMinusSrcAlpha

		ZTest LEqual
		ZWrite On

		LOD 100
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
//		#pragma surface surf Standard fullforwardshadows vertex:vert
//		#pragma surface surf Lambert fullforwardshadows vertex:vert
		#pragma surface surf Lambert fullforwardshadows 

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input 
		{
			float2 uv_MainTex;
			float2 localNormalPos;
			float4 color : COLOR;
		};

		half _Glossiness;
		half _Metallic;

//		 void vert (inout appdata_full v, out Input o) {
//  			UNITY_INITIALIZE_OUTPUT(Input,o);
//   			o.localNormalPos = (v.vertex.xz + 128.0) / 256.0;
// 		}

		void surf (Input IN, inout SurfaceOutput o) {
			// Albedo comes from a texture tinted by color
//			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * IN.color;
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
			// Metallic and smoothness come from slider variables
//			o.Metallic = _Metallic;
//			o.Smoothness = _Glossiness;
			o.Specular = _Metallic;
			o.Gloss = _Glossiness;
			o.Emission = _Metallic;

			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
