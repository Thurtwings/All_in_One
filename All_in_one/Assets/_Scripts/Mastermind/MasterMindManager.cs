using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace ThurtwingsGames.Mastermind
{
    public class MasterMindManager : MonoBehaviour
    {
        [SerializeField] Mastermind mastermind;
        [SerializeField] GameObject[] gameSlot;
        [SerializeField] GameObject[] gameVerif = new GameObject[4];
        private int currentSlot = 0, currentCol = 1;
        [SerializeField] string[] code = new string[4];
        [SerializeField] GameObject winPanel;
        [SerializeField] GameObject loosePanel;
        private Sprite emptySprite;
        public TMP_Text endingText;
        // Start is called before the first frame update
        void Start()
        {
            emptySprite = gameSlot[currentSlot].transform.Find("Slot").transform.Find("Choice").transform.Find("C1").GetComponent<Image>().sprite;
        }

        public void ColorSelect(Sprite sprite)
        {
            if (!mastermind.isClosed) return;

            gameSlot[currentSlot].transform.Find("Slot").transform.Find("Choice").transform.Find("C" + currentCol).GetComponent<Image>().sprite = sprite;
            code.SetValue(sprite.name, currentCol - 1);
            currentCol++;
            if (currentCol == 5)
                currentCol = 1;
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(0);
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
        public void Cancel()
        {
            for (int i = 1; i < 5; i++)
            {
                gameSlot[currentSlot].transform.Find("Slot").transform.Find("Choice").transform.Find("C" + i).GetComponent<Image>().sprite = emptySprite;
                currentCol = 1;
            }
        }
        
        public void Check()
        {
            if (!mastermind.isClosed) return;
            int x = 0;
            foreach (Transform child in gameSlot[currentSlot].transform.Find("Slot").transform.Find("Verif"))
            {
                if(child.tag == "Verif")
                {
                    gameVerif[x] = child.gameObject;
                    x++;
                }
            }

            for (int i = 1; i < 5; i++)
            {
                if (gameSlot[currentSlot].transform.Find("Slot").transform.Find("Choice").transform.Find("C" + i).GetComponent<Image>().sprite == emptySprite)
                    return;
            }

            int nbGoodPosition = mastermind.GetGoodPositions(code);
            for (int i = 0; i < nbGoodPosition; i++)
            {
                gameVerif[i].GetComponent<Image>().sprite = mastermind.black;
            }
            int nbWrongPosition = mastermind.GetWrongPositions();
            for (int i = nbGoodPosition; i < nbWrongPosition + nbGoodPosition; i++)
            {
                gameVerif[i].GetComponent<Image>().sprite = mastermind.white;
            }
            Debug.Log("Good position: " + mastermind.GetGoodPositions(code));
            Debug.Log("Wrong position: " + mastermind.GetWrongPositions());
            if(nbGoodPosition == 4)
            {
                //Do win anim etc
                Debug.Log("You win!");
                Win();
                return;
            }
            if(currentSlot == 9)
            {
                Debug.Log("You loose");
                GameOver();
                return;
            }

            currentSlot++;
            Color originalColor = gameSlot[currentSlot].transform.GetChild(0).GetComponent<Image>().color;
            Color selectedColor = originalColor;
            selectedColor.a = .25f;

            gameSlot[currentSlot].transform.GetChild(0).GetComponent<Image>().color = selectedColor;
            gameSlot[currentSlot-1].transform.GetChild(0).GetComponent<Image>().color = originalColor;
        }

        private void GameOver()
        {

            mastermind.animator.Play("OpenMastermind");

            mastermind.isClosed = false;
            loosePanel.SetActive(true);
        }

        private void Win()
        {
            mastermind.animator.Play("OpenMastermind");
            mastermind.isClosed = false;
            winPanel.SetActive(true);
            if((currentSlot + 1) > 1)
            {
                endingText.text = "Tu as réussi à trouvé le code secret du Mastermind en " + (currentSlot + 1) + " coups!";
            }
            else
            {
                endingText.text = "Tu as réussi à trouvé le code secret du Mastermind du premier coup!";

            }
        }
    }
}
