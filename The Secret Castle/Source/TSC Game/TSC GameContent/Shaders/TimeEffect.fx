sampler s0;
float2 pos;
int intensity;

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float dist = sqrt(pow(coords.x - pos.x, 2) + pow(coords.y - pos.y, 2));
	float4 color;
	float2 newCoords = coords + float2((coords.x - pos.x) / (dist * pow(intensity, 3)), (coords.y - pos.y) / (dist * pow(intensity, 3)));
	if (newCoords.x > 0.0 && newCoords.x < 1.0 && newCoords.y > 0.0 && newCoords.y < 1.0)
	{
		color = tex2D(s0, newCoords);
	}
	else
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
