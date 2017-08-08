Shader "Custom/MapShader" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	    _TransVal("Transparency Value", Range(0,1)) = 0.5
		_AlphaTex("Alpha Texture", 2D) = "white"{}
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" "Queue" = "Transparent" }
		ZTest Off
		LOD 200

		CGPROGRAM
#pragma surface surf Lambert alpha  

	sampler2D _MainTex;
	sampler2D _AlphaTex;
	float _TransVal;

	struct Input {
		float2 uv_MainTex;
	};

	void surf(Input IN, inout SurfaceOutput o) {
		half4 c = tex2D(_MainTex, IN.uv_MainTex);
		half4 a = tex2D(_AlphaTex, IN.uv_MainTex);
		if (a.r > .8 && a.g > .8 && a.b > .8)
		{
			discard;
		}
		o.Alpha = _TransVal;
		o.Albedo = c.rgb * 2;
		if (a.b > .3)
		{
			o.Alpha = _TransVal;
			o.Albedo = float3(0, .7, .5) * 2;
		}
	}
	ENDCG
	}
		FallBack "Diffuse"
}