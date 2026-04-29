float4 main(float4 pos : SV_POSITION, float2 uv : TEXCOORD) : SV_TARGET {
    float glitch = sin(uv.y * 10.0 + time) * 0.01;
    float4 color = tex.Sample(sampler, uv + float2(glitch, 0));
    return color * float4(0.0, 1.0, 1.0, 1.0);
}
