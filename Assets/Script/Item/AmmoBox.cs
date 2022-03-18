using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [Range(0, 5)]
    public int rewardsCount;
    public List<GameObject> rewards;
    bool opened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (opened)
            return;
        if (collision.gameObject.layer == 7 || collision.gameObject.layer == 8 || collision.gameObject.layer == 10)
        {
            opened = true;
            Destroy(gameObject);
            for (int i = 0; i <= rewardsCount; i++)
                Instantiate(
                    rewards[Random.Range(0, rewards.Count)], 
                    transform.position, 
                    Quaternion.identity
                );
            
        }
    }
}
