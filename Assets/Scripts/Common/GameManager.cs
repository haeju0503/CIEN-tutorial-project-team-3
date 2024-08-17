using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public bool isLive;

    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    [Header("# Player Info")]
    public int playerId;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 1, 1, 1, 2, 2, 2, 3, 3, 3, 4, 5, 10 };
    public float health;
    public float maxHealth = 100;

    [Header("# EU Upgrade")]
    public int scholarship = 30000;
    public float DamageMul = 1;
    public float StaticDamage = 0;
    public int additionalCount = 0;
    public float shotSpeed = 0;
    public float KnockBack = 0;
    public float rate = 0;
    

    private void Awake()
    {
        instance = this;
        isLive = false;
        if (!PlayerPrefs.HasKey("Scholarship"))
        {
            scholarship = 50;
            PlayerPrefs.SetInt("Scholarship", scholarship);
        }
        else
        {
            scholarship = PlayerPrefs.GetInt("Scholarship");
        }
    }
    public void TestDragon(int i) //잘 작동하는지 확인하는 테스트용 함수 (삭제해도 상관X)
    {
        Debug.Log(i);
    }
    public void GameStart(int id) 
    {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2); //임시 스크립트 % 2는 전체 무기 수
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene("Scenes/FinalScenes");
        //안에 이름 또는 scene index
        //File -> Build Setting에서 확인가능
    }
    public void GameQuit()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }
    public void GetExp()
    {
        if (!isLive)
            return;

        exp++;

        if (exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }
    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

    public int GetScholarship()
    {
        return scholarship;
    }
    public void AddScholarship(int amount)
    {
        scholarship += amount;
    }
    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
    }
    public void AddCount(int amount)
    {
        additionalCount += amount;
    }
    public void AddDamageMul(float amount)
    {
        DamageMul += amount;
    }


}
