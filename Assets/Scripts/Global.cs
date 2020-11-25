using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{
    public static bool HandsAreWashed = false;
    public static bool ForAnimHandsAreWashed = false;
    public static bool BandageIsApplied = false;
    public static bool BetonIsSoaked = false;
    public static bool BetonIsApplied = false;

    public static bool PlayerGrabbed = false;
    public static bool BandageIsGrabbed = false;
    public static bool BetonIsGrabbed = false;
    public static bool BasinIsGrabbed = false;
    public static bool PlayerIsWorkingOnleg = false;

    public static float TextTimer = 0;
    public static float GrabTimer = 0;

    public static int SlotOfReturnable = -1;
    public static bool WaterIsActive = false;
}
