﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMoveApp.commons.plugin;
using YourMoveApp.commons.model;

namespace YourMoveApp.server.api
{
    internal interface IGamePluginProvider
    {
        public IGamePlugin GetGamePlugin(GameType gameType);
    }
}