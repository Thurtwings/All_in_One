using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace ThurtwingsGames
{
    
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        internal Button[] levelButtons;
        
        

        public void LoadLevel(int levelBuildIndex)
        {
            SceneManager.LoadScene(levelBuildIndex);
        }
        
    }
}
