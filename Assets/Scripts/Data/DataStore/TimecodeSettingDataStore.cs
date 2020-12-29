﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectBlue.RepulserEngine.Domain.Model;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.DataStore
{

    [Serializable]
    public class TimecodeSettingListForSerialize
    {
        public List<TimecodeSetting> Data = new List<TimecodeSetting>();

        public TimecodeSettingListForSerialize(){}
        
        public TimecodeSettingListForSerialize(IEnumerable<TimecodeSetting> data)
        {
            
            Data.Clear();
            
            foreach (var component in data)
            {
                if(component == null) continue;
                Data.Add(component);
            }
        }
    }
    

    public class TimecodeSettingDataStore : ITimecodeSettingDataStore
    {

        private static readonly string JsonFilePath =
            Path.Combine(UnityEngine.Application.streamingAssetsPath, "TimecodeSetting.json");
        
        private List<TimecodeSetting> endpointList;
        public IEnumerable<TimecodeSetting> EndPointList => endpointList;

        private Subject<IEnumerable<TimecodeSetting>> onDataChangedSubject = new Subject<IEnumerable<TimecodeSetting>>();
        public IObservable<IEnumerable<TimecodeSetting>> OnDataChangedAsObservable => onDataChangedSubject;

        
        public void Save(IEnumerable<TimecodeSetting> endpointSettings)
        {

            var target = new TimecodeSettingListForSerialize(endpointSettings);

            var json = JsonUtility.ToJson(target);
            
            using (var sw = new StreamWriter (JsonFilePath, false)) 
            {
                try
                {
                    sw.Write (json);
                }
                catch (Exception e)
                {
                    Debug.Log (e);
                }
            }

            endpointList = endpointSettings.ToList();
            
            Debug.Log($"Saved : {JsonFilePath}");
        }
        
        
        public IEnumerable<TimecodeSetting> Load()
        {

            var jsonDeserializedData = new TimecodeSettingListForSerialize();

            try 
            {
                using (var fs = new FileStream (JsonFilePath, FileMode.OpenOrCreate))
                using (var sr = new StreamReader (fs)) 
                {
                    var result = sr.ReadToEnd ();
                    
                    jsonDeserializedData =  JsonUtility.FromJson<TimecodeSettingListForSerialize>(result);
                }
            }
            catch (Exception e)
            {
                Debug.Log (e);
            }
            
            endpointList = jsonDeserializedData.Data.ToList();
            
            return jsonDeserializedData.Data;
        }

    }
    
}