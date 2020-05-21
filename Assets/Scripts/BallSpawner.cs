using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    List<Pellet> pelletList = new List<Pellet>();
    public int pelletCount;
    public GameObject pellet;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < pelletCount; i++)
        {
            pelletList.Add(Instantiate(pellet).GetComponent<Pellet>());
            pelletList[i].observer = this;
            pelletList[i].index = i;
            pelletList[i].transform.parent = transform;
            Randomize(i);
        }
    }

    void Randomize(int index)
    {
        pelletList[index].SetupPellet(index, 35f, moveSpeed);
    }

    public void notify(int index)
    {
        Debug.Log(index);
        pelletList.RemoveAt(index);
        pelletList.Add(Instantiate(pellet).GetComponent<Pellet>());
        pelletList[pelletList.Count - 1].transform.parent = transform;
        pelletList[pelletList.Count - 1].observer = this;
        Randomize(pelletList.Count - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
