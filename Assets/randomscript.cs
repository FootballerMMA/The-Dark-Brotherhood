using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomscript : MonoBehaviour
{
    [SerializeField] private Sprite cardSprite;
    [SerializeField] private float spawnDuration = 2f;
    public void SpawnCard()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(screenCenter);

        GameObject spawnedObject = new GameObject("SpawnedSprite");
        spawnedObject.transform.position = spawnPosition;

        SpriteRenderer spriteRenderer = spawnedObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardSprite;

        StartCoroutine(DespawnSprite(spawnedObject));
    }

    private IEnumerator DespawnSprite(GameObject spawnedObject)
    {
        yield return new WaitForSeconds(spawnDuration);

        Destroy(spawnedObject);
    }
}
