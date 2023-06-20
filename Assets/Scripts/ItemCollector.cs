using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int melons = 0;

    [SerializeField] private TextMeshProUGUI melonsText;

    [SerializeField] private AudioSource collectSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Melon"))
        {
            Destroy(collision.gameObject);
            melons++;
            melonsText.text = "Melons:" + melons;
            collectSound.Play();
        }
    }
}
