using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteEffectController : MonoBehaviour
{
    [SerializeField] private Transform meteoriteTransform;
    [SerializeField] private float dropTime;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private Rigidbody[] meteoritePieces;
    [SerializeField] private ParticleSystem[] explosionEffects;
    public void Play()
    {
        meteoriteTransform.gameObject.SetActive(true);
        StartCoroutine(DropCor(dropTime));
    }

    private IEnumerator DropCor(float time)
    {
        Vector3 startPos = meteoriteTransform.localPosition;
        for (float t = 0; t < time; t += Time.deltaTime)
        {
            meteoriteTransform.localPosition = Vector3.Lerp(startPos, endPosition, t / time);
            yield return null;
        }
        Explode();
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void Explode()
    {
        foreach (var piece in meteoritePieces)
        {
            piece.transform.SetParent(null);
            piece.useGravity = true;
            piece.isKinematic = false;
            piece.AddExplosionForce(15f, meteoriteTransform.position, 5f, 0, ForceMode.Impulse);
        }
        meteoriteTransform.gameObject.SetActive(false);
        foreach (var effect in explosionEffects)
        {
            effect.Play();
        }
    }
    
}
