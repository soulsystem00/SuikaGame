using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"{GameManager.Instance.GetScore()}";
    }
}
