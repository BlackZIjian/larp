﻿using GameFramework;
using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace StarForce
{
    public class ProcedureLaunch : ProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            string welcomeMessage = string.Format("Hello! This is an empty project based on Game Framework {0}.", GameFrameworkEntry.Version);
            Log.Info(welcomeMessage);
            Log.Warning(welcomeMessage);
            Log.Error(welcomeMessage);
        }
    }
}
