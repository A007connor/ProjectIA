using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door: MonoBehaviour
{
    public Sprite doorClosed; // Sprite de la porte fermée
    public Sprite doorOpened; // Sprite de la porte ouverte
    public float transitionDuration = 0.5f; // Vitesse de transition
    public GameObject doorCollider; // Référence au collider de la porte

    private SpriteRenderer spriteRenderer;
    private bool isOpening = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpening)
        {
            isOpening = true;
            StartCoroutine(TransitionDoor());
            StartCoroutine(DisableColliderAfterDelay());
        }
    }

    private IEnumerator TransitionDoor()
    {
        float timer = 0f;
        Color startColor = spriteRenderer.color;
        Color endColor = doorOpened != null ? spriteRenderer.color : startColor; // Utilise la couleur de la porte ouverte s'il y en a une

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / transitionDuration);

            // Interpolation linéaire entre les couleurs pour une transition fluide
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        isOpening = false;

    }
    private IEnumerator DisableColliderAfterDelay()
    {
        yield return new WaitForSeconds(transitionDuration);
        doorCollider.SetActive(false); // Désactiver le collider de la porte
    }
}
