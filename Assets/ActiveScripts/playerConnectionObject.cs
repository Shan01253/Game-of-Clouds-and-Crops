using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class playerConnectionObject : NetworkBehaviour
{
    //NETWORK OPTIMIZATION UPDATE TIPS:

        //DONT UPDATE WHAT HASNT CHANGED

        //USE PREDICTION WHEN POSSIBLE

        //FIGURE OUT WHATS DETERMINISTIC


    public GameObject playerUnitPrefab;
    public GameObject myPlayerUnit;

    // syncvars are variables which if they change on the server, all clients are notified of the new value
    // making something a syncvar means no client is able to change them themselvesx`

    // the hook is the name of a function that is called whenever the sync var is changed
    // this will work very nicely with events if I make this hook function invoke an event action that takes the syncvar type
    // it must take the type of variable taken in as an argument
    // keep in mind it is called before the value is changed

        //warning if you use a hook, the local value of the syncvar does not get updated so must explicitly set the name ourselves
        // by using a hook you transfer responsiblity for updating local variables to the hook function from unity
    
        //sync vars will be sent to clients who join later as well as those who were around when the variable changed
        //however clientrpcs will not be sent to clients to who join later (so I guess less overhead if that matters)
    [SyncVar(hook = "onPlayerNameChanged")]
    public string myPlayerName = "Anonymous";

    // Start is called before the first frame update
    void Start()
    {
        //is this my own local playerobj

        if (!hasAuthority)
        {
            return;
        }
        Debug.Log("spawning my unit!");
        //TODO Command server to instantiate playerobject
        Cmd_spawnMyUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasAuthority)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            string n = "Cloud " + Random.Range(1, 100);
            Debug.Log("Sending Server Request to Change My Name to " + n);
            Cmd_changePlayerName(n);
        }
    }

    void onPlayerNameChanged(string newName)
    {
        Debug.Log("player name change side effect!!! OldName: " + myPlayerName + " NewName: " + newName);

        // we may not be the server so:
        // also on the server the player name is newName no matter what but on the client the name can be whatever locally
        myPlayerName = newName;

        gameObject.name = "PlayerConnectionObject[" + newName + "]";
    }

    //commands - special functions only executed on server

    [Command]
    void Cmd_spawnMyUnit()
    {
        //we are guaranteed to be on server now
        GameObject go = Instantiate(playerUnitPrefab);
        
        myPlayerUnit = go;
        //object exists on server but now we give it to all the clients and fire up the network component
        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);
    }
    [Command]
    void Cmd_moveUnitUp()
    {
        if(myPlayerUnit == null)
        {
            return;
        }

        myPlayerUnit.transform.Translate(0, 1, 0);
    }
    [Command]
    void Cmd_changePlayerName(string newName)
    {
        Debug.Log("Server changing player name");

        //we could block bad offensive names here
        //by just returning
        //fast for simple things like names but client may want to do complex things and run it by the server later

        //this is now a sync var so does not require an RPC
        myPlayerName = newName;
        //Rpc_changePlayerName(newName);

    }
    //commands runs functions only on server
    //RPC runs functions only clients
    // an Rpc is used when you need to know where it came from as a client and sync var when it only matters to the server
    [ClientRpc]
    void Rpc_changePlayerName(string newName)
    {
        Debug.Log("we were asked to change the name on a particular player object");

        myPlayerName = newName;
    }
}
