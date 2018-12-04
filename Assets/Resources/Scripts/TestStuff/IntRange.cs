using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntRange : MonoBehaviour
{
    public int Min;
    public int Max;


    public IntRange(int min, int max)
    {
        Min = min;
        Max = max;
    }
	

    public int Random
    {
        get { return UnityEngine.Random.Range(Min, Max); }
    }
}
