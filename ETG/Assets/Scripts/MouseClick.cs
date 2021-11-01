using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseClick : MonoBehaviour
{
    // Start is called before the first frame update

    public GraphicRaycaster gr;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
            //PointerEventData ped = new PointerEventData(null);
            //ped.position = Input.mousePosition;
            //List<RaycastResult> results = new List<RaycastResult>();
            //gr.Raycast(ped, results);

            //if (results.Count != 0)
            //{
            //    GameObject obj = results[0].gameObject;

            //    if (obj.CompareTag("Button"))
            //        Debug.Log("Asd");
            //}

            ////Vector2 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ////Ray2D ray = new Ray2D(wp, Vector2.zero);
            ////RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            ////if(hit.collider != null)
            ////{
            ////    Button btn = hit.collider.gameObject.GetComponent<Button>();

            ////    btn.onClick.Invoke();
            ////}

    }
}
