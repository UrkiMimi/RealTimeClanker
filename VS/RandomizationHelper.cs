using System;
using UnityEngine;


namespace RealTimeClanker
{
    public class RandomizationHelper
    {
        //instancing
        public static RandomizationHelper instance { get; private set; }

        // returns random vector3 from one value
        public Vector3 randomVector3(float value)
        {
            return new Vector3(UnityEngine.Random.Range(-value, value), UnityEngine.Random.Range(-value, value), UnityEngine.Random.Range(-value, value));
        }

        // returns random color from one value
        // main reason why unityengine is used for each random call is cause visual studio fucking sucks
        public Color randomColor(float value)
        {
            return new Color(UnityEngine.Random.Range(-value, value), UnityEngine.Random.Range(-value, value), UnityEngine.Random.Range(-value, value), UnityEngine.Random.Range(-value, value));
        }

        // specifically for shaders and translate and rotate functions
        public float randomFloat(float value)
        {
            return UnityEngine.Random.Range(-value,value);
        }

        // for roulette
        public bool randomBool(int chances, int multi = 1)
        {
            return UnityEngine.Random.Range(0, chances * multi) == 0;
        }

        // changes seed to current time 
        public void newSeed()
        {
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        }
    }
}