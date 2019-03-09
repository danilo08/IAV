using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace p1
{

    public class CameraManager : MonoBehaviour
    {

        private static readonly string MOUSE_SCROLLWHEEL = "Mouse ScrollWheel";
        private static readonly float ZOOM_SPEED = 5.0f;

        // Actualiza la posición de la cámara en función de cómo esté la rueda del ratón
        void Update()
        {
            float scroll = Input.GetAxis(MOUSE_SCROLLWHEEL);
            transform.Translate(0, 0, scroll * ZOOM_SPEED, Space.Self);
        }
    }

}