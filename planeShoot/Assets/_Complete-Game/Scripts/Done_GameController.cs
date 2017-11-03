using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;//敌人prefabs数组
    public Vector3 spawnValues; //敌人创建的基础坐标
    public int hazardCount;     //一波创建的敌人总数
    public float spawnWait;     //创建陨石和敌人的间隔时间
    public float startWait;     //开始游戏的等待时间
    public float waveWait;      //到下一波石头和敌人创建的间隔时间

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;      //gameOver和restart表示游戏进行的状态，可以设置成状态机
    private bool restart;
    private int score;          //得分

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());   // 协程运行SpawnWaves函数
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   // 重新加载场景
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait); //等待startWait时间
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)   // 游戏结束，设置restart为true，跳出while循环
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue) // 增加分数
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore() // 更新分数显示
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()  //游戏结束接口
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}