using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThurtwingsGames
{
    public class TicTacToeManager : MonoBehaviour
    {
        public bool isPlayerOne = true;
        public Sprite spritePlayerOne;
        public Sprite spritePlayerTwo;
        public Sprite currentSprite;

        

        // Update is called once per frame
        void Update()
        {
            if (isPlayerOne) currentSprite = spritePlayerOne;
            else currentSprite = spritePlayerTwo;
        }

        public void OnClick(GameObject image)
        {
            image.GetComponent<Image>().sprite = currentSprite;
            Color color = image.GetComponent<Image>().color;
            color.a = 1;
            SwitchPlayer();
        }

        void SwitchPlayer()
        {
            isPlayerOne = !isPlayerOne;
        }
    }
}
