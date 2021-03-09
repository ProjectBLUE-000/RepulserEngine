﻿using System;
using System.Collections.Generic;
using ProjectBlue.RepulserEngine.Domain.Model;

namespace ProjectBlue.RepulserEngine.Presentation
{
    public interface ICommandSettingListPresenter
    {
        void Save(IEnumerable<CommandSetting> settingList);
        IEnumerable<CommandSetting> Load();
        IObservable<IEnumerable<CommandSetting>> OnListChangedAsObservable { get; }
    }
}