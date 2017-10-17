Shader "Hidden/DS_HazeLightVolume"
{
	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 300

		Pass // 0 - Point light.
		{
			Cull Front
			ZTest Always
			ZWrite Off
			Blend One One

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment DSFP_PointVolume
			#pragma multi_compile __ SHADOWS_CUBE
			#pragma multi_compile __ POINT_COOKIE
			#pragma multi_compile __ DENSITY_TEXTURE
			#pragma multi_compile SAMPLES_4 SAMPLES_8 SAMPLES_16 SAMPLES_32

			#define POINT
			#define SHADOWS_NATIVE
			#include "DS_LightVolumeLib.cginc"
			ENDCG
		}

		Pass // 1 - Spot light (Inside the cone)
		{
			Cull Front
			ZTest Always
			ZWrite Off
			Blend One One

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment DSFP_SpotVolumeInterior
			#pragma multi_compile __ SPOT_COOKIE			
			#pragma multi_compile __ SHADOWS_DEPTH			
			#pragma multi_compile __ DENSITY_TEXTURE
			#pragma multi_compile SAMPLES_4 SAMPLES_8 SAMPLES_16 SAMPLES_32

			#define SPOT
			#define SHADOWS_NATIVE
			#include "DS_LightVolumeLib.cginc"
			ENDCG
		}

		Pass // 2 - Spot light (Outside the cone)
		{
			Cull Front
			ZTest Always
			ZWrite Off
			Blend One One

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment DSFP_SpotVolumeExterior
			#pragma multi_compile __ SPOT_COOKIE
			#pragma multi_compile __ SHADOWS_DEPTH
			#pragma multi_compile __ DENSITY_TEXTURE
			#pragma multi_compile SAMPLES_4 SAMPLES_8 SAMPLES_16 SAMPLES_32

			#define SPOT
			#define SHADOWS_NATIVE
			#include "DS_LightVolumeLib.cginc"
			ENDCG
		}

		Pass // 3 - Point light, 1/4.
		{
			Cull Front
			ZTest Always
			ZWrite Off
			Blend One One

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment DSFP_PointVolume
			#pragma multi_compile __ SHADOWS_CUBE
			#pragma multi_compile __ POINT_COOKIE
			#pragma multi_compile __ DENSITY_TEXTURE
			#pragma multi_compile SAMPLES_4 SAMPLES_8 SAMPLES_16 SAMPLES_32

			#define POINT
			#define SHADOWS_NATIVE
			#define kDownsampleFactor 4
			#include "DS_LightVolumeLib.cginc"
			ENDCG
		}

		Pass // 4 - Spot light (Inside the cone), 1/4
		{
			Cull Front
			ZTest Always
			ZWrite Off
			Blend One One

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment DSFP_SpotVolumeInterior
			#pragma multi_compile __ SPOT_COOKIE			
			#pragma multi_compile __ SHADOWS_DEPTH			
			#pragma multi_compile __ DENSITY_TEXTURE
			#pragma multi_compile SAMPLES_4 SAMPLES_8 SAMPLES_16 SAMPLES_32

			#define SPOT
			#define SHADOWS_NATIVE
			#define kDownsampleFactor 4
			#include "DS_LightVolumeLib.cginc"
			ENDCG
		}

		Pass // 5 - Spot light (Outside the cone), 1/4
		{
			Cull Front
			ZTest Always
			ZWrite Off
			Blend One One

			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment DSFP_SpotVolumeExterior
			#pragma multi_compile __ SPOT_COOKIE
			#pragma multi_compile __ SHADOWS_DEPTH
			#pragma multi_compile __ DENSITY_TEXTURE
			#pragma multi_compile SAMPLES_4 SAMPLES_8 SAMPLES_16 SAMPLES_32

			#define SPOT
			#define SHADOWS_NATIVE
			#define kDownsampleFactor 4
			#include "DS_LightVolumeLib.cginc"
			ENDCG
		}
	}
}
