using Grasshopper.Kernel;
using System;
using Rhino.NodeInCode;
using System.Linq;
using Grasshopper.Kernel.Parameters;
using Grasshopper.Kernel.Data;

namespace NodeInCode_InNode
{
    public class Component_Mimic : GH_Component
    {
        private Guid Parent_ComponentGuid => new Guid("a0d62394-a118-422d-abb3-6af115c75b25");//Addition
        private ComponentFunctionInfo Component {  get; set; }
        public Component_Mimic()
          : base("Mimic", "Mimic",
              "",
              "Params", "Util")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            object a = null;
            object b = null;
            DA.GetData("a", ref a);
            DA.GetData("b", ref b);
            DA.SetDataList("result", (Component.InvokeKeepTree(a, b).FirstOrDefault() as Grasshopper.DataTree<object>).AllData());
        }
        public override void AddedToDocument(GH_Document document)
        {
            base.AddedToDocument(document);
            string name = Components.NodeInCodeFunctions.GetDynamicMemberNames().FirstOrDefault(n => Components.FindComponent(n).ComponentGuid == Parent_ComponentGuid);
            Component = Components.FindComponent(name);
            this.Params.Input.AddRange(Component.InputNames.Select(n => new Param_GenericObject { Name = n, NickName = n }));
            this.Params.Output.AddRange(Component.OutputNames.Select(n => new Param_GenericObject { Name = n, NickName = n }));
            this.Message = Component.FullName;
        }
        protected override System.Drawing.Bitmap Icon => null;
        public override Guid ComponentGuid => new Guid("A44C410E-0D57-456E-B45A-9E057FA6DABE");
    }
}