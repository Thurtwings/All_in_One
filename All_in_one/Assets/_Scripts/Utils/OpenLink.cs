using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThurtwingsGames.Utils
{
    
    public class OpenLink : MonoBehaviour
    {
        public void OpenAppLink(string link)
        {
            Application.OpenURL(link);
        }

    }
}
