namespace scr.Processing;

public static class ProbabilityNormalizer
{
    public static float[] Normalize(float[] logits)
    {
        if (logits == null)
        {
            throw new ArgumentNullException(nameof(logits), "Array of logits cannot be null.");
        }
        
        if (logits.Length == 0)
        {
            throw new ArgumentException("Array of logits cannot be empty.", nameof(logits));
        }
        
        float[] probs = new float[logits.Length];

        float maxLogit = float.MinValue;
        for (int i = 0; i < probs.Length; i++)
        {
            if (logits[i] > maxLogit)
            {
                maxLogit = logits[i];
            }
        }

        float sumExp = 0f;
        for (int i = 0; i < probs.Length; i++)
        {
            probs[i] = (float)Math.Exp(probs[i] - maxLogit);
            sumExp += probs[i];
        }

        for (int i = 0; i < probs.Length; i++)
        {
            probs[i] /= sumExp;
        }
        
        return probs;
    }
}