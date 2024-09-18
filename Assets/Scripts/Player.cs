using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator anim;

    public float moveSpeed = 3;
    private Vector2 moveDir = Vector2.zero;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(moveDir.magnitude > 0)
        {
            anim.SetBool("IsWalking", true);
            anim.SetFloat("Horizontal", moveDir.x);
            anim.SetFloat("Vertical", moveDir.y);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
    }   

    private void FixedUpdate()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(xInput, yInput);

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Pickable")
        {
            InventoryManager.Instance.AddToBackpack(collision.GetComponent<Pickable>().type);
            Destroy(collision.gameObject);
        }
    }

    public void ThrowItem(GameObject itemPrefab, int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject go = GameObject.Instantiate(itemPrefab);

            Vector2 direction = Random.insideUnitCircle.normalized * 1.2f;
            go.transform.position = transform.position + new Vector3(direction.x, direction.y, 0);
            go.GetComponent<Rigidbody2D>().AddForce(direction*10);
        }
    }
}
