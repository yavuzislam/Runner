using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerControl : MonoBehaviour
{
    public GameManager gameManager;
    Rigidbody rb;
    public List<GameObject> Collectables;
    float speed = 2f, horizontal;
    int top;
    public int collec;
    public bool set;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        gameManager.AnimStart();
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
        horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * 8f * horizontal * Time.fixedDeltaTime);
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -4.5f, 4.5f);
        transform.position = pos;
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //        rb.AddForce(Vector3.up * 5f,ForceMode.Impulse);  
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            if (Collectables.Count < 4)
            {
                Collectables.Add(other.gameObject);
                other.transform.position = transform.position + new Vector3(0f, 0.150f, -0.5f) + new Vector3(0f, 0.3f, 0f) * Collectables.Count;
                top = Collectables.Count;
            }
            else if (Collectables.Count < 8)
            {
                Collectables.Add(other.gameObject);
                other.transform.position = transform.position + new Vector3(-0.3f, 0.150f, -0.5f) + new Vector3(0f, 0.3f, 0f) * (Collectables.Count - top);

            }
            else if (Collectables.Count >= 8 && Collectables.Count < 12)
            {
                Collectables.Add(other.gameObject);
                other.transform.position = transform.position + new Vector3(-0.6f, 0.150f, -0.5f) + new Vector3(0f, 0.3f, 0f) * (Collectables.Count - 8);
                top = Collectables.Count;
            }
            other.transform.SetParent(transform);
        }
        else if (other.tag == "Obstacle")
        {
            if (transform.childCount != 2)
            {
                foreach (var item in Collectables)
                {
                    Destroy(item.gameObject);
                    break;
                }
            }
            else
            {
                gameManager.Lose();
            }
        }
        else if (other.tag == "ObstacleAll")
        {
            if (transform.childCount != 2)
            {
                foreach (var item in Collectables)
                {
                    Destroy(item.gameObject);
                }
            }
            else
            {
                gameManager.Lose();
            }
        }
        else if (other.tag == "Up")
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        else if (other.tag == "Finish")
        {
            if (transform.childCount != 2)
            {
                gameManager.Win();
            }
            else
            {
                gameManager.Lose();
            }
        }

    }
}
