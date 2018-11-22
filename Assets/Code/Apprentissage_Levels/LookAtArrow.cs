using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtArrow : MonoBehaviour {

    public Transform[] target;
    public int numberPointArrow;
    private float dist;
    [SerializeField]
    private int numberGate;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        
        if(numberPointArrow < numberGate)
        {
            dist = Vector3.Distance(transform.position, target[numberPointArrow].position);
            if (dist > 40)
            {
                transform.LookAt(target[numberPointArrow]);
            }
            else
            {
                transform.LookAt(target[numberPointArrow + 1]);
            }
        }
        
    }
    public void increaseNumberPointArrow()
    {

    }

    public void decreaseeNumberPointArrow(int number)
    {
        numberPointArrow = number;
    }

    public void getTheNextGate(Collider other)
    {
        for (int i = 0; i < target.Length; i++)
        {

            if (target[i].name == other.name)
            {
                numberPointArrow = i + 1;
                i = target.Length;
            }

        }
        
    }
}
