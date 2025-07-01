Shader "Custom/OutlineShader"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1, 1, 0, 1) // ������ �� ���������
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.01 // ������� �������
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        // �������� ������ (������ �������)
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

        // ������ ��� ������� (���������� ��������� ��������)
        Pass
        {
            Cull Front // �������� ������ "�������" ���� (����� ������ ��� ������)

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
                v.vertex.xyz += v.normal * _OutlineWidth; // "���������" ��� �� ��������
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor; // ���� �������
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
