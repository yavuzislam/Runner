using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerControl : MonoBehaviour
{
    public UI_Control control;
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
        if (control.gameStart)
        {
            gameManager.AnimStart();
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Collectable")
        {
            if (Collectables.Count < 6)
            {
                Collectables.Add(other.gameObject);
                other.transform.position = transform.position + new Vector3(0f, 0.150f, -0.5f) + new Vector3(0f, 0.3f, 0f) * Collectables.Count;
                top = Collectables.Count;
            }
            else if (Collectables.Count < 12)
            {
                Collectables.Add(other.gameObject);
                other.transform.position = transform.position + new Vector3(-0.3f, 0.150f, -0.5f) + new Vector3(0f, 0.3f, 0f) * (Collectables.Count - top);

            }
            else if (Collectables.Count >= 12 && Collectables.Count < 18)
            {
                Collectables.Add(other.gameObject);
                other.transform.position = transform.position + new Vector3(-0.6f, 0.150f, -0.5f) + new Vector3(0f, 0.3f, 0f) * (Collectables.Count - 12);
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
            Collectables.Clear();
        }
        else if (other.tag == "Up")
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        else if (other.tag == "Finish")
        {
            if (transform.childCount != 2)
            {
                gameManager.anim.enabled = false;
                control.gameStart = false;
                gameManager.Win();
            }
            else
            {
                gameManager.Lose();
            }
        }

    }
}
