using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    public Material flashMaterial;
    public float flashDuration = 0.1f;

    private Material originalMaterial;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        if (rend != null)
        {
            originalMaterial = rend.material;
        }
    }

    public void Flash()
    {
        if (rend != null && flashMaterial != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashCoroutine());
        }
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        rend.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        rend.material = originalMaterial;
    }
}
