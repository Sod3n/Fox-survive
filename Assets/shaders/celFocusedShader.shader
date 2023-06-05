Shader "Unlit/celFocusedShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _ShadowStrength("Shadow Strength", Range(0, 1)) = 0.5 //сила теней
        _OutlineWidth("Outline Width", Range(0, 0.1)) = 0.01
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

                Cull Off // отключаем кулинг

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

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    // Ќормализуем вектор нормали, чтобы его длина равн€лась 1
                    float3 normal = normalize(i.worldNormal);

                    // —читаем Dot Product дл€ нормали и направлени€ к источнику света
                    // _WorldSpaceLightPos0 - встроенна€ переменна€ Unity
                    float NdotL = dot(_WorldSpaceLightPos0, normal);

                    // Cчитаем интенсивность света на поверхности
                    // ≈сли поверхность повернута к источнику света (NdotL > 0), 
                    // то она полностью освещена.
                    // ¬ противном случае учитываем Shadow Strength дл€ затенени€
                    float lightIntensity = NdotL > 0 ? 1 : _ShadowStrength;

                    // sample the texture
                    fixed4 col = tex2D(_MainTex, i.uv);

                    // ѕримен€ем затенение
                    col *= lightIntensity;
                    return col;
                }
                ENDCG
            }
            Pass
            {
                    // —крываем полигоны, повернутые к камере
                    Cull Off

                    Stencil
                    {
                        Ref 1
                        Comp Greater
                    }

                    CGPROGRAM
                    #pragma vertex vert
                    #pragma fragment frag

                    #include "UnityCG.cginc"

                    struct appdata
                    {
                        float4 vertex : POSITION;
                        float3 normal : NORMAL;
                    };

                    struct v2f
                    {
                        float4 vertex : SV_POSITION;
                    };

                    // ќбъ€вл€ем переменные
                    half _OutlineWidth;
                    static const half4 OUTLINE_COLOR = half4(0,0,0,0);

                    v2f vert(appdata v)
                    {
                        // —мещаем вершины по направлению нормали на заданное рассто€ние
                        v.vertex.xyz += v.normal * _OutlineWidth;

                        v2f o;
                        o.vertex = UnityObjectToClipPos(v.vertex);

                        return o;
                    }

                    fixed4 frag() : SV_Target
                    {
                        // ¬се пиксели контура имеют один и тот же цвет
                        return OUTLINE_COLOR;
                    }
                    ENDCG
                }
        }
}
