using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEffectController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] bricks;
    [SerializeField] private ParticleSystem dustExpl;

    public void PlayEffect(Vector3 playerPos)
    {
        foreach (var brick in bricks)
        {
            brick.useGravity = true;
            brick.isKinematic = false;
            brick.AddExplosionForce(15f, playerPos + Vector3.up * 2f, 10f, 0, ForceMode.Impulse);
            
        }
        dustExpl.transform.forward = dustExpl.transform.position - playerPos + Vector3.up * 2f;
        dustExpl.Play();
    }
}
