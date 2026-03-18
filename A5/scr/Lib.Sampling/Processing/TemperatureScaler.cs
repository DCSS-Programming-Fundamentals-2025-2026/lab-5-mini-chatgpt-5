namespace scr.Lib.Sampling.Processing;

public static class TemperatureScaler
{
    public static float[] Scale(float[] probs, float temperature)
    {
        if (temperature <= 0f)
        {
            throw new ArgumentOutOfRangeException(nameof(temperature), "Temperature must be > 0.");
        }

        float[] tempered = new float[probs.Length];

        for (int i = 0; i < tempered.Length; i++)
        {
            float p = Math.Max(probs[i], 1e-7f);

            tempered[i] = (float)Math.Log(p) / temperature;
        }

        return tempered;
    }
}