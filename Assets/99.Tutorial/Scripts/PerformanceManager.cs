using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceManager : MonoBehaviour {

    [SerializeField] Text presentsText;
    [SerializeField] float alphaSpeed;
    [SerializeField] Text[] textList = new Text[12];
    [SerializeField] GameObject cam;
    [SerializeField] Camera mainCam;
    //[SerializeField] Quaternion startRot; 지울것
    [SerializeField] Animator anim;

    [SerializeField] Coroutine openingCoroutine; //오프닝 코루틴 멈추기

    [SerializeField] GameObject unit;
    [SerializeField] GameObject scriptManager;
    [SerializeField] GameObject openingScript;
    [SerializeField] bool opening;

    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ 여기서부턴 본게임
   

    private void Start()
    {
        openingCoroutine = StartCoroutine(PerformanceSequence());

        opening = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))       //튜토리얼 종료용
        {
            if(opening == true)
            {
                anim.SetBool("openingCam", false);
                StopCoroutine(openingCoroutine);
                tutorialStart();

                
                opening = false;
            }
            
        }

       
    }

    IEnumerator PerformanceSequence()
    {
        int i = 0;

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(TextAlphaCaculator(true, textList[i])); // 0 START
        
        yield return new WaitForSeconds(3.5f);
        StartCoroutine(TextAlphaCaculator(false, textList[i])); // 0 END
        i++;
        anim.SetBool("openingCam", true);

        yield return new WaitForSeconds(3.5f);
        StartCoroutine(TextAlphaCaculator(true, textList[i])); //1 START
        i++;
        StartCoroutine(TextAlphaCaculator(true, textList[i])); //2 START

        yield return new WaitForSeconds(2.0f);
        StartCoroutine(TextAlphaCaculator(false, textList[i-1])); //1 END
        StartCoroutine(TextAlphaCaculator(false, textList[i]));   //2 END
        i++;

        yield return new WaitForSeconds(3.0f);
        StartCoroutine(TextAlphaCaculator(true, textList[i])); //3 START
        i++;
        StartCoroutine(TextAlphaCaculator(true, textList[i])); //4 START

        yield return new WaitForSeconds(3.0f);
        StartCoroutine(TextAlphaCaculator(false, textList[i - 1])); //3 END
        StartCoroutine(TextAlphaCaculator(false, textList[i]));   //4 END
        i++;

        yield return new WaitForSeconds(3.0f);
        StartCoroutine(TextAlphaCaculator(true, textList[i])); //5 START
        i++;
        StartCoroutine(TextAlphaCaculator(true, textList[i])); //6 START

        yield return new WaitForSeconds(3.0f);
        StartCoroutine(TextAlphaCaculator(false, textList[i - 1])); //5 END
        StartCoroutine(TextAlphaCaculator(false, textList[i]));   //6 END
        i++;

        yield return null;
    }   //오프닝 시퀀스

    

    IEnumerator TextAlphaCaculator(bool alphaMax, Text textAlpha)
    {
        if (alphaMax == true)
        {

            textAlpha.gameObject.SetActive(true);
            textAlpha.color = new Color(textAlpha.color.r, textAlpha.color.g, textAlpha.color.b, 0f);

            while (textAlpha.color.a < 1)
            {
                textAlpha.color = new Color(textAlpha.color.r, textAlpha.color.g, textAlpha.color.b, textAlpha.color.a + alphaSpeed);

                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            while (textAlpha.color.a > 0)
            {
                textAlpha.color = new Color(textAlpha.color.r, textAlpha.color.g, textAlpha.color.b, textAlpha.color.a - alphaSpeed);

                yield return new WaitForSeconds(0.05f);
            }
            textAlpha.gameObject.SetActive(false);
        }

        yield return null;
    }

    void tutorialStart()
    {
        cam.GetComponent<CameraManager>().opening = false;

        Destroy(mainCam.GetComponent<Animator>());

        openingScript.SetActive(false);
        
        unit.SetActive(true);

        scriptManager.SetActive(true);


    }

    // ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ 인게임입니다


}



