using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public bool isLive;

    // 게임 시작 시: 0, 보스 소환 준비: 1, 전투 중: 2, 보스전 종료: 3 
    public int bossState = 0;


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
    public int scholarship = 0;
    public float DamageMul = 1;
    public float StaticDamage = 0;
    public int additionalCount = 0;
    public float shotSpeed = 0;
    public float KnockBack = 0;
    public float rate = 0;
    public float restoration = 0;
    public int SecCounter = 0;



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

    public void GameStart(int id) 
    {
        playerId = id;
        health = maxHealth;

        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2); 
        Resume();
        SecCounter = 0;

        AudioManager.instance.PlayBgm(1, true);
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

        AudioManager.instance.PlayBgm(1, false);
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

        AudioManager.instance.PlayBgm(1, false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }
    public void GameRetry()
    {
        SceneManager.LoadScene("Scenes/FinalScenes");
        //�ȿ� �̸� �Ǵ� scene index
        //File -> Build Setting���� Ȯ�ΰ���
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

            if (bossState == 0)
            {
                bossState = 1;
            }
            if (bossState != 3)
                return;
            
            GameVictory();
        }
        if (gameTime > SecCounter)
        {
            if(maxHealth > health + restoration)
            {
                health += restoration;
            }
            else if (maxHealth <= health + restoration)
            {
                health = maxHealth;
            }

            SecCounter++;
        }
    }
    public void GetExp(int amount)
    {
        if (!isLive)
            return;

        for(int index=0; index<amount; index++)
        {
            exp++;
        }

        if (exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            exp -= nextExp[Mathf.Min(level, nextExp.Length - 1)];
            level++;
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

    //
    public void Damaged(float damage)
    {
        health -= damage * Time.deltaTime;
    }

    // EU  ------------------------------------------------
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
    public void AddStaticDamage(float amount)
    {
        StaticDamage += amount;
    }
    public void AddShotSpeed(float amount)
    {
        shotSpeed += amount;
    }
    public void AddKnockBack(float amount)
    {
        KnockBack += amount;
    }
    public void AddRate(float amount)
    {
        rate += amount;
    }
    public void AddRestoration(float amount)
    {
        restoration += amount;
    }

}
