Shader "Unlit/EnergyBallShd"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorGradient ("Gradient LUT", 2D) = "white" {}
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

            sampler2D _ColorGradient;

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
                // sample the texture
                float2 UV = float2(fmod(i.uv.x * 8.0, 1.0), fmod(i.uv.y * 6.0, 1.0));

                float distanceFromCenter = 2.0 * length(UV - float2(0.5, 0.5));

                fixed4 col = fixed4(0.5*tex2D(_MainTex, i.uv).a * tex2D(_ColorGradient, float2(distanceFromCenter, 0)).rgb, 1.0); //tex2D(_ColorGradient, float2(distanceFromCenter, 0)) * tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
