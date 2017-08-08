Shader "Custom/myWater" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_Normal("Normal", 2D) = "white"{}
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_SpecPower("SpecPower", Range(0,1)) = .5
		_Emission("Emission", float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf  BlinnPhong alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Normal;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed _SpecPower;
		float _Emission;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_CBUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_CBUFFER_END

		void surf (Input IN, inout SurfaceOutput o) {
			float3 refractVec = refract(UNITY_LIGHTMODEL_AMBIENT, 
														UnpackNormal(tex2D(_Normal, float2(IN.uv_MainTex.x + _Time.x, IN.uv_MainTex.y) )), .7);
			fixed4 c = tex2D (_MainTex, float2(IN.uv_MainTex.x, IN.uv_MainTex.y) + refractVec.xy) * _Color;
			o.Normal = UnpackNormal(tex2D(_Normal, float2(IN.uv_MainTex.y  + _Time.x, IN.uv_MainTex.y)));
			o.Emission = c.rgb * _Emission;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Specular = _SpecPower;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
