using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System;

public class HeadPosition : MonoBehaviour
{
    List<Vector3> EyeTrackingVecData = new List<Vector3>();
    List<Vector3> EyeTrackingPosData = new List<Vector3>();
    List<string> EyeTrackingObjData = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test");

        using (var reader = new StreamReader(@"Assets/Scripts/FoveEyes1.csv"))
        {
            var SkipHeader = false;
            while (!reader.EndOfStream)
            {
                if (!SkipHeader)
                {
                    SkipHeader = true;
                    continue;
                }

                string line = reader.ReadLine();
                string[] vals = line.Split(new char[] { ',' });
                Debug.Log("VALS: " + vals[1]);
                EyeTrackingVecData.Add(new Vector3(float.Parse(vals[1]), float.Parse(vals[2]), float.Parse(vals[3])));
                EyeTrackingPosData.Add(new Vector3(float.Parse(vals[4]), float.Parse(vals[5]), float.Parse(vals[6])));
                EyeTrackingObjData.Add(vals[7]);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}