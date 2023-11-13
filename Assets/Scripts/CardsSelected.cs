using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsSelected : MonoBehaviour
{
    [SerializeField] private Sprite snowSprite;
    [SerializeField] private Sprite fireSprite;
    [SerializeField] private Sprite waterSprite;
    [SerializeField] private Sprite cardBackSprite;

    private float spawnDuration = 2f;
    Vector3 location = new Vector3(0f, 0f, 0f);
    Vector3 enemyLocation = new Vector3(5f, 0f, 0f);

    public void SpawnPlayerCard(JitsuCard playerCard)
    {
        StartCoroutine(SpawnPlayerCards(playerCard));
    }

    public IEnumerator SpawnPlayerCards(JitsuCard playerCard)
    {
        SpawnCard(playerCard, location);

        yield return new WaitForSeconds(spawnDuration);

        SpawnBackCard();
    }
    private void SpawnCard(JitsuCard card, Vector3 location)
    {
        Sprite spawnedSprite = Instantiate(snowSprite, location, Quaternion.identity);
        DespawnSprite(spawnedSprite);
    }
    private void SpawnBackCard()
    {
        Sprite spawnedSprite = Instantiate(cardBackSprite, enemyLocation, Quaternion.identity);
        DespawnSprite(spawnedSprite);
    }
    public void SpawnEnemyCard(JitsuCard enemyCard)
    {
        SpawnCard(enemyCard, enemyLocation);
    }
    private IEnumerator DespawnSprite(Sprite spawnedSprite)
    {
        yield return new WaitForSeconds(spawnDuration);
        Destroy(spawnedSprite);
    }
    
}
