using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActivationFunctions{

    #region Activation Functions
    public static double SigmoidFunction(double x){

        return 1 / (1 + Mathf.Exp(-(float)x));

    }

    public static double HyperbolicTangent(double x){

        return Math.Tanh(x);

    }

    public static double ReLU(double x){

        return Math.Max(0,x);

    }

    public static double LeakyReLU(double x){

        return Math.Max(0.01 * x, x);

    }

    public static double Swish(double x){

        return (x * SigmoidFunction(x));

    }

    #endregion

}