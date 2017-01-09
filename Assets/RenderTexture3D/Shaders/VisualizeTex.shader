// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/VisualizeTex"
{
	Properties
	{
		_Tex ("Texture", 3D) = "white" {}
		_Scale ("scale", Float) = 1.0
		_Amp ("amplitude", Float) = 1.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
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
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float3 wPos : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler3D _Tex;
			float _Scale,_Amp;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.wPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex3D(_Tex, i.wPos * _Scale);

				return col * _Amp;
			}
			ENDCG
		}
	}
}
