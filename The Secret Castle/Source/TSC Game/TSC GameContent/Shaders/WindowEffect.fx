sampler s0;
texture windows;
sampler sw = sampler_state{Texture = windows;};

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);
	float4 compColor = tex2D(sw, coords);
	if (compColor.a > 0.3)
	{
		color = float4(0, 0, 0, 0);
	}
    return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
