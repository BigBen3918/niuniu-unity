using Firesplash.UnityAssets.SocketIO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PingPongClientSample : MonoBehaviour
{
    public Text txtSID, txtPing, txtPong, txtLosses, txtDC;
    SocketIOCommunicator sioCom;
    int pings = 0, pongs = 0, losses = 0, dcs = 0;
    bool pongReceived = false;

    void Start()
    {
        sioCom = GetComponent<SocketIOCommunicator>();

        sioCom.Instance.On("connect", (payload) =>
        {
            Debug.Log("Connected! Socket ID: " + sioCom.Instance.SocketID);
            txtSID.text = "SocketID: " + sioCom.Instance.SocketID;
        });

        sioCom.Instance.On("disconnect", (payload) =>
        {
            Debug.LogWarning("Disconnected: " + payload);
            txtDC.text = "Disconnects: " + ++dcs;
            txtSID.text = "SocketID: <i>lost connection to server</i>";
        });

        sioCom.Instance.On("PONG", (seqno) =>
        {
            if (int.Parse(seqno) != pings)
            {
                Debug.LogWarning("Received PONG with SeqNo " + seqno + " out of order. Ignoring.");
            }
            else
            {
                pongReceived = true;
                txtPong.text = "PONGs sent: " + ++pongs;
            }
        });

        sioCom.Instance.Connect();

        StartCoroutine(Pinger());
    }

    IEnumerator Pinger()
    {
        while(true)
        {
            //we will not send if we are not connected
            yield return new WaitUntil(() => sioCom.Instance.IsConnected());

            pongReceived = false;
            sioCom.Instance.Emit("PING", (++pings).ToString(), true);
            txtPing.text = "PINGs sent: " + pings;

            //we will wait 3 seconds after each ping
            yield return new WaitForSeconds(3);

            //Check if the server PONGed back
            if (!pongReceived)
            {
                losses++;
                txtLosses.text = "Losses: " + losses;
            }
        }
    }
}
