Shader "Unlit/LightningSmallShd"
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
                float4 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 texcoord : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _ColorGradient;

            float hash(float t)
            {
                return frac(sin(7.289 * t) * 23758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(v);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord.xy = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.texcoord.z = v.texcoord.z;
                o.texcoord.w = v.texcoord.w;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float lifeTime = i.texcoord.z;
                float t = floor(_Time.y * 20.0);
                float2 UV = i.texcoord.xy + float2(hash(t), 0.0);


                float mask = pow(tex2D(_MainTex, UV).r, 3.0);
                float cutoff = step(1.0 - lifeTime, mask);
                float sideFade = pow(clamp(1.0 - 2.0 * abs(i.texcoord.x - 0.5), 0.0, 1.0), 0.1);
                fixed4 col = tex2D(_ColorGradient, float2(0.5 * (1.0 - mask), 0)) * mask * cutoff * sideFade;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }

            ENDCG
        }
    }
}
