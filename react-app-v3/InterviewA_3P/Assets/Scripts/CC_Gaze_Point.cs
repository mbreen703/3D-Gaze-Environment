using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class CC_Gaze_Point : MonoBehaviour
{

    Vector3 tempPos;
    Vector3 tempVec;
    Vector3 lookPos;
    RaycastHit hit;
    float framenumber = 0;
    int updateframe = 0;
    float updatefps = 02.5F;


    int yth2 = 0;
    List<Vector3> EyeTrackingVecData = new List<Vector3>();
    List<Vector3> EyeTrackingPosData = new List<Vector3>();
    List<string> EyeTrackingObjData = new List<string>();

    void Start()
    {
        TextAsset reader = (TextAsset)Resources.Load("CC_Data_Gaze_Point");

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
        using (var reader = new StreamReader(@"Assets/CarbonComputing/CC_Data_Gaze_Point.csv"))
        {
            var SkipHeader = false;
            while (!reader.EndOfStream)
            {
                if (!SkipHeader)
                {
                    SkipHeader = true;
                    continue;
                }
                string line2 = reader.ReadLine();
                string[] vals2 = line2.Split(new char[] { ',' });
                //Debug.Log("##### vision tracking test   A");
                float VecX2 = 0.0f;
                float VecY2 = 0.0f;
                float VecZ2 = 0.0f;

                float PosX2 = 0.0f;
                float PosY2 = 0.0f;
                float PosZ2 = 0.0f;

                float.TryParse(vals2[1] != null ? vals2[1] : "0.0", out VecX2);
                float.TryParse(vals2[2] != null ? vals2[2] : "0.0", out VecY2);
                float.TryParse(vals2[3] != null ? vals2[3] : "0.0", out VecZ2);

                float.TryParse(vals2[4] != null ? vals2[4] : "0.0", out PosX2);
                float.TryParse(vals2[5] != null ? vals2[5] : "0.0", out PosY2);
                float.TryParse(vals2[6] != null ? vals2[6] : "0.0", out PosZ2);
                EyeTrackingVecData2.Add(new Vector3(VecX2, VecY2, VecZ2));
                EyeTrackingPosData2.Add(new Vector3(PosX2, PosY2, PosZ2));
                EyeTrackingObjData2.Add(vals2[7]);
            }
        }
    }
    */

    void Update()
    {
        /*
        tempPos = EyeTrackingPosData2[yth2];
        tempVec = EyeTrackingVecData2[yth2];
        //raycast
        if (Physics.Raycast(tempPos, tempVec, out hit))
        { // We have hit geometry of some sort
            lookPos = tempPos + tempVec * hit.distance;
        }
        yth2++;
        transform.position = lookPos;
        Thread.Sleep(250);
        */
        framenumber += Time.deltaTime;

        if (framenumber >= (1 / updatefps))
        {
            tempPos = EyeTrackingPosData[updateframe];
            tempVec = EyeTrackingVecData[updateframe];

            if (Physics.Raycast(tempPos, tempVec, out hit))
            { // We have hit geometry of some sort.
                lookPos = tempPos + tempVec * hit.distance;
            }

            if (updateframe < EyeTrackingPosData.Count)
            {
                Debug.Log(updateframe);
                updateframe++;
            } 

            transform.position = lookPos;
            framenumber -= (1 / updatefps);
        }
    }

}