Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1, 1, 0, 1) // Желтый по умолчанию
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.01 // Толщина контура
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        // Основной проход (рендер объекта)
        HLSLPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
        }
        ENDCG

        // Проход для контура (используем вершинный смещение)
        Pass
        {
            Cull Front // Рендерим только "изнанку" меша (чтобы контур был вокруг)

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float _OutlineWidth;
            float4 _OutlineColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz += v.normal * _OutlineWidth; // "Раздуваем" меш по нормалям
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor; // Цвет контура
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
