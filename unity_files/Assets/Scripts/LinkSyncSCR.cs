using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using SharpConnect;
using System.Security.Permissions;

public class LinkSyncSCR
{
    public Connector test = new Connector();
	public bool connected;
    string lastMessage;
	Grid grid;
	string id;


    public LinkSyncSCR()
	{
		connected = false;
	}
	/*
    void Update()
    {
		/*
        if (Input.GetKeyDown("space"))
        {

  
            //Debug.Log("space key was pressed");
            //test.fnPacketTest("space key was pressed");
        }

        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("escape key was pressed");
            test.fnPacketTest("escape key was pressed");
        }
        if (test.strMessage != "JOIN")
        {
            if (test.res != lastMessage)
            {
                Debug.Log(test.res);
                lastMessage = test.res;
            }
        }
        //test.fnPacketTest("BOOM SHAKALAKA");

    }
*/
	public void ConnectToServer(){
		Security.PrefetchSocketPolicy("192.168.0.11", 843);
		grid = GameObject.Find ("Grid").GetComponent<Grid>();
		id = grid.fbManager.profile ["id"].ToString();
		string result = test.fnConnectResult ("192.168.0.11", 4551, id);
		Debug.Log(result);
		connected = false;
		if (result == "Connection Succeeded")
		{
			connected = true;
		}

	}

    void OnApplicationQuit()
    {
        try { test.fnDisconnect(); }
        catch { }
    }
}