// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/celShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowStrength("Shadow Strength", Range(0, 1)) = 0.5 //сила теней
        _LightShadowStrength("Light Shadow Strength", Range(0, 1)) = 0.5 //сила теней
        _OutlineWidth("Outline Width", Range(0, 0.1)) = 0.01
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        _OUTLINE_COLOR("OUTLINE Color", Range(0, 1)) = 0.01
    }
    SubShader
    {
             Pass
        {
Blend SrcAlpha OneMinusSrcAlpha
            Tags {"LightMode" = "ForwardAdd"
            "PassFlags" = "OnlyDirectional"
            "RenderType"="Transparent"
        }

            Cull off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            // compile shader into multiple variants, with and without shadows
            // (we don't care about any lightmaps yet, so skip these variants)
            #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
            // shadow helper functions and macros
            #include "AutoLight.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                SHADOW_COORDS(1) // put shadows data into TEXCOORD1
                fixed3 diff : COLOR0;
                fixed3 ambient : COLOR1;
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float4 posWorld : TEXCOORD1;
            };
            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                half3 worldNormal = UnityObjectToWorldNormal(v.normal);
                half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
                o.diff = nl * _LightColor0.rgb;
                o.ambient = ShadeSH9(half4(worldNormal,1));
                // compute shadows data
                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                TRANSFER_SHADOW(o);
                return o;
            }

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ShadowStrength;
            float _LightShadowStrength;
            bool _UnderLight;
            float4 _AmbientColor;
            half _OutlineWidth;
            float4 _RimColor;
            float _RimAmount;


            fixed4 frag(v2f i, half facing : VFACE) : SV_Target
            {
                float3 normal = normalize(i.worldNormal);
                // –азворачиваем нормаль внутрь, если 
                // нормаль направлена от камеры
                half sign = facing > 0.5 ? 1.0 : -1.0;
                normal *= sign;

                // —читаем Dot Product дл€ нормали и направлени€ к источнику света
                // _WorldSpaceLightPos0 - встроенна€ переменна€ Unity
                float shadow = SHADOW_ATTENUATION(i);
                float NdotL = dot(_WorldSpaceLightPos0, normal);

                // Cчитаем интенсивность света на поверхности
                // ≈сли поверхность повернута к источнику света (NdotL > 0), 
                // то она полностью освещена.
                // ¬ противном случае учитываем Shadow Strength дл€ затенени€
                float lightIntensity = 1;
                fixed4 col = tex2D(_MainTex, i.uv);
                if (_WorldSpaceLightPos0.w == 0.0) {

                    if (NdotL < -0.5) {
                        lightIntensity = _ShadowStrength;
                    }
                    else if (NdotL < 0) {
                        lightIntensity = _LightShadowStrength;
                    }
                    if (shadow < 0.2) {
                        lightIntensity = _ShadowStrength;
                    }
                    else if (shadow < 0.5) {
                        lightIntensity = _LightShadowStrength;
                    }
                    // float lightIntensity = NdotL > 0 ? 1 : _ShadowStrength;

                        // sample the texture


                    /*float3 viewDir = normalize(i.viewDir);

                    float4 rimDot = 1 - dot(viewDir, normal);
                    float rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimDot);
                    float4 rim = rimIntensity * _RimColor;*/

                    // ѕримен€ем затенение
                    col.rgb *= (lightIntensity) * (_LightColor0) * (_AmbientColor);
                    col.a = _AmbientColor.a;
                    return col;
                }
                else {
                    return col;
                }
            }
        ENDCG
                }
                GrabPass{
                "_GrabTexture"
                }
            Pass
        {
Blend SrcAlpha OneMinusSrcAlpha
            Tags {"LightMode" = "ForwardAdd"
"RenderType"="Transparent"  
        }

            Cull off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

        // compile shader into multiple variants, with and without shadows
        // (we don't care about any lightmaps yet, so skip these variants)
        #pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap
        // shadow helper functions and macros
        #include "AutoLight.cginc"

        struct v2f
        {
            float4 uv : TEXCOORD0;
            SHADOW_COORDS(1) // put shadows data into TEXCOORD1
            fixed3 diff : COLOR0;
            fixed3 ambient : COLOR1;
            float4 pos : SV_POSITION;
            float3 worldNormal : NORMAL;
            float4 posWorld : TEXCOORD1;
            
        };
        
        v2f vert(appdata_base v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = ComputeGrabScreenPos(o.pos);
            half3 worldNormal = UnityObjectToWorldNormal(v.normal);
            half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
            o.diff = nl * _LightColor0.rgb;
            o.ambient = ShadeSH9(half4(worldNormal,1));
            // compute shadows data
            o.worldNormal = UnityObjectToWorldNormal(v.normal);
            
            o.posWorld = mul(unity_ObjectToWorld, v.vertex);
            TRANSFER_SHADOW(o);
            return o;
        }

        sampler2D _GrabTexture;
        sampler2D _MainTex;
        float4 _MainTex_ST;
        float _ShadowStrength;
        float _LightShadowStrength;
        bool _UnderLight;
        float4 _AmbientColor;
        half _OutlineWidth;
        float4 _RimColor;
        float _RimAmount;


        fixed4 frag(v2f i, half facing : VFACE) : SV_Target
        {
            float3 normal = normalize(i.worldNormal);
            // –азворачиваем нормаль внутрь, если 
            // нормаль направлена от камеры
            half sign = facing > 0.5 ? 1.0 : -1.0;
            normal *= sign;

            // —читаем Dot Product дл€ нормали и направлени€ к источнику света
            // _WorldSpaceLightPos0 - встроенна€ переменна€ Unity
            float shadow = SHADOW_ATTENUATION(i);
            float NdotL = dot(_WorldSpaceLightPos0, normal);

            // Cчитаем интенсивность света на поверхности
            // ≈сли поверхность повернута к источнику света (NdotL > 0), 
            // то она полностью освещена.
            // ¬ противном случае учитываем Shadow Strength дл€ затенени€
            float lightIntensity = 1;
            fixed4 col = tex2Dproj(_GrabTexture, i.uv);
            if (_WorldSpaceLightPos0.w == 0.0) {
                

                return col;
            }
            else {
                float3 fragmentToLightSource = _WorldSpaceLightPos0.xyz - i.posWorld.xyz;
                float distance = length(fragmentToLightSource);
                if (distance < 2) {
                    lightIntensity = 2;
                }
                else if (distance < 6) {
                    lightIntensity = 1 + _LightShadowStrength;
                    
                }
                else if (distance < 10) {
                    lightIntensity = 1 + _ShadowStrength;
                }
                else{
                    lightIntensity = 1;
                }
                /*if (shadow < 0.2) {
                    lightIntensity = lightIntensity - 0.2;
                }
                else if (shadow < 0.5) {
                    lightIntensity = lightIntensity - 0.1;
                }*/
                col *= (lightIntensity) * (_LightColor0) * (_AmbientColor);
                col.a = _AmbientColor.a;
                return col;
            }
        }
    ENDCG
    }

        // shadow casting support
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"

            Pass
        {
Blend SrcAlpha OneMinusSrcAlpha
            // —крываем полигоны, повернутые к камере
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;

            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            // ќбъ€вл€ем переменные
            half _OutlineWidth;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _OUTLINE_COLOR;
            float4 _AmbientColor;

            v2f vert(appdata v)
            {


                v2f o;
                v.vertex *= (1 + _OutlineWidth);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col = col * _OUTLINE_COLOR * (_LightColor0);
                col.a = _AmbientColor.a;
                return col;
            }
            ENDCG
        }
    }
}
