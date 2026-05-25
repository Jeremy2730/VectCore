using System;
using VectCore.Core.Mathematics;

namespace VectCore.Core.Transformations;

public static class TransformationMatrix
{
    public static Matrix3x3 CreateTranslation(
        double tx,
        double ty)
    {
        return new Matrix3x3(new double[,]
        {
            {1,0,tx},
            {0,1,ty},
            {0,0,1}
        });
    }

    public static Matrix3x3 CreateRotation(
        double degrees)
    {
        double radians =
            degrees * System.Math.PI / 180.0;

        double cos =
            System.Math.Cos(radians);

        double sin =
            System.Math.Sin(radians);

        return new Matrix3x3(new double[,]
        {
            {cos,-sin,0},
            {sin, cos,0},
            {0,0,1}
        });
    }

    public static Matrix3x3 CreateScale(
        double sx,
        double sy)
    {
        return new Matrix3x3(new double[,]
        {
            {sx,0,0},
            {0,sy,0},
            {0,0,1}
        });
    }
}