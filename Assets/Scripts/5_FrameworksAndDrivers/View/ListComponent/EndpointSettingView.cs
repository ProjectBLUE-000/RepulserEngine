﻿using System.Net;
using ProjectBlue.RepulserEngine.Domain.Model;
using TMPro;
using UniRx;
using UnityEngine;

namespace ProjectBlue.RepulserEngine.View
{
    public class EndpointSettingView : ReorderableListComponentView<EndpointSetting>
    {

        [SerializeField] public TMP_InputField ipTextField;
        [SerializeField] public TMP_InputField portTextField;

        [SerializeField] private ConnectionCheckButton connectionCheckButton;

        private EndpointSetting data;

        private void Start()
        {
            Observable.Merge(
                ipTextField.OnValueChangedAsObservable().Skip(1),
                portTextField.OnValueChangedAsObservable().Skip(1)
            )
            .Subscribe(value =>
            {
                SetDirty();

                data = ParseData(ipTextField.text, portTextField.text);
                if (data == null)
                {
                    Invalid();
                }

            }).AddTo(this);
        }

        protected override void OnUpdateIndex(int index)
        {
            connectionCheckButton.SetIndex(Index);
        }

        public override void UpdateView(EndpointSetting data)
        {
            if (data == null)
            {
                ipTextField.text = "";
                portTextField.text = "";
                return;
            }
            
            this.data = data;
            ipTextField.text = data.EndPoint.Address.ToString();
            portTextField.text = data.EndPoint.Port.ToString();
            
            Debug.Log(Index);
        }

        private EndpointSetting ParseData(string ip, string port)
        {

            if (IPAddress.TryParse(ip, out var ipParsed) && int.TryParse(port, out var portParsed))
            {
                return new EndpointSetting(new IPEndPoint(ipParsed, portParsed), "", 0);
            }

            return null;
        }

        public override EndpointSetting GetData()
        {
            return data;
        }
    }
    
}