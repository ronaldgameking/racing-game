using System.Net;
using System.Net.Http;

using UnityEngine;
using UnityEditor;

public class ServerClient : MonoBehaviour
{
    [MenuItem("Tools / Test Server")]
    public static async void SendReq()
    {
        HttpClient client = new HttpClient();
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "http://127.0.0.1:5750/");
        HttpResponseMessage message = await client.SendAsync(request);
        Logger.Log(message.StatusCode.ToString());
        Logger.Log(await message.Content.ReadAsStringAsync());
    }
}
