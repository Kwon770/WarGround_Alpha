using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour {

    public Camera mainCamera;
    public GameObject OverlayUi;
    public Text Attack;
    public Text Defense;
    public Text Health;

    UnitInfo unitInfo;


    void Update ()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        rayCasting(ray);
	}

    void rayCasting(Ray ray)
    {
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // 유닛이 레이에 맞았을시
            if(hit.transform.tag == "Unit")
            {
                // 오브젝트 켜고
                OverlayUi.SetActive(true);

                // 위치 동기화

                // 정보 할당
                unitInfo = hit.transform.GetComponent<UnitInfo>();
                Attack.text = (unitInfo.ATK + unitInfo.AddATK).ToString();
                Defense.text = unitInfo.SHD.ToString();
                Health.text = unitInfo.HP.ToString();
            }
            else OverlayUi.SetActive(false);
        }
        else OverlayUi.SetActive(false);
    }
}
