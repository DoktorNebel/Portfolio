sampler s0;
float2 seed;
float intensity;

float random( float2 p )
{
  const float2 r = float2(
    23.1406926327792690,
     2.6651441426902251);
  return frac( cos( fmod( 123456789., 1e-7 + 256. * dot(p,r) ) ) );  
}

float4 PixelShaderFunction(float2 coords: TEXCOORD0) : COLOR0
{
	float4 color = tex2D(s0, coords + float2(random(coords) * intensity, random(coords) * intensity));
	if (color.a > 0)
	{
		color.a = 1 - (10 * intensity);
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
