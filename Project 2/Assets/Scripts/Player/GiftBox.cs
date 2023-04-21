using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox : MonoBehaviour
{
    public float itemDisplayTime = 10f;
    public float fallSpeed = 3f;
    public GameObject[] items;
    private bool isOpen = false;

    void Start()
    {
        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        float randomX = Random.Range(-cameraWidth, cameraWidth);
        transform.position = new Vector2(randomX, cameraHeight);
    }

    void Update()
    {
        if (!isOpen)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isOpen)
        {
            OpenBox();
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            fallSpeed = 0f;

            transform.position = new Vector3(transform.position.x, other.transform.position.y + other.collider.bounds.extents.y + GetComponent<SpriteRenderer>().bounds.extents.y, transform.position.z);
        }
    }

    void OpenBox()
    {
        isOpen = true;

        GetComponent<SpriteRenderer>().enabled = false;

        int randomIndex = Random.Range(0, items.Length);
        GameObject randomItem = items[randomIndex];

        Instantiate(randomItem, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
