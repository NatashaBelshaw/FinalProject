using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shark_manager : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    public GameObject shark;

    public float timerMin = 30f;
    public float timerMax = 40f;

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
        StartCoroutine("SpawnSharkTimer");
    }

    void SpawnShark()
    {
        float x = -1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(x, Random.Range(0, 1f), 0));
        spawnPoint.z = 0;

        //adjust y-axis position
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        Vector3 sharkSize = shark.GetComponent<Renderer>().bounds.size;
        spawnPoint.y = Mathf.Clamp(spawnPoint.y, bottomBorder + sharkSize.y / 2, topBorder - sharkSize.y / 2);

        GameObject.Instantiate(shark, spawnPoint, new Quaternion(0, 0, 0, 0));
    }

    IEnumerator SpawnSharkTimer()
    {
        if (timer >= maxTimer)
        {
            //spawn a fish
            SpawnShark();
            timer = 0;
            maxTimer = Random.Range(timerMin, timerMax);
        }

        timer += 0.1f;
        yield return new WaitForSeconds(0.1f);
    }
}
