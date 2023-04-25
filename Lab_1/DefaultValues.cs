namespace Lab_1
{
    public class DefaultValues
    {
        public MatExt Cond_LU_Gaussian = new()
        {
            A = new float[4, 4]
            {
                {8f, 8f, -5f, -8f},
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
        public MatExt Cond_Rundown = new()
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
        public MatExt Cond_Iterative_Zeidel = new()
        {
            A = new float[4, 4]
            {
                {-19f, 2f, -1f, -8f},
                {2f, 14f, 0f, -4f},
                {6f, -5f, -20f, -6f},
                {-6f, 4f, -2f, 15f}
            },
            B = new float[4, 1]
            {
                {38f},
                {20f},
                {52f},
                {43f}
            }
        };
        public Mat Cond_Jakobi = new()
        {
            A = new float[3, 3]
            {
                {5f, -3f, -4f},
                {-3f, -3f, 4f},
                {-4f, 4f, 0f}
            }
        };
    }
}
