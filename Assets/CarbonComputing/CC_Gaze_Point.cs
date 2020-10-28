using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using System.Threading;

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
    List<Vector3> EyeTrackingVecData2 = new List<Vector3>();
    List<Vector3> EyeTrackingPosData2 = new List<Vector3>();
    List<string> EyeTrackingObjData2 = new List<string>();

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

        //IF CHANGING TO A CYLINDER 

        //https://forum.unity.com/threads/draw-cylinder-between-2-points.23510/


        if (framenumber >= (1 / updatefps))
        {
            tempPos = EyeTrackingPosData2[updateframe];
            tempVec = EyeTrackingVecData2[updateframe];

            if (Physics.Raycast(tempPos, tempVec, out hit))
            { // We have hit geometry of some sort.
                lookPos = tempPos + tempVec * hit.distance;
            }

            if (updateframe < EyeTrackingPosData2.Count)
            {
                Debug.Log(updateframe);
                updateframe++;
            }
               

            transform.position = lookPos;
            framenumber -= (1 / updatefps);
        }
    }

}