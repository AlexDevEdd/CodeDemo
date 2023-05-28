Shader "Mobile/DiffuseGrayscale" {
Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
        _Grayscale("Grayscale", Range(0,1)) = 0
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 150

CGPROGRAM
#pragma surface surf Lambert noforwardadd

sampler2D _MainTex;

half _Grayscale;

struct Input {
    float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
    half gray = (c.r + c.g + c.b) / 3.0;
    o.Albedo = lerp(c.rgb, gray.rrr, _Grayscale);
    o.Alpha = c.a;
}
ENDCG
}

Fallback "Mobile/VertexLit"
}