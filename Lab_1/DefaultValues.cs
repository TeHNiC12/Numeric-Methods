namespace Lab_1
{
    public class DefaultValues
    {
        public MatExt Cond1_6 = new()
        {
            A = new float[4, 4]
            {
                {8f, 8f, -5f, -8},
                {8f, -5f, 9f, -8f},
                {5f, -4f, -6f, -2f},
                {8f, 3f, 6f, 6f}
            },
            B = new float[4, 1]
            {
                {13f},
                {38f},
                {14f},
                {-95f}
            }
        };
        public MatExt Cond2 = new()
        {
            A = new float[5, 3]
            {
                {0f, -6f, 5f},
                {-1f, 13f, 6f},
                {-9f, -15f, -4f},
                {-1f, -7f, 1f},
                {9f, -18f, 0f}
            },
            B = new float[5, 1]
            {
                {51f},
                {100f},
                {-12f},
                {47f},
                {-90f}
            }
        };
    }
}
