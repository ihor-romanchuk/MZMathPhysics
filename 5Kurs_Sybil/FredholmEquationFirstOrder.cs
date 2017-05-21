using System;
using System.Collections.Generic;
using PoohMathParser;

namespace _5Kurs_Sybil
{
    public static class FredholmEquationFirstOrder
    {
        public static double Calculate_Aij(List<double> partitionPoints, List<double> colocationPoints, int i, int j)
        {
            if (partitionPoints[j - 1] > colocationPoints[i])
            {
                return -1 / (2 * Math.PI) * ((partitionPoints[j] - colocationPoints[i]) * Math.Log(partitionPoints[j] - colocationPoints[i]) - (partitionPoints[j - 1] - colocationPoints[i]) * Math.Log(partitionPoints[j - 1] - colocationPoints[i]) - (partitionPoints[j] - partitionPoints[j - 1]));
            }
            else
            {
                if (partitionPoints[j] < colocationPoints[i])
                {
                    return -1 / (2 * Math.PI) * (-(-partitionPoints[j] + colocationPoints[i]) * Math.Log(-partitionPoints[j] + colocationPoints[i]) + (-partitionPoints[j - 1] + colocationPoints[i]) * Math.Log(-partitionPoints[j - 1] + colocationPoints[i]) - (partitionPoints[j] - partitionPoints[j - 1]));
                }
                else
                {
                    return -1 / (2 * Math.PI) * ((-partitionPoints[j - 1] + colocationPoints[i]) * Math.Log(-partitionPoints[j - 1] + colocationPoints[i]) + (partitionPoints[j] - colocationPoints[i]) * Math.Log(partitionPoints[j] - colocationPoints[i]) - (partitionPoints[j] - partitionPoints[j - 1]));
                }
            }
        }
        public static List<List<double>> Calculate_A(List<double> partitionPoints, List<double> colocationPoints)
        {
            List<List<double>> result = new List<List<double>>();

            for (int i = 0; i < colocationPoints.Count; i++)
            {
                result.Add(new List<double>());

                for (int j = 1; j < partitionPoints.Count; j++)
                {
                    result[i].Add(Calculate_Aij(partitionPoints, colocationPoints, i, j));
                }
            }

            return result;
        }

        public static List<double> Calculate_Fj(string func, List<double> points)
        {
            List<double> result = new List<double>();

            MathExpression function = new MathExpression(func);

            for (int i = 0; i < points.Count; i++)
            {
                result.Add(function.Calculate(points[i]));
            }

            return result;
        }
    }
}
