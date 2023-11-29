using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 과일 소환, 점수 계산, 게임 초기화 등등
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }


    [SerializeField] private GameObject[] fruitPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject spawnParent;

    Fruit currentFruit;

    public FruitType maxfruit;

    bool isActive = true;

    float minX;
    float maxX;

    int score = 0;

    private void Awake()
    {
        maxfruit = FruitType.Cherry;

        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
    }

    private void Start()
    {
        SpawnFruit();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && isActive == true)
        {
            StartCoroutine(DropandSpawn());
        }
    }

    private void DropFruit()
    {
        if (currentFruit != null)
        {
            currentFruit.SetColliderActive(true);
            score += ((int)currentFruit.Type + 1) * 50;
        }

        currentFruit = null;
    }

    private void SpawnFruit()
    {
        currentFruit = Instantiate(fruitPrefabs[Random.Range(0, (int)maxfruit + 1)], spawnPoint.position, Quaternion.identity, spawnParent.transform).GetComponent<Fruit>();
        currentFruit.SetColliderActive(false);
    }

    public void SetFruitPos(Vector3 pos)
    {
        if (currentFruit != null)
        {
            float radius = currentFruit.GetRadius();
            float curX = Mathf.Clamp(pos.x, minX + radius, maxX - radius);
            currentFruit.transform.position = new Vector3(curX, currentFruit.transform.position.y, currentFruit.transform.position.z);
        }
    }

    public void MergeFruit(GameObject fruitA, GameObject furitB)
    {
        FruitType type = fruitA.GetComponent<Fruit>().Type;

        if (type < FruitType.Watermelon)
        {
            maxfruit = (FruitType)Mathf.Max((int)maxfruit, (int)type);

            Vector3 spawnPos = (fruitA.transform.position + furitB.transform.position) / 2;

            Destroy(fruitA);
            Destroy(furitB);

            GameObject newFruit = Instantiate(fruitPrefabs[(int)type + 1], spawnPos, Quaternion.identity, spawnParent.transform);

            score += ((int)type + 2) * 100;
        }
    }

    public int GetScore()
    {
        return score;
    }
    IEnumerator DropandSpawn()
    {
        isActive = false;
        DropFruit();

        yield return new WaitForSeconds(1f);

        isActive = true;
        SpawnFruit();
    }
}
