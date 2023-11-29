using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private FruitType type;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private CircleCollider2D col;


    public bool isMerged = false;

    public FruitType Type
    {
        get { return type; }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Fruit") && isMerged == false)
        {
            Fruit f = collision.collider.GetComponent<Fruit>();

            if (f != null)
            {
                if (f.Type == this.Type)
                {
                    f.isMerged = true;
                    this.isMerged = true;

                    GameManager.Instance.MergeFruit(f.gameObject, this.gameObject);
                }
            }
        }
    }

    public void SetColliderActive(bool isActive)
    {
        col.enabled = isActive;
        rigid.gravityScale = isActive ? 1 : 0;
    }

    public float GetRadius()
    {
        return col.radius;
    }

}

public enum FruitType
{
    Cherry,
    Strawberry,
    Grape,
    Orange,
    Persimmon,
    Apple,
    Pear,
    Peach,
    PineApple,
    Melon,
    Watermelon,
    Max
}