//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CZBK.HeiMaOA.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class WF_StepInfo
    {
        public int ID { get; set; }
        public string SetpName { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsStartStep { get; set; }
        public bool IsEndStep { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public short StepResult { get; set; }
        public short DelFlag { get; set; }
        public System.DateTime SubTime { get; set; }
        public System.DateTime ProcessTime { get; set; }
        public string Remark { get; set; }
        public int ProcessBy { get; set; }
        public int ParentStepID { get; set; }
        public int ChildStepID { get; set; }
        public int WF_InstanceID { get; set; }
    
        public virtual WF_Instance WF_Instance { get; set; }
    }
}
