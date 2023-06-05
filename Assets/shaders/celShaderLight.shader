Shader "Unlit/celShaderNew"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _ShadowStrength("Shadow Strength", Range(0, 1)) = 0.5 //���� �����
        _LightingStrength("Lighting Strength", Range(0, 1)) = 0.5
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                Tags
                {
                    "LightMode" = "ForwardBase"
                    "PassFlags" = "OnlyDirectional"
                }

                Cull Off // ��������� ������

                Stencil
                {
                    Ref 1
                    Comp Always
                    Pass Replace
                }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                    float3 worldNormal : NORMAL;
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _ShadowStrength;
                float _LightingStrength;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    return o;
                }

                fixed4 frag(v2f i, half facing : VFACE) : SV_Target
                {
                    // ����������� ������ �������, ����� ��� ����� ��������� 1
                    float3 normal = normalize(i.worldNormal);
                    // ������������� ������� ������, ���� 
                    // ������� ���������� �� ������
                    half sign = facing > 0.5 ? 1.0 : -1.0;
                    normal *= sign;

                    // ������� Dot Product ��� ������� � ����������� � ��������� �����
                    // _WorldSpaceLightPos0 - ���������� ���������� Unity
                    float NdotL = dot(_WorldSpaceLightPos0, normal);

                    // C������ ������������� ����� �� �����������
                    // ���� ����������� ��������� � ��������� ����� (NdotL > 0), 
                    // �� ��� ��������� ��������.
                    // � ��������� ������ ��������� Shadow Strength ��� ���������
                    float lightIntensity = NdotL > 0 ? 1 : _ShadowStrength;

                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);

                    // ��������� ���������
                    col *= lightIntensity;

                    col.r += (255 - col.r) * _LightingStrength / 500;
                    col.g += (255 - col.g) * _LightingStrength / 500;
                    col.b += (255 - col.b) * _LightingStrength / 500;
                    return col;
                }
                ENDCG
            }
        }
}
