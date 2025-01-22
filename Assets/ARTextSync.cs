using Photon.Pun;
using TMPro;
using UnityEngine;

public class ARTextSync : MonoBehaviourPun, IPunObservable
{
    public TextMeshPro textMesh;

    private Vector3 syncedPosition;
    private Vector3 syncedScale;
    private string syncedText;

    void Update()
    {
        if (!photonView.IsMine)
        {
            // Synchronize the position, scale, and text for remote instances
            transform.position = syncedPosition;
            transform.localScale = syncedScale;
            textMesh.text = syncedText;
        }
    }

    public void SetText(string text)
    {
        if (photonView.IsMine)
        {
            syncedText = text;
            textMesh.text = text;
        }
    }

    public void SetPosition(Vector3 position)
    {
        if (photonView.IsMine)
        {
            syncedPosition = position;
            transform.position = position;
        }
    }

    public void SetScale(Vector3 scale)
    {
        if (photonView.IsMine)
        {
            syncedScale = scale;
            transform.localScale = scale;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send data to other clients
            stream.SendNext(transform.position);
            stream.SendNext(transform.localScale);
            stream.SendNext(textMesh.text);
        }
        else
        {
            // Receive data from other clients
            syncedPosition = (Vector3)stream.ReceiveNext();
            syncedScale = (Vector3)stream.ReceiveNext();
            syncedText = (string)stream.ReceiveNext();
        }
    }
}
