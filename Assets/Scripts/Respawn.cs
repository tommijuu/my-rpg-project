using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject prefab;
    public GameObject target;

    void Start()
    {
        StartCoroutine(Spawn(0));
    }

    public IEnumerator Spawn(float spawnTime)
    {
        yield return new WaitForSeconds(spawnTime);

        Vector3 randomSpawn = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));

        GameObject clone;
        clone = Instantiate(prefab, randomSpawn, Quaternion.identity);
        target = clone;
        target.transform.GetComponent<EnemyStats>().respawnPoint = this.gameObject;

        RaycastHit hit;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit)) //raycast downwards
        {
            target.transform.position = new Vector3(target.transform.position.x, hit.point.y + 5, target.transform.position.z); //y + 5 just to make sure spawn is on top of the terrain
        }
    }
}
