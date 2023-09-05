using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected float horizontal;
    [SerializeField] protected float speed;
    protected Rigidbody2D _rb;


    [SerializeField]private float jumpForce;
    [SerializeField] private float pushingForce;

    [SerializeField] protected KeyCode left;
    [SerializeField] protected KeyCode right;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        Jumping();
        Pushing();
    }
    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {

    }

    protected void Moving()
    {
        if (Input.GetKeyDown(left))
        {
            horizontal = -1;
        }
        if (Input.GetKeyDown(right))
        {
            horizontal = 1;
        }
        if (Input.GetKeyUp(left) || Input.GetKeyUp(right))
        {
            horizontal = 0;
        }

        Debug.Log(horizontal);
        _rb.velocity = new Vector2(speed * horizontal, _rb.velocity.y);
    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }
    void Pushing()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _rb.AddForce(Vector2.up * pushingForce, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Object")
        {
            collision.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        if (collision.CompareTag("Collectible"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Object")
        {
            collision.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Collectible")
        {
            Destroy(collision.gameObject);
        }
        if (collision.collider.tag == "Object")
        {
            collision.collider.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Object")
        {
            collision.collider.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }







}
