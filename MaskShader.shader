Shader "Custom/MaskShader" {
	Properties{
		_MainTex("Base (RGB)", Color) = (0,0,0,0)
		_TransVal("Transparency Value", Range(0,1)) = 0.5
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" "Queue" = "Transparent" }
		ZTest Off
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert alpha  

	fixed4 _MainTex;
	sampler2D _AlphaTex;
	float _TransVal;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		o.Alpha = _TransVal;
		o.Albedo = _MainTex.rgb;
	}
	ENDCG
	}
		FallBack "Diffuse"
}