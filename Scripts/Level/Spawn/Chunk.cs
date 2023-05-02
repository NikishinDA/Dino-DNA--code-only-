using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Chunk : MonoBehaviour
{
    public Transform begin;
    public Transform end;
    [SerializeField] private float width;
    [SerializeField] private float length;

    [SerializeField] private GameObject[] chunkObjects;
    [SerializeField] private LayerMask layerMask;

   /* [Header("Visuals")]
    [SerializeField] private Renderer plainRenderer;
    [SerializeField] private Material[] firstMaterials;
    [SerializeField] private Material[] secondMaterials;
    [SerializeField] private int levelsPerChange = 3;
*/
    private void Awake()
    {
       /* if (plainRenderer)
        {
            Material[] mats = plainRenderer.materials;
            int ambianceNum =
                PlayerPrefs.GetInt(PlayerPrefsStrings.Level.Name, PlayerPrefsStrings.Level.DefaultValue) 
                / levelsPerChange % firstMaterials.Length;
            mats[0] = firstMaterials[ambianceNum];
            mats[1] = secondMaterials[ambianceNum];
            plainRenderer.materials = mats;
        }*/
       
       length = end.position.z - begin.position.z;
    }

    public void InitializeChunk(ChunkTemplate template, bool delayed = true)
    {
        if (delayed)
            StartCoroutine(DelayedInitialization(template));
        else
        {
            Vector3 objectPos = new Vector3();
            length = end.position.z - begin.position.z;
            Physics.autoSimulation = false;
            Physics.Simulate(Time.fixedDeltaTime);  
            Physics.autoSimulation = true;
            foreach (var levelObject in template.objects)
            {
                objectPos.x = levelObject.position.x * width / 2;
                objectPos.z = levelObject.position.y * length;
                objectPos.y = 10f;
                GameObject spawnPrefab = chunkObjects[(int)levelObject.type];
                Transform go = Instantiate(spawnPrefab, transform).transform;
                float originY = spawnPrefab.transform.localPosition.y;
                go.localPosition = objectPos;
                RaycastHit hit;
                Color rayColor = Color.red;
                float dist = 20f;
                if (Physics.Raycast(
                    go.position,
                    Vector3.down,
                    out hit,
                    20f,
                    layerMask))
                {
                    objectPos.y = originY + transform.InverseTransformPoint(hit.point).y;

                    go.localPosition = objectPos;
                    rayColor = Color.green;
                    dist = hit.distance;
                }

                Debug.DrawRay(go.position, Vector3.down * dist, rayColor, 60f);
            }
        }
    }

    private IEnumerator DelayedInitialization(ChunkTemplate template)
    {
        yield return new WaitForFixedUpdate();
        Vector3 objectPos = new Vector3();
        foreach (var levelObject in template.objects)
        {
            objectPos.x = levelObject.position.x * width/2;
            objectPos.z = levelObject.position.y * length;
            objectPos.y = 10f;
            GameObject spawnPrefab = chunkObjects[(int) levelObject.type];
            Transform go = Instantiate(spawnPrefab, transform).transform;
            float originY = spawnPrefab.transform.localPosition.y;
            go.localPosition = objectPos;
            RaycastHit hit;
            Color rayColor = Color.red;
            float dist = 20f;
            if (Physics.Raycast(
                go.position,
                Vector3.down,
                out hit,
                20f,
                layerMask))
            {
                objectPos.y = originY + transform.InverseTransformPoint( hit.point).y;

                go.localPosition = objectPos;
                rayColor = Color.green;
                dist = hit.distance;
            }

            Debug.DrawRay(go.position, Vector3.down * dist, rayColor, 60f);
        }
    }
}