/* Carbon Computing
 * This code uses previously developed code (see Windexglow) 
 * for maniplating the camera movement during play mode.
 * User can use the mouse to pan left, right, up down. 
 * However, user is bound to a space within the 'interviewee's' location.
 * 
 */

/* Sections of this code was taken from the following link:
* 
   Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.  
   Converted to C# 27-02-13 - no credit wanted.
   Simple flycam I made, since I couldn't find any others made public.  
   Made simple to use (drag and drop, done) for regular keyboard layout  
   wasd : basic movement
   shift : Makes camera accelerate
   space : Moves camera on X and Z axis only.  So camera doesn't gain any height
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

public class CC_Camera_Movement : MonoBehaviour
{
    float framenumber = 0;
    int updateframe = 0;
    float updatefps = 02.5F;
    float mainSpeed = 5.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    Vector3 tempHead;
    Vector3 tempVect;
    int yth2 = 0;
    List<Vector3> EyeTrackingVecData = new List<Vector3>();
    List<Vector3> EyeTrackingPosData = new List<Vector3>();
    List<string> EyeTrackingObjData = new List<string>();

    void Start()
    {
        TextAsset reader = (TextAsset)Resources.Load("CC_Data_Camera");

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
        using (var reader = new StreamReader(@"Assets/CarbonComputing/CC_Data_Camera.csv"))
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
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;
        //Mouse  camera angle done.  

        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            p = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }       
        p = p * Time.deltaTime;

        framenumber += Time.deltaTime;

        if (framenumber >= (1 / updatefps))
        { //We want to update every 5 frames, or at 20 fps.
            tempHead = EyeTrackingPosData[updateframe];
            tempVect = EyeTrackingVecData[updateframe];
            Vector3 visionSpot = (tempVect - tempHead).normalized * Vector3.Distance(tempHead, tempVect);
            visionSpot *= -1.0f;
            //visionSpot.z *= 1.1f;
            updateframe++;
            transform.position = visionSpot;
            framenumber -= (1 / updatefps);
        }
    }
    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
