using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerUnit : NetworkBehaviour
{
    Vector2 prev = Vector2.zero;
    Vector2 velocity;

    //position which we think is most correct for this player
    //If we are the authority then this will be exactly the same as transform.position
    Vector2 bestGuessPosition;

    // the higher this value the faster our local position will match our best guess position
    float latencySmoothingFactor = 0;

    //constantly updated value about our latency to the server
    //How many seconds it takes to receive a oneway message
    //TODO probably should be handled by playerConnectionObject
    float ourLatency = 0; 

    //unit controlled by a player
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // code running here is running for ALL versions on this object on all pcs
        // maybe we want to simulate how we think the object is moving on different pcs
        //this is a PREDICTION based on last velocity update for this playerunit
        transform.Translate(velocity * Time.deltaTime);


        if (!hasAuthority)
        {
            // we arent the authority but still need to update our local position for this object based on our best guess 
            //for its position on the owning players screen

            bestGuessPosition = bestGuessPosition + (velocity * Time.deltaTime);
            //instead of jitterly teleporting player to best guess position, we will lerp it to make it smooth
            transform.position = Vector3.Lerp(transform.position, bestGuessPosition, Time.deltaTime * latencySmoothingFactor);
            return;
        }
        //if we get here we own this unit

        velocity = Vector2.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity += Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity += Vector2.down;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity += Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity += Vector2.left;
        }
        //if (Input.GetKeyDown(KeyCode.Equals))
        //{
        //    Destroy(gameObject);
        //}


        if (prev != velocity)
        {
            Cmd_UpdateVelocity(velocity, transform.position);
        }
        prev = velocity;
        transform.Translate(velocity * Time.deltaTime);
    }

    [Command]
    void Cmd_UpdateVelocity(Vector2 v, Vector2 p)
    {
        //on the server
        transform.position = p;
        velocity = v;

        //if we know our current latency we could do some PREDICTION:
        // transform.position = p + (v * (thisPlayersLatencyToServer));

        //on all the clients - sent to every client
        Rpc_UpdateVelocity(velocity, transform.position);
    }
    [ClientRpc]
    void Rpc_UpdateVelocity(Vector2 v, Vector2 p)
    {

        if (hasAuthority)
        {

            //I am on an authoritative client


            // this is my object so I should already have the most accurate position/velocity (maybe more accurate than server)
            //I might want want to patch this info with instructions 
            //from the server if its an illegal action despite jittery rubberband looking effect

            //lets assume there are no possible illegal moves so we ignore server's update to all clients of my own move;
            return;
      
        }

        //this is a nonauthoritative client, so I definitely need to listen to server

        //transform.position = p;
        velocity = v;
        bestGuessPosition = p + (velocity * ourLatency);
        // if we know what our current latency is, we could do this to PREDICT their position a little better to smooth it
        // transform.position = p + (v * ourLatencyToServer);


        //Now the position of player is as close as possible on all players screens

        //There could be blinking drift of player from different positions because there could be latency issues that makes
        //this look very ugly and terrible
        //so updating transform.position would not work for it looks dumb



    }
}
