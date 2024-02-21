using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Linq;

namespace NodeInCode_InNode
{
    public class Component_Mimic : GH_Component
    {
        public Component_Mimic()
          : base("Mimic", "Mimic",
              "",
              "Params", "Util")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Component", "C", "", GH_ParamAccess.item);
        }
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
        }
        protected override void BeforeSolveInstance()
        {
            if (this.Params.Input.FirstOrDefault().VolatileDataCount == 0)
                return;

            IGH_Component component = (this.Params.Input.FirstOrDefault(p => p.Name == "Component").VolatileData.AllData(true).FirstOrDefault() as GH_ObjectWrapper).Value as IGH_Component;
            Func<IGH_Param, IGH_Param> get_instance = param =>
            {
                var a = param.GetType().GetConstructor(Type.EmptyTypes).Invoke(null) as IGH_Param;
                a.Name = param.Name;
                a.NickName = param.NickName;
                a.Access = param.Access;
                return a;
            };
            foreach (IGH_Param param in component.Params.Input)
                this.Params.RegisterInputParam(get_instance(param));
            foreach (IGH_Param param in component.Params.Output)
                this.Params.RegisterOutputParam(get_instance(param));
        }
        protected override System.Drawing.Bitmap Icon => null;
        public override Guid ComponentGuid => new Guid("A44C410E-0D57-456E-B45A-9E057FA6DABE");
    }
}