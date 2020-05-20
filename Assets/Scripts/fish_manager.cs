using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fish_manager : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    public GameObject fish;

    public float timerMin = 18f;
    public float timerMax = 25f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        //range bounds are inclusive
        maxTimer = Random.Range(timerMin, timerMax);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("SpawnFishTimer");
    }

    void SpawnFish()
    {
        float x = -1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(x,Random.Range(0, 1f), 0));
        spawnPoint.z = 0;

        //adjust y-axis position
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        Vector3 fishSize = fish.GetComponent<Renderer>().bounds.size;
        spawnPoint.y = Mathf.Clamp(spawnPoint.y, bottomBorder + fishSize.y / 2, topBorder - fishSize.y / 2);

        GameObject.Instantiate(fish, spawnPoint, new Quaternion(0, 0, 0, 0));
    }

    IEnumerator SpawnFishTimer()
    {
        if (timer >= maxTimer)
        {
            //spawn a fish
            SpawnFish();
            timer = 0;
            maxTimer = Random.Range(timerMin, timerMax);
        }

        timer += 0.1f;
        yield return new WaitForSeconds(0.1f);
    }
}
