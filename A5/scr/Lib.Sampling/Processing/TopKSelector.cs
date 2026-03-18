namespace scr.Lib.Sampling.Processing;

public static class TopKSelector
{
    public static int[] GetTopKIndices(float[] values, int k)
    {
        int length = values.Length;

        float[] valuesCopy = new float[length];
        int[] indices = new int[length];

        for (int i = 0; i < length; i++)
        {
            valuesCopy[i] = values[i];
            indices[i] = i; 
        }

        Array.Sort(valuesCopy, indices);

        int actualK = Math.Min(k, length);
        int[] result = new int[actualK];

        for (int i = 0; i < actualK; i++)
        {
            result[i] = indices[length - 1 - i];
        }

        return result;
    }
}