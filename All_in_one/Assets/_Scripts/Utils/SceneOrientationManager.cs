using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.Utils
{
    public class SceneOrientationManager : MonoBehaviour
    {

        public bool isPortrait;
        public bool isLandscape;
        public bool isAuto;
        // Start is called before the first frame update
        void Update()
        {
            if(isPortrait) Screen.orientation = ScreenOrientation.Portrait;
            else if(isLandscape) Screen.orientation = ScreenOrientation.LandscapeRight;
            else if(isAuto) Screen.orientation = ScreenOrientation.AutoRotation;
            
        }

       
    }
}
