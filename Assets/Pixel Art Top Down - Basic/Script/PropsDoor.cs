using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDoorTransition : MonoBehaviour
{
    public Sprite doorClosed; // Sprite de la porte fermée
    public Sprite doorOpened; // Sprite de la porte ouverte
    public float transitionSpeed = 0.5f; // Vitesse de transition

    private SpriteRenderer spriteRenderer;
    private bool isOpening = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpening)
        {
            isOpening = true;
            StartCoroutine(TransitionDoor());
        }
    }

    private IEnumerator TransitionDoor()
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * transitionSpeed;
            spriteRenderer.sprite = t < 0.5f ? doorClosed : doorOpened; // Transition progressive
            yield return null;
        }
        isOpening = false;
    }
}