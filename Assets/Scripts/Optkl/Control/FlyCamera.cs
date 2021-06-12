using System;
using UnityEngine;
using UnityEngine.UI;
using Optkl.Control;

namespace Optkl.Control
{
    public class FlyCamera : MonoBehaviour
    {
        private Camera cam;
        private Vector3 mouseOrigin = Vector3.zero;
        [SerializeField]
        [Range(5000, 20000)]
        private float KeyboardZoomSpeed;
        [SerializeField]
        [Range(300, 2000)]        
        private float MouseZoomSpeed = 500f;
        [SerializeField]
        [Range(100, 1000)]   
        private float panSpeed = 250;
        private Boolean isPanning = false;
        private float totalRun = 1.0f;
        // private Slider historySlider;

        void Awake()
        {
            cam = GetComponent<Camera>();
        }

        void Update()
        {
            Vector3 p = GetBaseInput();
            if (p.sqrMagnitude > 0)
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * 20;
            }
            cam.gameObject.transform.Translate(p);
            if (Input.GetKey(KeyCode.LeftBracket))
            {
                cam.orthographicSize -= Time.deltaTime * 8000;
            }
            if (Input.GetKey(KeyCode.RightBracket))
            {
                cam.orthographicSize += Time.deltaTime * 8000;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                // if (historySlider.IsActive())
                // {
                //     historySlider.value++;
                // }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                // if (historySlider.IsActive())
                // {
                //     historySlider.value--;
                // }
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            Zoom(scroll, MouseZoomSpeed);

            if (Input.GetMouseButtonDown(1))
            {
                mouseOrigin = Input.mousePosition;
                isPanning = true;
            }
            // cancel on button release
            if (!Input.GetMouseButton(1))
                isPanning = false;
            //move camera on X & Y
            if (isPanning)
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
                Vector3 move = new Vector3(-pos.x * panSpeed, -pos.y * panSpeed, 0);
                cam.transform.Translate(move);
            }
        }

        void Zoom(float deltaMagnitudeDiff, float speed)
        {
            cam.orthographicSize += deltaMagnitudeDiff * speed;
        }

        private Vector3 GetBaseInput()
        {
            Vector3 p_Velocity = new Vector3();
            if (Input.GetKey(KeyCode.Comma) || Input.GetKey(KeyCode.W))
            {
                p_Velocity += new Vector3(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.O) || Input.GetKey(KeyCode.S))
            {
                p_Velocity += new Vector3(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                p_Velocity += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.D))
            {
                p_Velocity += new Vector3(1, 0, 0);
            }
            return p_Velocity;
        }
    }
}
