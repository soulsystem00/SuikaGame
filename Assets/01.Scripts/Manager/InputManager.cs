using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    bool isTouched = false;
    Vector3 currentPos;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse Down");

            isTouched = true;
            currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //TODO : Fruits Drag
            GameManager.Instance.SetFruitPos(currentPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");

            if (isTouched)
            {
                isTouched = false;


                //TODO : Fruits Drop
            }
        }

    }
}
