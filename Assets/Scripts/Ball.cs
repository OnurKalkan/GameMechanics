using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
/*
 * Throw the ball to a random place each time, so ball will land on each time on a random place
 * striker going to make a home run, base1->base2->base3->base4
 * who reaches to base4 first that team earn 1 point, either ball(red team) or striker(blue team)
 * there will 9 turns in the game, who reaches to score 5 first, going to win the game
 * show the results on UI
 * Add environment
 */

public class Ball : MonoBehaviour
{
    public Transform strikerPoint, baseGuy1, baseGuy2, baseGuy3, baseGuy4;
    bool isThrowed = false, catcher = false;
    public GameObject ballCatcher;

    void FirstThrow()
    {
        transform.DOMove(strikerPoint.position, 2).OnComplete(BallHit);        
    }

    void BallHit()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(0,0.35f,0.8f) * 2000);
        Invoke(nameof(BallCatcher), 1);
    }

    void BallCatcher()
    {
        catcher = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball Catcher")
        {
            transform.parent = other.gameObject.transform;
            GetComponent<Rigidbody>().isKinematic = true;
            catcher = false;
            transform.DOLocalMoveY(0.3f, 0.25f);
            other.transform.DORotate(new Vector3(0, -75, 0), 1).OnComplete(BallToTheBases);
        }
    }

    void BallToTheBases()
    {
        transform.parent = null;
        transform.DOMove(baseGuy1.position, 1.5f);
        transform.DOMove(baseGuy2.position, 1.5f).SetDelay(1.5f);
        transform.DOMove(baseGuy3.position, 1.5f).SetDelay(3.0f);
        transform.DOMove(baseGuy4.position, 1.5f).SetDelay(4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isThrowed == false)
        {
            isThrowed = true;
            FirstThrow();
        }
        if(catcher == true)
        {
            ballCatcher.GetComponent<NavMeshAgent>().destination = transform.position;
        }
    }
}
