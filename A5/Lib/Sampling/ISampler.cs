namespace Lib;

public interface ISampler
{
    int Sample(float[] probs, float temperature, int topK, Random? rng);
    int Sample(float[] probs, float temperature, int topK, int seed);
}