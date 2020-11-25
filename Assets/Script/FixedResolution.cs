using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FixedResolution : MonoBehaviour
{
    // 고정해상도를 적용하고 남는 부분(Letterbox)에 사용될 게임오브젝트(Prefab)
    public GameObject m_objBackScissor;


    void Awake()
    {
        // 시작시 한번 실행(게임 실행중에 해상도가 변경되면 다시 호출)
        UpdateResolution();
    }


    void UpdateResolution()
    {
        // 프로젝트 내에 있는 모든 카메라 얻어오기
        Camera[] objCameras = Camera.allCameras;

        // 비율 구하기(16:9 기준)
        //width 9, height 16
        float fResolutionX = Screen.width / 16f;
        float fResolutionY = Screen.height / 9f;

        // X가 Y보가 큰 경우는 화면이 가로로 놓인 경우
        if (fResolutionX > fResolutionY)
        {
            // 종횡비(Aspect Ratio) 구하기
            // 16:9의 경우 1.77:1
            float fValue = (fResolutionX - fResolutionY) * 0.5f;
            fValue = fValue / fResolutionX;

            // 위에서 구한 종횡비를 기준으로 카메라의 뷰포트를 재설정
            // 정규화된 좌표라는걸 잊으면 안됨!
            foreach (Camera obj in objCameras)
            {
                obj.rect = new Rect(((Screen.width * fValue) / Screen.width) + (obj.rect.x * (1.0f - (2.0f * fValue))),
                                    obj.rect.y,
                                    obj.rect.width * (1.0f - (2.0f * fValue)),
                                    obj.rect.height);
            }


            // 왼쪽에 들어갈 레터박스를 생성하고 위치지정
            GameObject objLeftScissor = (GameObject)Instantiate(m_objBackScissor);
            objLeftScissor.GetComponent<Camera>().rect = new Rect(0, 0, (Screen.width * fValue) / Screen.width, 1.0f);

            // 오른쪽 레터박스
            GameObject objRightScissor = (GameObject)Instantiate(m_objBackScissor);
            objRightScissor.GetComponent<Camera>().rect = new Rect((Screen.width - (Screen.width * fValue)) / Screen.width,
                                                                   0,
                                                                   (Screen.width * fValue) / Screen.width,
                                                                   1.0f);


            // 생성된 두 레터박스를 자식으로 추가
            objLeftScissor.transform.parent = gameObject.transform;
            objRightScissor.transform.parent = gameObject.transform;
        }
        // 화면이 세로로 놓은 경우도 동일한 과정을 거침
        else if (fResolutionX < fResolutionY)
        {
            float fValue = (fResolutionY - fResolutionX) * 0.5f;
            fValue = fValue / fResolutionY;

            foreach (Camera obj in objCameras)
            {
                obj.rect = new Rect(obj.rect.x,
                                    ((Screen.height * fValue) / Screen.height) + (obj.rect.y * (1.0f - (2.0f * fValue))),
                                    obj.rect.width,
                                    obj.rect.height * (1.0f - (2.0f * fValue)));

                //obj.rect = new Rect( obj.rect.x , obj.rect.y + obj.rect.y * fValue, obj.rect.width, obj.rect.height - obj.rect.height * fValue );
            }


            GameObject objTopScissor = (GameObject)Instantiate(m_objBackScissor);
            objTopScissor.GetComponent<Camera>().rect = new Rect(0, 0, 1.0f, (Screen.height * fValue) / Screen.height);

            GameObject objBottomScissor = (GameObject)Instantiate(m_objBackScissor);
            objBottomScissor.GetComponent<Camera>().rect = new Rect(0, (Screen.height - (Screen.height * fValue)) / Screen.height
                                                    , 1.0f, (Screen.height * fValue) / Screen.height);


            objTopScissor.transform.parent = gameObject.transform;
            objBottomScissor.transform.parent = gameObject.transform;
        }
        else
        {
            // Do Not Setting Camera
        }
    }
}
