using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trash_manager : MonoBehaviour
{
    private float timer;
    private float maxTimer;
    public GameObject trash;

    public float timerMin = 15f;
    public float timerMax = 22f;

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
        StartCoroutine("SpawnTrashTimer");
    }

    void SpawnTrash()
    {
        float x = -1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(x, Random.Range(0, 1f), 0));
        spawnPoint.z = 0;

        //adjust y-axis position
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
        Vector3 trashSize = trash.GetComponent<Renderer>().bounds.size;
        spawnPoint.y = Mathf.Clamp(spawnPoint.y, bottomBorder + trashSize.y / 2, topBorder - trashSize.y / 2);

        GameObject.Instantiate(trash, spawnPoint, new Quaternion(0, 0, 0, 0));
    }

    IEnumerator SpawnTrashTimer()
    {
        if (timer >= maxTimer)
        {
            //spawn a fish
            SpawnTrash();
            timer = 0;
            maxTimer = Random.Range(timerMin, timerMax);
        }

        timer += 0.1f;
        yield return new WaitForSeconds(0.1f);
    }
}
