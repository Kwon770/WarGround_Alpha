using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour {

    [SerializeField] GameObject[] boxLoc;
    [SerializeField] Image[] messageBox;
    [SerializeField] public bool canSkip;
    
    [SerializeField] public int boxIndex;
    [SerializeField] int scriptIndex;
    [SerializeField] float alphaSpeed;

    [SerializeField] string[] guideText;
    [SerializeField] public int textNumber;

    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ 본게임입니다
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] GameObject[] pressEnterText; 

    private void Start()
    {
        messageBox[boxIndex].color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, 0f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && canSkip == true)  //0, 1, 5, 6enemy, 13, 14enemy, 15, 17, 20 enter 할 시 씬 종료
        {
                if(textNumber == 20)
                {
                GameObject Canvas;
                Canvas = GameObject.Find("LoadingCanvas");
                Canvas.SetActive(true);
                Canvas.GetComponent<LoadTutorial>().Exiting();
                }
                else
                {
                textNumber++;
                StartCoroutine(MessagePrint(boxIndex));
                boxIndex = (boxIndex - 1) * -1;
                }
            
                
            
           
        }

        if (textNumber != 0 && textNumber != 1 && textNumber != 5 && textNumber != 6 && textNumber != 13 && textNumber != 14 && textNumber != 15 && textNumber != 17)
        {
            foreach (GameObject i in pressEnterText)
            {
                i.SetActive(true);
            }
        }
        else
        {
            foreach(GameObject i in pressEnterText)
            {
                i.SetActive(false);
            }
        }
    }



    public IEnumerator MessagePrint(int boxIndex)
    {
        canSkip = false;
        float time = 0;
        messageBox[boxIndex].transform.GetChild(1).gameObject.GetComponent<Text>().text = guideText[textNumber];
        messageBox[(boxIndex - 1) * -1].transform.GetChild(1).gameObject.GetComponent<Text>().text = guideText[textNumber - 1];
        while ( time <= 1)
        {
            
            messageBox[boxIndex].gameObject.transform.position = Vector3.Lerp(boxLoc[0].transform.position, boxLoc[1].transform.position, time);
            

            messageBox[boxIndex].color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, messageBox[boxIndex].color.a + alphaSpeed);
            messageBox[boxIndex].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0, 0, 0, messageBox[boxIndex].color.a + alphaSpeed);
            messageBox[boxIndex].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0, 0, 0, messageBox[boxIndex].color.a + alphaSpeed);
            

            if (messageBox[boxIndex].color.a >= 1)
            {
                messageBox[boxIndex].color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, 1f);
                
            }
            // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
            messageBox[(boxIndex - 1) * -1].gameObject.transform.position = Vector3.Lerp(boxLoc[1].transform.position, boxLoc[2].transform.position, time);

            messageBox[(boxIndex - 1) * -1].color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, messageBox[(boxIndex - 1) * -1].color.a - alphaSpeed);
            messageBox[(boxIndex - 1) * -1].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(0, 0, 0, messageBox[(boxIndex - 1) * -1].color.a - alphaSpeed);
            messageBox[(boxIndex - 1) * -1].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(0, 0, 0, messageBox[(boxIndex - 1) * -1].color.a - alphaSpeed);

            if (messageBox[(boxIndex - 1) * -1].color.a <= 0)
            {
                messageBox[(boxIndex - 1) * -1].color = new Color(messageBox[(boxIndex - 1) * -1].color.r, messageBox[(boxIndex - 1) * -1].color.g, messageBox[(boxIndex - 1) * -1].color.b, 0f);
            }

            time += Time.deltaTime * 2f;

            
            yield return null;
        }
        messageBox[(boxIndex - 1) * -1].gameObject.transform.position = new Vector3(boxLoc[0].transform.position.x, boxLoc[0].transform.position.y, boxLoc[0].transform.position.z);


        if(textNumber != 1 && textNumber != 5 && textNumber != 6 && textNumber != 13 && textNumber != 14 && textNumber != 15 && textNumber != 17)
        {
            canSkip = true;
        }
        Debug.Log(canSkip);

        yield return null;
    } //리스트에 있는 텍스트 출력 함수
}
