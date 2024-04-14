Shader "Custom/FortniteStorm"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        _Intensity("Intensity", Range(0, 1)) = .5

        _FresnelColor ("Fresnel Color", Color) = (1,1,1,1)
        [PowerSlider(4)] _FresnelExponent ("Fresnel Exponent", Range(0.25, 4)) = 1
        _FresnelIntensity("Fresnel Intensity", Range(0, 10)) = .5
        _FresnelProgress("Fresnel Progress", Range(0, 1)) = 1


        _ScrollTex1 ("Scroll Tex 1", 2D) = "white" {}
        _ScrollTex2 ("Scroll Tex 2", 2D) = "white" {}



    }
    SubShader
    {
        Tags { "RenderType"="Transparent"  "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha

        LOD 200
        //Cull Off disable backface culling

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0


        sampler2D _MainTex;
        sampler2D _ScrollTex1;
        sampler2D _ScrollTex2;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_ScrollTex1;
            float2 uv_scrollTex2;
            float3 worldNormal;
            float3 viewDir;
            INTERNAL_DATA
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        half _Intensity;

        float3 _FresnelColor;
        float _FresnelExponent;
        half _FresnelIntensity;
        half _FresnelProgress;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            fixed4 tex1 = tex2D (_ScrollTex1, IN.uv_ScrollTex1 * _Time[0]);
            fixed4 tex2 = tex2D (_ScrollTex1, IN.uv_ScrollTex1);


            o.Albedo = c.rgb + min(tex1, tex2);
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = _Intensity;


            // all the fresnel stuff
            //get the dot product between the noN.viewDir);rmal and the view direction
            float fresnel = dot(IN.worldNormal, IN.viewDir);
            //invert the fresnel so the big values are on the outside
            fresnel = saturate(1 - fresnel);
            //raise the fresnel value to the exponents power to be able to adjust it
            fresnel = pow(fresnel, _FresnelExponent);
            //combine the fresnel value with a color
            float3 fresnelColor = fresnel * _FresnelColor;
            //apply the fresnel value to the emission
            o.Emission = _FresnelIntensity * fresnelColor * _FresnelProgress;
        }
        ENDCG

    }
    FallBack "Diffuse"
}
