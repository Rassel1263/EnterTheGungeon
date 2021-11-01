using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    [SerializeField] GameObject heartContainer;
    [SerializeField] GameObject heartPrefab;

    [SerializeField] GameObject hitEffect;

    [SerializeField] GameObject gameOver;
    [SerializeField] Text[] gameOverTxts;
    [SerializeField] Button[] gameOverButtons;

    public void DrawHp(int hp, int maxHp)
    {
        foreach (Transform child in heartContainer.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < maxHp; ++i)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer.transform.position, Quaternion.identity);

            if (i + 1 <= hp)
            {
                heart.transform.SetParent(heartContainer.transform);
                heart.transform.localScale = new Vector2(1, 1);
                heart.GetComponent<Image>().sprite = fullHeart;
            }
            else
            {
                heart.transform.SetParent(heartContainer.transform);
                heart.transform.localScale = new Vector2(1, 1);
                heart.GetComponent<Image>().sprite = emptyHeart;
            }
        }
    }

    public void DrawHit()
    {
        hitEffect.SetActive(true);

        StartCoroutine(HitUpdate());
    }

    IEnumerator HitUpdate()
    {
        float alpha = 1.0f;
        while(alpha > 0.0f)
        {
            alpha -= 0.02f;
            yield return null;
            hitEffect.GetComponent<RawImage>().color = new Color(255, 255, 255, alpha);
        }
    }
  
    public IEnumerator DrawGameOver()
    {
        float alpha = 0.0f;
        gameOver.SetActive(true);
        while (alpha < 1.0f)
        {
            alpha += Time.unscaledDeltaTime / 1.0f;
            gameOver.GetComponent<Image>().color = new Color(255, 255, 255, alpha);

            yield return null;
        }
    }
}
