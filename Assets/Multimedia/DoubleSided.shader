Shader "Custom/DoubleSidedShinyShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _Shininess ("Shininess", Range(0.0, 1.0)) = 0.8
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        // 設置雙面渲染
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _Shininess;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = normalize(v.normal);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 獲取光照
                fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 normal = normalize(i.normal);
                float NdotL = max(0, dot(normal, lightDir));

                // 計算顏色和光澤
                fixed4 texColor = tex2D(_MainTex, i.uv) * _Color;
                fixed4 finalColor = texColor * NdotL;

                // 加入高光效果
                finalColor += pow(NdotL, _Shininess) * fixed4(1, 1, 1, 1) * _Color;

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
