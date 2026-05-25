namespace VectCore.Core.Mathematics;

public class Matrix3x3
{
    public double[,] Values { get; set; }

    public Matrix3x3(double[,] values)
    {
        Values = values;
    }

    public static Matrix3x3 Identity()
    {
        return new Matrix3x3(new double[,]
        {
            {1,0,0},
            {0,1,0},
            {0,0,1}
        });
    }

    public Matrix3x3 Multiply(
        Matrix3x3 other)
    {
        double[,] result = new double[3,3];

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                result[row,col] =
                    Values[row,0] * other.Values[0,col] +
                    Values[row,1] * other.Values[1,col] +
                    Values[row,2] * other.Values[2,col];
            }
        }

        return new Matrix3x3(result);
    }
}