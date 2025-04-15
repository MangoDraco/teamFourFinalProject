using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public HealthManager playerHealth;
    List<Heart> hearts = new List<Heart>();
    private void Start()
    {
        DrawHearts();
    }
    private void OnEnable()
    {
        HealthManager.OnPlayerDamaged += DrawHearts;
    }
    private void OnDisable()
    {
        HealthManager.OnPlayerDamaged -= DrawHearts;
    }
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        Heart heartComponent = newHeart.GetComponent<Heart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }
    public void DrawHearts()
    {
        ClearHearts();
        int heartsToMake = (int)playerHealth.maxHealth;
        Debug.Log(heartsToMake);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerHealth.curHealth - i, 0, 1);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }
    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<Heart>();
    }
}
