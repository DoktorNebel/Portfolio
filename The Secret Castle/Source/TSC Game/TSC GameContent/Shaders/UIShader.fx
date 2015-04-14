float4x4 World;
float4x4 View;
float4x4 Projection;

sampler s0;
bool vertical;
float value;
float maxValue;
float up;
float low;
float start;
float end;
bool lines;

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords);
	if (vertical)
	{
		if (coords.y < 1 - (start / 64.0 + (value / maxValue) * ((end - start) / 64.0)))
		{
			color = float4(0, 0, 0, 0);
		}
		if (lines && coords.y > start / 64.0 && coords.y < end / 64.0 && coords.x > up / 64.0 && coords.x < low / 64.0)
		{
			float steps = (end - start) / maxValue;
			if (color.a > 0 && coords.y * 64.0 % steps <= 1.0)
			{
				color = float4(0, 0, 0, 1);
			}
		}
	}
	else
	{
		if (coords.x > start / 1024.0 + (value / maxValue) * ((end - start) / 1024.0))
		{
			color = float4(0, 0, 0, 0);
		}
		if (lines && coords.x > start / 1024.0 && coords.x < end / 1024.0 && coords.y > up / 128.0 && coords.y < low / 128.0)
		{
			float steps = (end - start) / maxValue;
			if (coords.x * 1024.0 % steps <= 2.5)
			{
				color = float4(0, 0, 0, 1);
			}
		}
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
