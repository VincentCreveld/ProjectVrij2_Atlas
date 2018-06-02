using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockslide : MonoBehaviour {


    [Header("Specifics")]
    public float slideDuration;
    public float anticipation =2;
    public int rockAmount;
    public int rocksThrown;
    public List<Transform> spawnPositions = new List<Transform>();
    [SerializeField]
    private List<GameObject> rocks = new List<GameObject>();
    private List<GameObject> rockPool = new List<GameObject>();

    [Header("Cam variables")]
    [SerializeField]
    private float shakeDuration;
    [SerializeField]
    private float shakeForce;
    private Camera cam;


    public void Start() {
        cam = Camera.main;
        for (int i = 0; i < rocks.Count; i++) {
            for (int j = 0; j < rockAmount; j++) {
                //spawn obj pool
                GameObject GO = Instantiate(rocks[i], transform.position, Quaternion.identity);
                GO.SetActive(false);
                GO.transform.parent = spawnPositions[0];
                rockPool.Add(GO);
            }
        }
    }

    [ContextMenu("Rockslide")]
	public void Slide() {
        StartCoroutine(RocksSetup());
    }

    public IEnumerator Earthquake(bool withSlide) {

        StartCoroutine(cam.GetComponent<CameraScript>().CameraShake(shakeForce, anticipation));
        yield return new WaitForSeconds(anticipation);
        if (withSlide) {
            StartCoroutine(RocksSetup());
            }
        }

    public IEnumerator RocksSetup() {
        int throwAmount = Random.Range(0, 10);
        float wait = Random.Range(0.1f, 0.3f);
        StartCoroutine(cam.GetComponent<CameraScript>().CameraShake(shakeForce, shakeDuration));
        for (int i = 0; i < throwAmount; i++) {
            ThrowRock();
        }
        rocksThrown += throwAmount;
        yield return new WaitForSeconds(wait);
        if(rocksThrown < rockAmount - 10) {
            StartCoroutine(RocksSetup());
        }else {
            yield return new WaitForSeconds(0f);
        }
    }

    public GameObject spawnRock() {
        //function to return an inactive gameobject to use
        int r = Random.Range(0, rockPool.Count - 1);
        if (!rockPool[r].activeSelf) {
            return rockPool[r];
        }
        return spawnRock();
    }

    public void ThrowRock() {
        GameObject go = spawnRock();
        go.transform.position = new Vector2(Random.Range(spawnPositions[0].position.x, spawnPositions[1].position.x), spawnPositions[0].position.y);
        go.SetActive(true);
    }




}
