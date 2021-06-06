using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachine
{
    enum AppState
    {
        MenuEnter,
        MenuExit,
        GameEnter,
        GameExit
    }
    enum State
    {
        GameInit,
        GameReady,
        GameStart,
        GamePause,
        GameResume,
        GameEnd,
        GameRestart,

        EnableSettingBtn,
        EnableControlBtns
    }
}
