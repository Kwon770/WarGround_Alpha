using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptManager : MonoBehaviour {

    [SerializeField] GameObject[] boxLoc;
    [SerializeField] Image[] messageBox;
    [SerializeField] bool canSkip;
    [SerializeField] int boxIndex;
    [SerializeField] int scriptIndex;
    [SerializeField] float alphaSpeed;

    private void Start()
    {
        messageBox[boxIndex].color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, 0f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && canSkip == true)
        {
            
            StartCoroutine(MessagePrint(boxIndex));
            boxIndex = (boxIndex - 1) * -1;
        }
    }

    IEnumerator MessagePrint(int boxIndex)
    {
        canSkip = false;
        float time = 0;

        while ( time <= 1)
        {
            
            messageBox[boxIndex].gameObject.transform.position = Vector3.Lerp(boxLoc[0].transform.position, boxLoc[1].transform.position, time);
            

            messageBox[boxIndex].color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, messageBox[boxIndex].color.a + alphaSpeed);
            messageBox[boxIndex].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, messageBox[boxIndex].color.a + alphaSpeed);
            messageBox[boxIndex].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, messageBox[boxIndex].color.a + alphaSpeed);
            if (messageBox[boxIndex].color.a >= 1)
            {
                messageBox[boxIndex].color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, 1f);
                messageBox[boxIndex].color = new Color(messageBox[boxIndex].color.r, messageBox[boxIndex].color.g, messageBox[boxIndex].color.b, 1f);
            }
            // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
            messageBox[(boxIndex - 1) * -1].gameObject.transform.position = Vector3.Lerp(boxLoc[1].transform.position, boxLoc[2].transform.position, time);

            messageBox[(boxIndex - 1) * -1].color = new Color(messageBox[(boxIndex - 1) * -1].color.r, messageBox[(boxIndex - 1) * -1].color.g, messageBox[(boxIndex - 1) * -1].color.b, messageBox[(boxIndex - 1) * -1].color.a - alphaSpeed);
            messageBox[(boxIndex - 1) * -1].transform.GetChild(0).gameObject.GetComponent<Text>().color = new Color(messageBox[(boxIndex - 1) * -1].color.r, messageBox[(boxIndex - 1) * -1].color.g, messageBox[(boxIndex - 1) * -1].color.b, messageBox[(boxIndex - 1) * -1].color.a + alphaSpeed);
            messageBox[(boxIndex - 1) * -1].transform.GetChild(1).gameObject.GetComponent<Text>().color = new Color(messageBox[(boxIndex - 1) * -1].color.r, messageBox[(boxIndex - 1) * -1].color.g, messageBox[(boxIndex - 1) * -1].color.b, messageBox[(boxIndex - 1) * -1].color.a + alphaSpeed);
            if (messageBox[(boxIndex - 1) * -1].color.a <= 0)
            {
                messageBox[(boxIndex - 1) * -1].color = new Color(messageBox[(boxIndex - 1) * -1].color.r, messageBox[(boxIndex - 1) * -1].color.g, messageBox[(boxIndex - 1) * -1].color.b, 0f);
            }

            time += Time.deltaTime * 2f;
            yield return null;
        }
        messageBox[(boxIndex - 1) * -1].gameObject.transform.position = new Vector3(boxLoc[0].transform.position.x, boxLoc[0].transform.position.y, boxLoc[0].transform.position.z);
        canSkip = true;
        yield return null;
    }
}
