using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Grid
{
    public static CC_Camera_Movement camMovement;
    public static CC_Gaze_Point gazePoint;
    public static CC_Player_Movement plyMovement;


    static Grid()
    {
        GameObject gm = safeFind("_appManager");

        camMovement = (CC_Camera_Movement)SafeComponent(gm, "CC_Camera_Movement");
        gazePoint = (CC_Gaze_Point)SafeComponent(gm, "CC_Gaze_Point");
        plyMovement = (CC_Player_Movement)SafeComponent(gm, "CC_Player_Movement");

    }

    // when Grid wakes up, it checks everything is in place
    // it uses these trivial routines to do so
    private static GameObject safeFind(string s)
    {
        GameObject gm = GameObject.Find(s);
        if (gm == null) Woe("GameObject " + s + "  not on _preload.");
        return gm;
    }
    private static Component SafeComponent(GameObject gm, string s)
    {
        Component component = gm.GetComponent(s);
        if (component == null) Woe("Component " + s + " not on _preload.");
        return component;
    }
    private static void Woe(string error)
    {
        Debug.Log(">>> Cannot proceed... " + error);
        Debug.Log(">>> It is very likely you just forgot to launch");
        Debug.Log(">>> from scene zero, the _preload scene.");
    }
}
