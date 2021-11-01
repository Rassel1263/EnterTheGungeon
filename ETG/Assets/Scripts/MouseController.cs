using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    public Vector2 mousePos;

    private Canvas canvas = null;
    public RectTransform canvasRect = null;

    [SerializeField]
    int index;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;   
    }

    // Update is called once per frame
    void Update()
    {
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.position = mousePos;
        SetMouse();
    }

    public void SetMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (index == 0)
        {
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(Input.mousePosition);
            Vector2 WorldObject_CanvasPosition = new Vector2(
            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

            transform.position = WorldObject_CanvasPosition + new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        }
        else
        {
            transform.position = mousePos;
        }
    }
}
