using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Manager;
using UnityEngine;

namespace Assets.Scripts.Data
{
    public class RemoteDataFetch : MonoBehaviour
    {
        [SerializeField] private string dataUrl;
        [SerializeField] private List<JengaBlockData> dataEntries = new List<JengaBlockData>();

        private void Start()
        {
            StartCoroutine(nameof(DownloadJSON));
        }
        
        IEnumerator DownloadJSON()
        {
            WWW www = new WWW(dataUrl);
            yield return www;
            if(www.error == null)
            {
                Debug.Log("Downloaded JSON: " + www.text);
                this.ProcessData(www.text);
            }
            else
            {
                Debug.Log("ERROR: " + www.error);
            }
        }

        private void ProcessData(string jsonText)
        {
            string jsonString = fixJson(jsonText);
            this.dataEntries.AddRange(JsonHelper.FromJson<JengaBlockData>(jsonString));
            JengaGenerator.Instance.GenerateStacks(this.dataEntries);
            Debug.Log($"Processed all data entries {dataEntries.Count}");
        }
        
        string fixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }
        
        public static class JsonHelper
        {
            public static T[] FromJson<T>(string json)
            {
                Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
                return wrapper.Items;
            }

            [Serializable]
            private class Wrapper<T>
            {
                public T[] Items;
            }
        }
    }
}
