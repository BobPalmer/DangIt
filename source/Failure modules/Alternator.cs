﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ippo
{
    public class ModuleAlternatorReliability : FailureModule
    {
        EngineManager engineManager;
        ModuleAlternator alternatorModule;

        public override string DebugName { get { return "DangItAlternator"; } }
        public override string InspectionName { get { return "Alternator"; } }
        public override string FailureMessage { get { return "Alternator failure!"; } }
        public override string RepairMessage { get { return "Alternator repaired."; } }
        public override string FailGuiName { get { return "Fail alternator"; } }
        public override string EvaRepairGuiName { get { return "Replace alternator"; } }
        public override string MaintenanceString { get { return "Replace alternator"; } }


        public override bool PartIsActive()
        {
            return engineManager.IsActive;
        }


        protected override void DI_Start(StartState state)
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                this.alternatorModule = this.part.Modules.OfType<ModuleAlternator>().Single();
                this.engineManager = new EngineManager(this.part);
            }
        }


        protected override bool DI_FailBegin()
        {
            // Always agree to fail
            return true;
        }


        protected override void DI_Disable()
        {
            this.alternatorModule.enabled = false;
        }


        protected override void DI_EvaRepair()
        {
            this.alternatorModule.enabled = true; 
        }

    }
}
