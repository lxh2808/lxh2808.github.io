using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class APIRequest
{
    private static APIRequest instance = new APIRequest();

    public static APIRequest GetInstance()
    {
        return instance;
    }

    private APIRequest()
    {
    }

    public IEnumerator RequestAPIByGet<T>(string api, Action<ResponseData<T>> action)
    {
        UnityWebRequest request = UnityWebRequest.Get(GaiaConst.URL + api);

        request.SetRequestHeader("Token", GaiaConst.Token);

        // Debug.Log("[Request]:" + api + "?");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            //下载完成后执行的回调
            if (request.isDone)
            {
                // if(!api.Equals("/cart/list"))
                    Debug.Log(request.downloadHandler.text);
                // var data = JSON.Parse(request.downloadHandler.text);
                var data = JsonConvert.DeserializeObject<ResponseData<T>>(request.downloadHandler.text);
                if (action != null)
                    action(data);
            }
        }
    }

    public IEnumerator RequestAPIByPost<T>(string api, List<IMultipartFormSection> formdata,
        Action<ResponseData<T>> action)
    {
        // List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        // formData.Add(new MultipartFormDataSection("field1=foo&field2=bar"));
        // formData.Add(new MultipartFormFileSection("my file data", "myfile.txt"));
        UnityWebRequest request = UnityWebRequest.Post(GaiaConst.URL + api, formdata);
        request.SetRequestHeader("Token", GaiaConst.Token);


        // Debug.Log("[Request]:"+api+"?"+formdata.ToString());
        Debug.Log("[Request]:" + api + "");

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            //下载完成后执行的回调
            if (request.isDone)
            {
                Debug.Log("[Response]:" + request.downloadHandler.text);

                var data = JsonConvert.DeserializeObject<ResponseData<T>>(request.downloadHandler.text);

                if (action != null)
                    action(data);
            }
        }
    }


    public IEnumerator LoadImageWithWebRequest(string url, Image image)
    {
        UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url);

        yield return unityWebRequest.SendWebRequest();

        if (string.IsNullOrEmpty(unityWebRequest.error))
        {
            var texture = DownloadHandlerTexture.GetContent(unityWebRequest);
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                image.sprite = sprite;
            }
            else
            {
            }
        }
        else
        {
            Debug.LogError(unityWebRequest.error);
        }

        unityWebRequest.Dispose();
    }

    public IEnumerator LoadBatchImage(string url, SellBatchItem item, Action<int, SellBatchItem> action)
    {
        UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url);

        //request.SetRequestHeader("Token", GaiaConst.Token);
        yield return unityWebRequest.SendWebRequest();

        if (string.IsNullOrEmpty(unityWebRequest.error))
        {
            var texture = DownloadHandlerTexture.GetContent(unityWebRequest);
            if (texture != null)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0.5f, 0.5f));
                item.sprites.Add(sprite);
                action(0, item);
            }
            else
            {
                action(-1, null);
            }
        }
        else
        {
            action(-1, null);
        }

        unityWebRequest.Dispose();
    }

    //保存文件
    IEnumerator SaveAssetBundle(string path, string filename, Action DownLoad = null)
    {
        //服务器上的文件路径
        string originPath = path + filename;

        using (UnityWebRequest request = UnityWebRequest.Get(originPath))
        {
            yield return request.SendWebRequest();

            //下载完成后执行的回调
            if (request.isDone)
            {
                byte[] results = request.downloadHandler.data;

                //转换sprite
                //var texture = DownloadHandlerTexture.GetContent(request);
                //if (texture != null)
                //    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                //保存File
                string savePath = Application.dataPath + "/DownLoadTexture";

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                FileInfo fileInfo = new FileInfo(savePath + "/" + filename);
                FileStream fs = fileInfo.Create();
                //fs.Write(字节数组, 开始位置, 数据长度);
                fs.Write(results, 0, results.Length);
                fs.Flush(); //文件写入存储到硬盘
                fs.Close(); //关闭文件流对象
                fs.Dispose(); //销毁文件对象
                if (DownLoad != null)
                    DownLoad();
            }
        }
    }
}

//==================================映射类===============================

[Serializable]
public class ResponseData<T>
{
    public int code;
    public string message;
    public T data;
}


[Serializable]
public class SellBatch
{
    public List<SellBatchItem> rows;
}



[Serializable]
public class SellBatchItem
{
    public string id { get; set; }
    public string batch { get; set; }
    public int batchNumber { get; set; }
    public List <string > image { get; set; }
    public int sellNumber { get; set; }
    public string solPrice { get; set; }
    public string startTime { get; set; }
    public string title { get; set; }
    public int type { get; set; }
    public List <string > attribute { get; set; }
    public string detail { get; set; }
    public string video { get; set; }
    public double metgPrice { get; set; }
    public double usdPrice { get; set; }
    public double gasFee { get; set; }
    public string isStartSell { get; set; }

    //扩展持有属性
    public List<Sprite> sprites { get; set; }
}

[Serializable]
public class LoginReturn
{
    public string token { get; set; }
    public string image { get; set; }
    public string phone { get; set; }
    public string username { get; set; }
    public string tfa { get; set; }
}


[Serializable]
public class NTFOrder
{
    public double payMetg { get; set; }
    public double accountMetg { get; set; }
    public string redirectUrl { get; set; }
}


[Serializable]
public class CartList
{
    public List<CartListItem> rows { get; set; }
    public List<CartListItem> invalidRows { get; set; }
    //扩展属性
    public double totalPrice { get; set; }
    public double totalUsd { get; set; }
}


[Serializable]
public class CartListItem
{
    public string id { get; set; }
    public string batchTitle { get; set; }
    public List<string> batchImage { get; set; }
    public double batchSolPrice { get; set; }
    public double metgPrice { get; set; }
    public double usdPrice { get; set; }
    public int buyStatus { get; set; }
}

[Serializable]
public class UserInfo
{
    public string id { get; set; }
    public string avatar { get; set; }
    public string username { get; set; }
    public double metg { get; set; }
}






[Serializable]
public class NTFDataAttributeItem
{
    /// <summary>
    /// 
    /// </summary>
    public string trait_type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string value { get; set; }
}
[Serializable]
public class NTFData
{
    /// <summary>
    /// 
    /// </summary>
    public string image { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List <NTFDataAttributeItem> attribute { get; set; }
}