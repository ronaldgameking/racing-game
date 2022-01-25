using System.Net;
using System.Net.Http;
using System.Net.Sockets;

using UnityEngine;
using UnityEditor;

public class ServerClient : MonoBehaviour
{
    [MenuItem("Tools / Test Server")]
    public static async void SendReq()
    {
        HttpClient client = new HttpClient();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://127.0.0.1:5750/");
        try
        {
            HttpResponseMessage message = await client.SendAsync(request);
            Logger.Log(message.StatusCode.ToString());
            Logger.Log(await message.Content.ReadAsStringAsync());
        }
        catch (SocketException)
        {
            Logger.LogWarning("The server isn't running on your computer");
        }
    }
}
