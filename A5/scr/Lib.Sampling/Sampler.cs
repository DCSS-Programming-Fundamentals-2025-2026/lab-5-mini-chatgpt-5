using System.Net.Security;
using scr.Lib.Sampling.Processing;
using scr.Processing;

namespace scr;

public class Sampler : ISampler
{
    public int Sample(float[] probs, float temperature, int topK, Random? rng)
    {
        if (rng == null)
        {
            rng = new Random();
        }

        float[] temperedLogits = TemperatureScaler.Scale(probs, temperature);
        int[] topKIndices = TopKSelector.GetTopKIndices(temperedLogits, topK);
        float[] topKlogits = new float[topKIndices.Length];

        for (int i = 0; i < topKIndices.Length; i++)
        {
            topKlogits[i] = temperedLogits[topKIndices[i]];
        }

        float[] topKProbs = ProbabilityNormalizer.Normalize(topKlogits);
        float randomValue = (float)rng.NextDouble();

        float cumulativeProbability = 0f;
        int selectedIndex = 0;

        for (int i = 0; i < topKProbs.Length; i++)
        {
            cumulativeProbability += topKProbs[i];

            if (randomValue <= cumulativeProbability)
            {
                selectedIndex = i;
                break;
            }
        }

        return topKIndices[selectedIndex];
    }

    public int Sample(float[] probs, float temperature, int topK, int seed)
    {
        Random deterministicRng = new Random(seed);
        return Sample(probs, temperature, topK, deterministicRng);
    }
}