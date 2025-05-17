namespace _20250517_Task21
{
    /*
     Выполните асинхронное умножение матриц. Вот так может выглядеть сигнатура метода, для этой операции:
    static async Task<int[,]> MultiplyMatricesAsync(int[,] matrix1, int[,] matrix2)
     */
    internal class Program
    {
        static async Task Main()
        {
            int[,] matrix1 =
            {
                {10, 12 }, {20, 30}
            };
            int[,] matrix2 =
            {
                {50, 62 }, {40, 80}
            };

            try
            {
                int[,] result = await MultiplyMatricesAsync(matrix1, matrix2);
                Console.WriteLine("Resulting matrix:");
                PrintMatrix(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task<int[,]> MultiplyMatricesAsync(int[,] matrix1, int[,] matrix2)
        {
            return await Task.Run(() =>
            {
                int r1 = matrix1.GetLength(0);
                int c1 = matrix1.GetLength(1);
                int r2 = matrix2.GetLength(0);
                int c2 = matrix2.GetLength(1);

                if (c1 != r2) throw new Exception("Matrix cannot be multiplied");

                int[,] result = new int[r1, c2];

                for (int i = 0; i < r1; i++)
                {
                    for (int j = 0; j < c2; j++)
                    {
                        int sum = 0;
                        for (int k = 0; k <c1; k++)
                        {
                            sum += matrix1[i, k] * matrix2[k, j];
                        }
                        result[i, j] = sum;
                    }
                }

                return result;
            });
        }

        static void PrintMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0;i < rows;i++)
            {
                for (int j = 0;j < cols; j++)
                {
                    Console.Write($" {matrix[i,j] }");
                }
                Console.WriteLine();
            }
        }
    }
}
