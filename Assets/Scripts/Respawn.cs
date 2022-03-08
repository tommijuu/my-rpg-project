using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject prefab;
    public GameObject target;
    public Vector3 randomSpawn;

    void Start()
    {
        //int i = 10;
        //while (i > 0)
        //{
        StartCoroutine(Spawn(0f));
        //    i--;
        //}

    }

    public IEnumerator Spawn(float spawnTime)
    {
        Debug.Log(spawnTime, prefab);
        yield return new WaitForSeconds(spawnTime);

        randomSpawn = new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10));

        GameObject clone;
        clone = Instantiate(prefab, randomSpawn, Quaternion.identity);

        target = clone;
        target.transform.GetComponent<EnemyStats>().respawnPoint = this.gameObject;

        RaycastHit hit;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit)) //raycast downwards
        {
            target.transform.position = new Vector3(target.transform.position.x, hit.point.y + 2, target.transform.position.z); //y + 5 just to make sure spawn is on top of the terrain
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(randomSpawn, new Vector3(1f, 1f, 1f));
    }
}
