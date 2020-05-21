using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    List<Pipe> pipes;
    Transform playerPosition;
    public float moveSpeed;
    public Material pipeMat;
    public float gap;
    public float distanceBetweenPipes;
    public int pipeCount = 5;
    // Start is called before the first frame update
    void Start()
    {
        pipes = new List<Pipe>();

        for (int i = 0; i < pipeCount; i++)
        {
            bool isLast = i + 1 == pipeCount;
            pipes.Add(new Pipe(gap, transform, i, isLast, distanceBetweenPipes, pipeMat));
        }
    }

    public void Reset()
    {
        for (int i = 0; i < pipes.Count; ++i)
        {
            pipes[i].ResetPipe();
            pipes[i].SetupPipe(i, distanceBetweenPipes);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Pipe pipe in pipes)
        {
            pipe.MovePipe(moveSpeed);
        }
    }
}

public class Pipe
{
    float gap;
    GameObject topCube;
    GameObject bottomCube;
    static float globalSpawnX;

    const float bottom = 0f;
    const float top = 20f;
    float pipeHeight;

    public float Gap { get; }

    public Pipe(float gap, Transform parentTransform, int index, bool isLast, float distanceBetweenPipes, Material pipeMat)
    {

        this.gap = gap;

        this.pipeHeight = (top - gap) / 2;

        Color pipeColor = Random.ColorHSV(0f, 1f, 0.5f, 1f);

        topCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        topCube.GetComponent<Renderer>().material.color = pipeColor;
        topCube.transform.parent = parentTransform;
        topCube.tag = "Wall";
        
        bottomCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottomCube.GetComponent<Renderer>().material.color = pipeColor;
        bottomCube.transform.parent = parentTransform;
        bottomCube.tag = "Wall";

        ResetPipe();
        SetupPipe(index, distanceBetweenPipes);
        

        if (isLast)
        {
            globalSpawnX = index * distanceBetweenPipes;
        }

    }



    public void SetupPipe(int index, float distanceBetweenPipes)
    {
        topCube.transform.localPosition = new Vector3(index * distanceBetweenPipes, topCube.transform.localPosition.y, 0);
        bottomCube.transform.localPosition = new Vector3(index * distanceBetweenPipes, bottomCube.transform.localPosition.y, 0);
    }

    public void ResetPipe()
    {
        float offset = Random.Range(-gap - 4f, gap + 4f);

        float topPipeY = ((pipeHeight + offset) / 2) - 0.5f;
        float bottomYScale = Mathf.Clamp(pipeHeight + offset, 0, float.MaxValue);

        bottomCube.transform.localPosition = new Vector3(globalSpawnX, topPipeY, 0);
        bottomCube.transform.localScale = new Vector3(1f, bottomYScale, 15f);


        float topYScale = Mathf.Clamp(pipeHeight - offset, 0, float.MaxValue);
        topCube.transform.localPosition = new Vector3(globalSpawnX, topPipeY + gap + pipeHeight, 0);
        topCube.transform.localScale = new Vector3(1f, topYScale, 15f);


        Color col = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.85f, 1f);
        topCube.GetComponent<Renderer>().material.color = col;
        bottomCube.GetComponent<Renderer>().material.color = col;

    }

    public void MovePipe(float amount) 
    {
        if (topCube.transform.localPosition.x < -50f)
        {
            ResetPipe();
        }

        topCube.transform.localPosition += Vector3.left * amount * Time.deltaTime;
        bottomCube.transform.localPosition += Vector3.left * amount * Time.deltaTime;
    }


}
