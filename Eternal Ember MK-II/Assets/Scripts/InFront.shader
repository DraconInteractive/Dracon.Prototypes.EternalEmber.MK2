Shader "Custom/InFront" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 200

		Pass 
		{
			//ZWrite Off
			ZTest LEqual
		}
	}
	FallBack "Transparent/VertexLit"
}
