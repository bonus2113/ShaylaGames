Shader "Custom/Water" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_PhaseNoise ("Phase Noise", 2D) = "white" {}
		_Normals ("Water normals", 2D) = "bump" {}
		_Flow ("Flow map", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_SpeedModifier ("Speed", Range(0,10)) = 1.0
		_Phase ("Phase", Float) = 1.0
		_Strength ("Strength", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Normals;
		sampler2D _Flow;
		sampler2D _PhaseNoise;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Normals;
			float2 uv_Flow;
			float2 uv_PhaseNoise;
		};

		half _Glossiness;
		half _Metallic;
		float _SpeedModifier;
		float _Phase;
		float _Strength;
		float4 _Color;
	
		void surf (Input IN, inout SurfaceOutputStandard o) {
			
			float4 currentFlow = (tex2D (_Flow, IN.uv_Flow) * 2 - 1.0f) * _Strength;
			float4 phaseNoise = tex2D (_PhaseNoise, IN.uv_PhaseNoise);
			
			float2 uvNorm = IN.uv_Normals;
			
			float time = _Time.y * _SpeedModifier + phaseNoise *_Phase;

			int phaseCountA = floor(time/_Phase);
			int phaseCountB = floor((time+0.5f*_Phase)/_Phase);
			
			float phaseA = fmod(time, _Phase)/_Phase;
			float phaseB = fmod(time + _Phase * (0.5f), _Phase)/_Phase;
			
			float2 uvPhaseA = uvNorm + currentFlow * phaseA + float2(0.12, 0.65) * phaseCountA;
			float2 uvPhaseB = uvNorm + currentFlow * phaseB + float2(0.04, 0.45) * phaseCountB;
			
			float3 normPhaseA = UnpackNormal (tex2D (_Normals, uvPhaseA));
			float3 normPhaseB = UnpackNormal (tex2D (_Normals, uvPhaseB));
			
			float factorPhase = fmod(phaseB, 1.0);
			
			float factor = (factorPhase < 0.5f) ? (factorPhase * 2) : (1.0f - (factorPhase - 0.5) * 2);
						
			float3 normFinal = lerp(normPhaseA, normPhaseB, factor);
			
			float normScale = length(currentFlow.xy)/length(float2(1,1));
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Normal = normFinal; //lerp(float3(0,0,1), normFinal, 2 * normScale/_Strength + 0.1);
			o.Alpha = factor;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}