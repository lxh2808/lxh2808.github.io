using System.Collections;
using System.Collections.Generic;
using Invector.vCharacterController;
using UnityEngine;

public static class GaiaConst
{
    public static string Token =
        //"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjE0NjAwMjgwNTAzMzQwMjc3NzYiLCJlbWFpbCI6Ijg5Njk1NjY5MkBxcS5jb20iLCJwaG9uZSI6IjE4Ni0xMDAwMDAwMCIsImlhdCI6MTY1NTcxMTE0MywiZXhwIjoxNjU4MzAzMTQzfQ.uREnvlZOxiqWci9bTCGWTRLvIGNtKlY-yaxoXtfSehU";
         "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjE0NjAxMTk3ODU1MTcwODg3NjgiLCJlbWFpbCI6IjcxNDcyMTdAMTYzLmNvbSIsInBob25lIjoiMTg2LTE4MjExMDIxOTAyIiwiaWF0IjoxNjU0MTUwOTY3LCJleHAiOjE2NTY3NDI5Njd9.HmOLDq-G5yaRCDF7UQ1B17w67ecIIY86eL3r3gEIBmA";
    public static string URL = "https://api.metagaia.io/api/v1";// "https://test-api.metagaia.io/api/v1";
        
    public static UserInfo UserInfo;


    public static string MACAdress;
    
    public static Canvas _canvas;
    public static vThirdPersonInput vInput;
    
    public static Texture2D GaiaCursor;
    public static Texture2D GaiaCursorLight;


    public static int CurrentHeroIndex = 0;
    public static string AvatarURL="";
    public static bool lockCamera = true;

}



