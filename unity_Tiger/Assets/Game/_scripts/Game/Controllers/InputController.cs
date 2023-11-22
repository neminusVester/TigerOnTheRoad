using UnityEngine;
using UnityEngine.UI;


public class InputController : MonoBehaviour
{
    private void Update()
    {
            if (Input.GetMouseButtonDown(0))
            {
                GameEvents.Instance.PlayerHoldongFinger(true);
            }
            if (Input.GetMouseButtonUp(0))
            {
                GameEvents.Instance.PlayerHoldongFinger(false);
            }

            /*  if (Input.touchCount > 0)
             {
                 Touch touch = Input.GetTouch(0);

                 if (touch.phase == TouchPhase.Began)
                 {
                     GameEvents.Instance.PlayerHoldongFinger(true);
                 }
                 else if (touch.phase == TouchPhase.Ended)
                 {
                     GameEvents.Instance.PlayerHoldongFinger(false);
                 }
             } */
    }
}
