using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject help;
    public GameObject fade;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

        GetComponent<AudioSource>().Play();

        StartCoroutine(FadeIn());   
    }

    // Update is called once per frame
    void Update()
    {
        if (help.activeInHierarchy && Input.GetMouseButtonDown(0))
        {
            help.SetActive(false);
        }
    }

    public void OnButton(int index)
    {
        if (index == 0)
            StartCoroutine(FadeOut(ChangeScene));
        else if (index == 1)
            help.SetActive(true);
        else if(index == 2)
            StartCoroutine(FadeOut(Application.Quit));
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OnHelp()
    {
        help.SetActive(true);
    }

    IEnumerator FadeIn()
    {
        float alpha = 1.0f;
        while(alpha > 0.0f)
        {
            alpha -= Time.deltaTime / 1.0f;

            fade.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fade.SetActive(false);
    }

    IEnumerator FadeOut(System.Action action)
    {
        float alpha = 0.0f;
        fade.SetActive(true);

        while (alpha < 1.0f)
        {
            alpha += Time.deltaTime / 1.0f;

            fade.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        action();
    }
}
