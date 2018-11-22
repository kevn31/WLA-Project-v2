using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace WeLoveAero
{
    public static class Airplane_Menus 
    {
        [MenuItem("Airplane Tools/Create New Airplane")]
        public static void CreateNewAirplane()
        {
//            
            AirplaneSetup_Window.LaunchSetupWindow();
        }
    }
}
