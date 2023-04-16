using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ThurtwingsGames.Utils;

namespace ThurtwingsGames.Mastermind
{
    
    public class Mastermind : MonoBehaviour
    {
        public Animator animator;
        public string[] secretCode = new string[4], secretCodeTemp = new string[4];
        public Transform choice;
        public TMP_Text text;
        [SerializeField] internal Sprite blue, red, black, green, yellow, violet, white;
        private string[] codePlayer = new string[4];
        Dictionary<string, Sprite> dicoSprite = new Dictionary<string, Sprite>();
        internal bool isClosed;
        // Start is called before the first frame update
        private void Awake()
        {
            dicoSprite.Add("Blue", blue);
            dicoSprite.Add("Red", red);
            dicoSprite.Add("Black", black);
            dicoSprite.Add("Green", green);
            dicoSprite.Add("Yellow", yellow);
            dicoSprite.Add("Violet", violet);
        }
        void Start()
        {
            //StartCoroutine(GenerateNewCode());
        }
        public Array GetNewSecretCode()
        {
            for (int i = 0; i < 4; i++)
            {
                int rnd = UnityEngine.Random.Range(0, dicoSprite.Count);
                secretCode.SetValue(dicoSprite.ElementAt(rnd).Key, i);
            }
            choice.Find("C1").GetComponent<Image>().sprite = dicoSprite[secretCode.GetValue(0).ToString()];
            choice.Find("C2").GetComponent<Image>().sprite = dicoSprite[secretCode.GetValue(1).ToString()];
            choice.Find("C3").GetComponent<Image>().sprite = dicoSprite[secretCode.GetValue(2).ToString()];
            choice.Find("C4").GetComponent<Image>().sprite = dicoSprite[secretCode.GetValue(3).ToString()];
            return secretCode;
        }
        // Update is called once per frame
        void Update()
        {
        
        }

        public IEnumerator GenerateNewCode()
        {
            animator.SetTrigger("closed");
            isClosed = true;
            yield return new WaitForSeconds(2f);
            animator.ResetTrigger("closed");
            GetNewSecretCode();
            
            yield return new WaitForSeconds(.2f);
            
        }

        public int GetGoodPositions(string[] code)
        {
            Array.Copy(secretCode, secretCodeTemp, secretCode.Length);

            int good = 0;

            for (int i = 0; i < secretCodeTemp.Length; i++)
            {
                if(code[i] == secretCodeTemp[i])
                {
                    good++;
                    code[i] = "Well placed";
                    secretCodeTemp[i] = "Well placed";
                }
            }
            Array.Copy(code, codePlayer, code.Length);
            return good;
        }
        public int GetWrongPositions()
        {

            int wrong = 0;

            for (int i = 0; i < codePlayer.Length; i++)
            {
                for (int j = 0; j < secretCodeTemp.Length; j++)
                {
                    if(codePlayer[i] == secretCodeTemp[j] && codePlayer[i] != "Well placed" && secretCodeTemp[j] != "Well placed")
                    {
                        secretCodeTemp[j] = "Wrong";
                        wrong++;
                        break;
                    }
                }
                
                
            }
            return wrong;
        }
    }
}
