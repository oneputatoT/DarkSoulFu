// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "DarkSoul/Bu"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BumpTex("BumpTex",2D)="bump"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            Tags
            {
                "LightMode"="ForwardBase"
            }
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase_fullshadow

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            sampler2D _MainTex;
            sampler2D _BumpTex;

            struct a2v
            {
                float4 vertex:POSITION;
                float3 normal:NORMAL;
                float4 tangent:TANGENT;
                float2 uv:TEXCOORD0;
            };

            struct v2f
            {
                float4 pos:SV_POSITION;
                float3 worldNormal:TEXCOORD0;
                float3 worldTangent:TEXCOORD1;
                float3 worldBiTangent:TEXCOORD2;
                float2 uv:TEXCOORD3;
                float3 worldPos:TEXCOORD4;
            };

            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld,v.vertex).xyz;
                o.worldNormal = normalize(mul(v.normal,(float3x3)unity_WorldToObject));
                o.worldTangent = normalize(mul(unity_ObjectToWorld,v.tangent).xyz);
                o.worldBiTangent = normalize(cross(o.worldNormal,o.worldTangent)*v.tangent);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i):SV_Target
            {
                float3 lDirWs = normalize(UnityWorldSpaceLightDir(i.worldPos));



                float3 nDirTs = UnpackNormal(tex2D(_BumpTex,i.uv));
                float3x3 TBN = float3x3(i.worldTangent,i.worldBiTangent,i.worldNormal);
                float3 nDirWs = mul(nDirTs,TBN);

                float4 mainTex = tex2D(_MainTex,i.uv);


                float3 diffuse = mainTex.rgb * (dot(lDirWs,nDirWs)*0.5+0.5);

                

                return float4(diffuse,1.0);
            }
            ENDCG
        }
    }
}
