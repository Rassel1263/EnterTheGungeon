using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public MouseController mouse;
    public MouseController UImouse;

    public int score;

    private static GameManager instance;

    [SerializeField] Text scoreText;

    [SerializeField] GameObject startFade;

    [SerializeField] GameObject gameOver;
    [SerializeField] Text[] gameOverText;
    [SerializeField] Button[] gameOverButton;

    [SerializeField] GameObject enemySpawner;
    int spawnIndex;
    float spawnTime;

    public static GameManager Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (instance == null)
                    Debug.Log("no Singleton obj");
            }

            return instance;
        }

    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        spawnTime = 3.0f;
        spawnIndex = 0;
        score = 0;

        GetComponent<AudioSource>().Play();
        startFade.SetActive(true);

        StartCoroutine(FadeIn());
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        string scoreStr = score.ToString();
        scoreText.text = scoreStr.PadLeft(6, '0');
    }
    public void DrawGameOver()
    {
        StartCoroutine(FillGameOver());

        if (PlayerPrefs.GetInt("BestScore", 0) < score)
            PlayerPrefs.SetInt("BestScore", score);

        string bestScore = PlayerPrefs.GetInt("BestScore").ToString();
        string curScore = score.ToString();

        gameOverText[1].text = "BestScore : " + bestScore.PadLeft(6, '0');
        gameOverText[2].text = "YourScore : " + curScore.PadLeft(6, '0');

        mouse.gameObject.SetActive(false);

        UImouse.gameObject.SetActive(true);
        UImouse.SetMouse();
    }

    public IEnumerator EnemySpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            GameObject enemy = Instantiate(enemySpawner);
            enemy.transform.position = new Vector2(Random.Range(-300, 301), Random.Range(-300, 301));
            spawnIndex++;

            if(spawnIndex % 5 == 0)
            {
                spawnIndex = 0;

                if(spawnTime >= 1.0f)
                    spawnTime -= 0.5f;
            }
        }
    }

    public IEnumerator FillGameOver()
    {
        float alpha = 0.0f;
       

        gameOver.SetActive(true);
        while (alpha < 1.0f)
        {
            alpha += Time.unscaledDeltaTime / 1.0f;
            foreach (Text tex in gameOverText)
                tex.color = new Color(255, 255, 255, alpha);

            foreach (Button btn in gameOverButton)
                btn.image.color = new Color(255, 255, 255, alpha);
                

            gameOver.GetComponent<Image>().color = new Color(255, 255, 255, alpha);

            yield return null;
        }

    }

    public IEnumerator FadeIn()
    {
        float alpha = 1.0f;
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime / 1.0f;

            startFade.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        startFade.SetActive(false);
    }

    public void OnButton(int index)
    {
         SceneManager.LoadScene(index);
    }
}
