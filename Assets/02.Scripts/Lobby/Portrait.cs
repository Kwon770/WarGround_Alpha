using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Portrait : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{ 

    public string grade;
    public string troop;
    public string name;
    public string cost;
    public string hp;
    public string atk;
    public string shd;
    public string skillName;
    public string skillDescription;

    public Text Grade;
    public Text Troop;
    public Text Name;
    public GameObject Text1;
    public Text Cost;
    public GameObject Text2;
    public Text Hp;
    public GameObject Text3;
    public Text Atk;
    public GameObject Text4;
    public Text Shd;
    public Image SkillImage;
    public Text SkillName;
    public Text SkillDescription;

    public GameObject Story;

    public void OnPointerEnter (PointerEventData eventData)
    {
        Story.SetActive(false);

        Grade.gameObject.SetActive(true);
        Troop.gameObject.SetActive(true);
        Name.gameObject.SetActive(true);
        Text1.SetActive(true);
        Cost.gameObject.SetActive(true);
        Text2.SetActive(true);
        Hp.gameObject.SetActive(true);
        Text3.SetActive(true);
        Atk.gameObject.SetActive(true);
        Text4.SetActive(true);
        Shd.gameObject.SetActive(true);
        SkillImage.gameObject.SetActive(true);
        SkillName.gameObject.SetActive(true);
        SkillDescription.gameObject.SetActive(true);


        Grade.text = grade;
        Troop.text = troop;
        Name.text = name;
        Cost.text = cost;
        Hp.text = hp;
        Atk.text = atk;
        Shd.text = shd;
        SkillName.text = skillName;
        SkillDescription.text = skillDescription;
    }

    public void OnPointerExit (PointerEventData eventData)
    {
        Story.SetActive(true);

        Grade.gameObject.SetActive(false);
        Troop.gameObject.SetActive(false);
        Name.gameObject.SetActive(false);
        Text1.SetActive(false);
        Cost.gameObject.SetActive(false);
        Text2.SetActive(false);
        Hp.gameObject.SetActive(false);
        Text3.SetActive(false);
        Atk.gameObject.SetActive(false);
        Text4.SetActive(false);
        Shd.gameObject.SetActive(false);
        SkillImage.gameObject.SetActive(false);
        SkillName.gameObject.SetActive(false);
        SkillDescription.gameObject.SetActive(false);
    }
}
