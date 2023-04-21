using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int numHearts = 3;
    public TextMeshProUGUI heartsText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Killzone"))
        {
            numHearts--;
            heartsText.text = "x" + numHearts + " heart";
        }
    }
}


