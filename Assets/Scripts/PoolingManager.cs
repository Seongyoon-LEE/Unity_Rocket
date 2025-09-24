using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    public static PoolingManager p_Instance = null;

    [Header("Asteroid Pool")]
    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] int maxPool = 5;
    [SerializeField] List<GameObject> asteroidPool = new List<GameObject>();

    [Header("Coin Pool")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] int c_maxPool = 10;
    [SerializeField] List<GameObject> coinPool = new List<GameObject>();
    void Awake()
    {
        if (p_Instance == null)
        {
            p_Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (p_Instance != this)
        {
            Destroy(this.gameObject);
        }
        CreateAsteroidPooling();
        CreateCoinPooling();
    }
    void CreateAsteroidPooling()
    {

        GameObject asteroidPools = new GameObject("AsteroidPools");
        DontDestroyOnLoad(asteroidPools);

        for (int i = 0; i < maxPool; i++)
        {
            var asteroid = Instantiate(asteroidPrefab, asteroidPools.transform);
            asteroid.name = $"운석 : {i + 1} 개";
            asteroid.SetActive(false);
            asteroidPool.Add(asteroid);
        }
    }

    void CreateCoinPooling()
    {
        GameObject coinPools = new GameObject("CoinPools");
        DontDestroyOnLoad(coinPools);

        for (int i = 0; i < c_maxPool; i++)
        {
            var coin = Instantiate(coinPrefab, coinPools.transform);
            coin.name = $"코인 : {i + 1} 개";
            coin.SetActive(false);
            coinPool.Add(coin);
        }
    }
    public GameObject GetAsteroid()
    {
        for (int i = 0; i < asteroidPool.Count; i++)
        {
            if (asteroidPool[i].activeSelf == false)
                return asteroidPool[i];
        }
        return null;
    }
    public GameObject GetCoin()
    {
        for (int i = 0; i < coinPool.Count; i++)
        {
            if (coinPool[i].activeSelf == false)
                return coinPool[i];
        }
        return null;
    }
}
