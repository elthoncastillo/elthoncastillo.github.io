using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{

    //this is script is for prevent mouse-clicking outside the UI
    //Code inspared by this: https://www.youtube.com/watch?v=QL6LOX5or84

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (!IsPointerOverUIObject())
                {
                    Debug.Log("The UI buttons are failing");
                }
            }
        }

    }
    private bool IsPointerOverUIObject()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


}