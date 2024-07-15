Shader"Custom/ShadowColorShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShadowColor ("Shadow Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
#include "UnityCG.cginc"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
};

struct v2f
{
    float4 vertex : SV_POSITION;
    float2 uv : TEXCOORD0;
    float4 shadowColor : COLOR1;
};

sampler2D _MainTex;
float4 _ShadowColor;

v2f vert(appdata v)
{
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.uv = v.uv;
    o.shadowColor = _ShadowColor;
    return o;
}

fixed4 frag(v2f i) : SV_Target
{
                // Sample texture
    fixed4 col = tex2D(_MainTex, i.uv);
                // Apply shadow color
    col *= i.shadowColor;
    return col;
}
            ENDCG
        }
    }
}
