/* Carbon Computing
 * This section of code manipulates the player
 * position. 
 * 
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class CC_Player_Movement : MonoBehaviour
{
    float framenumber = 0;
    int updateframe = 0;
    float updatefps = 02.5F;
    Vector3 tempPos;
    int yth = 0;
    List<Vector3> EyeTrackingVecData = new List<Vector3>();
    List<Vector3> EyeTrackingPosData = new List<Vector3>();
    List<string> EyeTrackingObjData = new List<string>();

    void Start()
    {
        TextAsset reader = (TextAsset)Resources.Load("CC_Data_Player_Movement");

        string fs = reader.text;
        //Convert data file into an array of strings consisting of each line
        string[] lineNum = fs.Split(System.Environment.NewLine.ToCharArray());

        for (int i = 1; i < lineNum.Length; i++)
        {
            //Get current line and parse columns
            string line = lineNum[i];
            string[] vals = line.Split(new char[] { ',' });

            Vector3 Vec = new Vector3(0, 0, 0);
            Vector3 Pos = new Vector3(0, 0, 0);

            for (int v = 0; v < vals.Length; v++)
            {
                switch (v)
                {
                    case 1:
                        float.TryParse(vals[1] != null ? vals[1] : "0.0", out Vec.x); break;
                    case 2:
                        float.TryParse(vals[2] != null ? vals[2] : "0.0", out Vec.y); break;
                    case 3:
                        float.TryParse(vals[3] != null ? vals[3] : "0.0", out Vec.z); break;
                    case 4:
                        float.TryParse(vals[4] != null ? vals[4] : "0.0", out Pos.x); break;
                    case 5:
                        float.TryParse(vals[5] != null ? vals[5] : "0.0", out Pos.y); break;
                    case 6:
                        float.TryParse(vals[6] != null ? vals[6] : "0.0", out Pos.z); break;
                    case 7:
                        EyeTrackingObjData.Add(vals[7]); break;
                }
            }
            EyeTrackingVecData.Add(Vec);
            EyeTrackingPosData.Add(Pos);
        }
    }


    /*
    void Start()
    {
        using (var reader = new StreamReader(@"Assets/CarbonComputing/CC_Data_Player_Movement.csv"))
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
                float VecX = 0.0f;
                float VecY = 0.0f;
                float VecZ = 0.0f;

                float PosX = 0.0f;
                float PosY = 0.0f;
                float PosZ = 0.0f;

                float.TryParse(vals[1] != null ? vals[1] : "0.0", out VecX);
                float.TryParse(vals[2] != null ? vals[2] : "0.0", out VecY);
                float.TryParse(vals[3] != null ? vals[3] : "0.0", out VecZ);

                float.TryParse(vals[4] != null ? vals[4] : "0.0", out PosX);
                float.TryParse(vals[5] != null ? vals[5] : "0.0", out PosY);
                float.TryParse(vals[6] != null ? vals[6] : "0.0", out PosZ);
                EyeTrackingVecData.Add(new Vector3(VecX, VecY, VecZ));
                EyeTrackingPosData.Add(new Vector3(PosX, PosY, PosZ));
                EyeTrackingObjData.Add(vals[7]);
            }
        }
    }
    */

    void Update()
    {
        framenumber += Time.deltaTime;
        if (framenumber >= (1 / updatefps))
        { //We want to update every 5 frames, or at 20 fps.
            tempPos = EyeTrackingPosData[updateframe];
            transform.position = tempPos;
            framenumber -= (1 / updatefps);     
        }
    }
}

