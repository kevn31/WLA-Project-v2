using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WeLoveAero
{
    public static class Airplane_SetupTools 
    {
        public static void BuildDefaultAirplane(string aName)
        {
            //Create the root GO
            GameObject rootGO = new GameObject(aName, typeof(Airplane_Controller), typeof(BaseAirplane_Input));

            //Create the Center of Gravity
            GameObject cogGO = new GameObject("COG");
            cogGO.transform.SetParent(rootGO.transform, false);

            //Create the Base Components or Find them
            BaseAirplane_Input input = rootGO.GetComponent<BaseAirplane_Input>();
            Airplane_Controller controller = rootGO.GetComponent<Airplane_Controller>();
            Airplane_Characteristics characteristics = rootGO.GetComponent<Airplane_Characteristics>();

            //Setup the Airplane
            if(controller)
            {
                //Assing core Components
                controller.input = input;
                controller.charactistics = characteristics;
                controller.centerOfGravity = cogGO.transform;

                //Create Structure
                GameObject graphicsGrp = new GameObject("Graphics_GRP");
                GameObject collisionGrp = new GameObject("Collision_GRP");
                GameObject controlSurfaceGrp = new GameObject("ControlSurfaces_GRP");

                graphicsGrp.transform.SetParent(rootGO.transform, false);
                collisionGrp.transform.SetParent(rootGO.transform, false);
                controlSurfaceGrp.transform.SetParent(rootGO.transform, false);

                //Create First Engine
                GameObject engineGO = new GameObject("Engine", typeof(Airplane_Engine));
                Airplane_Engine engine = engineGO.GetComponent<Airplane_Engine>();
                controller.engines.Add(engine);
                engineGO.transform.SetParent(rootGO.transform, false);

                //Create the base Airplane
                GameObject defaultAirplane = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/AirplanePhysics/Art/Objects/Airplanes/Indie-Pixel_Airplane/IndiePixel_Airplane.fbx", typeof(GameObject));
                if (defaultAirplane)
                {
                    GameObject.Instantiate(defaultAirplane, graphicsGrp.transform);
                }
            }

            //Select the Airplane Setup
            Selection.activeGameObject = rootGO;

        }
    }
}
