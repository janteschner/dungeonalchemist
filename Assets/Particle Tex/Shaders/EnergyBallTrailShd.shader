Shader "Unlit/EnergyTrailShd"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR] _ColorOne ("Color 1", Color) = (1.0, 0.0, 0.0, 1.0)
        [HDR] _ColorTwo ("Color 2", Color) = (0.0, 1.0, 0.0, 1.0)
        [HDR] _ColorThree ("Color 3", Color) = (0.0, 0.0, 1.0, 1.0)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
               "Queue"="Transparent"}
        LOD 100

        Pass
        {
            ZWrite Off
            Blend One One

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

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
            float4 _MainTex_ST;

            float4 _ColorOne;
            float4 _ColorTwo;
            float4 _ColorThree;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 UV = i.uv * float2(0.75, 1.0) - float2(5.0 * _Time.y, 0.0);

                float distanceFromCenter = 2.0 * length(UV - float2(0.5, 0.5));

                fixed4 col = tex2D(_MainTex, UV).r * _ColorOne + tex2D(_MainTex, UV).g * _ColorTwo + tex2D(_MainTex, UV).b * _ColorThree;
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
