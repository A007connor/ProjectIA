Shader "Unlit/PixelisedShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_Threshold("Threshold", Range(0,1)) = 0.1
		_PixelationAmount("Pixelation Amount", Range(0,300)) = 5
	}
		SubShader
		{
			Tags { "Queue" = "Transparent" }
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
					UNITY_FOG_COORDS(1)
					float4 vertex : SV_POSITION;
				};

				sampler2D _MainTex;
				float4 _Color;
				float _Threshold;
				float _PixelationAmount;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					float2 uv = i.uv;
					uv *= _PixelationAmount;
					uv = floor(uv) / _PixelationAmount;
					fixed4 col = tex2D(_MainTex, uv) * _Color;

					if (col.a <= _Threshold)
						col.a = 0; // Rendre les pixels ayant une faible opacité transparents

					return col;
				}
				ENDCG
			}
		}
}
