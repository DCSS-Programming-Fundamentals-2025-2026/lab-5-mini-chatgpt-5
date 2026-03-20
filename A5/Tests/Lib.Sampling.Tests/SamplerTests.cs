namespace Tests;

using scr;

[TestFixture]
public class SamplerTests
{
    [Test]
    public void Test1()
    {
        ISampler sampler = new Sampler();
        float[] probs = { 0.1f, 0.5f, 0.3f, 0.05f, 0.05f };
        float temp = 1.0f;
        int topK = 5;
        int seed = 42;

        int[] resultsRun1 = new int[10];
        int[] resultsRun2 = new int[10];
        
        Random rng1 = new Random(seed);
        for (int i = 0; i < 10; i++)
        {
            resultsRun1[i] = sampler.Sample(probs, temp, topK, rng1);
        }
        
        Random rng2 = new Random(seed);
        for (int i = 0; i < 10; i++)
        {
            resultsRun2[i] = sampler.Sample(probs, temp, topK, rng2);
        }
        
        for (int i = 0; i < 10; i++)
        {
            Assert.That(resultsRun1[i], Is.EqualTo(resultsRun2[i]), "Determinism is violated: the results vary.");
        }
    }



        [Test]
    public void Test2()
    {
        ISampler sampler = new Sampler();

        float[] probs = { 0.05f, 0.1f, 0.8f, 0.05f }; 
        float temp = 1.0f;
        int topK = 1;
        Random rng = new Random(123);

        for (int i = 0; i < 50; i++)
        {
            int result = sampler.Sample(probs, temp, topK, rng);
            
            Assert.That(result, Is.EqualTo(2), "Top-K clipping is not working properly.");
        }
    }

    
    
    [Test]
    public void Test3()
    {
        ISampler sampler = new Sampler();
        
        float[] probs = { 0.0f, 1.0f, 0.0f }; 
        
        float temp = 1.0f; 
        int topK = 3;
        int seed = 42; 
        
        int result = sampler.Sample(probs, temp, topK, seed);
        
        Assert.That(result, Is.EqualTo(1), "At normal temperatures, the algorithm should correctly handle a standard probability distribution.");
    }
    
}