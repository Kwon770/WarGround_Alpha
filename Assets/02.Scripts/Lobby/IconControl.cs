using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconControl : MonoBehaviour {

    [SerializeField] GameObject Pos;
    
    [SerializeField] float Speed;
    [SerializeField] AnimationCurve curve;

    [SerializeField] TeamStory Story;

    public GameObject Empty_Char;
    public GameObject Panel_Char;
    public GameObject Menu;
    public GameObject PanelPos;



    public void PanelMove1(int teamIndex)
    {
        Story.Story(teamIndex);

        for(int i = 0; i < 6; i++)
        {
            Empty_Char.transform.GetChild(i).gameObject.SetActive(false);
        }
        Empty_Char.transform.GetChild(teamIndex).gameObject.SetActive(true);

        StartCoroutine(PanelMove2());
    }

    IEnumerator PanelMove2()
    {
        Manager.instance.corutine = true;
        Manager.instance.scene = (int)Manager.Menunum.Description;

        Vector3 startPos = PanelPos.transform.position;
        Vector3 endPos = Menu.transform.position;
        float time = 0;

        while (time <= 1)
        {
            Panel_Char.transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(time));
            time += Time.deltaTime * Speed;
            yield return null;
        }

        Manager.instance.corutine = false;
    }
}
