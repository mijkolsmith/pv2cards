public class EffectStats
{
    public EffectStats(int mod, bool l, bool mul)
	{
        modifier = mod;
        left = l;
        multiply = mul;
	}

    public int modifier;
    public bool left;
    public bool multiply;
}