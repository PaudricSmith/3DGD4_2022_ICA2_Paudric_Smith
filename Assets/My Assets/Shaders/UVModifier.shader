Shader "Unlit/UVModifier"
{
	Properties
	{		
		_MainTex ("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags {"RenderType" = "Opaque" }
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			float4 _MainTex_ST; // optional

			struct AppData 
			{
				float4 vertex : POSITION; // vertex position
				float4 normals : NORMAL; 
				float2 uv : TEXCOORD0; // uv0 coordinates
			};

			struct v2f // Interpolators
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			sampler2D _Pattern;

			v2f vert(AppData v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.uv.y += _Time.x * 0.5;

				return o;
			}

			float4 frag(v2f i) : SV_Target
			{
				float4 mainTexture = tex2D(_MainTex, i.uv);

				return mainTexture;
			}
				ENDCG
			}	
		}
			FallBack "Diffuse"
}