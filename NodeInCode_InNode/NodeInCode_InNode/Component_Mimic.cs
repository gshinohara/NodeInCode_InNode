using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using System;
using System.Linq;
using System.Reflection;
using System.Drawing;

namespace NodeInCode_InNode
{
    public class Component_Mimic : GH_Component
    {
        private IGH_Param Input_Component {  get; set; }
        public Component_Mimic()
          : base("Mimic", "Mimic",
              "",
              "Params", "Util")
        {
        }
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            int index = pManager.AddGenericParameter("Component", "C", "", GH_ParamAccess.item);
            Input_Component = pManager[index];
            Input_Component.ObjectChanged -= Input_Component_ObjectChanged;
            Input_Component.ObjectChanged += Input_Component_ObjectChanged;
        }
        private void Input_Component_ObjectChanged(IGH_DocumentObject sender, GH_ObjectChangedEventArgs e)
        {
            if (e.Type == GH_ObjectEventType.Sources && sender == Input_Component)
            {
                if (Input_Component.VolatileDataCount == 0)
                    return;

                var paramList = this.Params.ToList();
                foreach (IGH_Param param in paramList)
                    if (param != Input_Component) this.Params.UnregisterParameter(param);

                IGH_Component component = (Input_Component.VolatileData.AllData(true).FirstOrDefault() as GH_ObjectWrapper).Value as IGH_Component;
                Func<IGH_Param, IGH_Param> get_instance = param =>
                {
                    var param_dup = param.GetType().GetConstructor(Type.EmptyTypes).Invoke(null) as IGH_Param;
                    param_dup.Name = param.Name;
                    param_dup.NickName = param.NickName;
                    param_dup.Access = param.Access;
                    param_dup.Optional = param.Optional;
                    return param_dup;
                };
                for (int i = 0; i < component.Params.Input.Count; i++)
                    this.Params.RegisterInputParam(get_instance(component.Params.Input[i]), i);
                for (int i = 0; i < component.Params.Output.Count; i++)
                    this.Params.RegisterOutputParam(get_instance(component.Params.Output[i]), i);

                this.Message = component.Name;
                Bitmap icon = component.Icon_24x24.Clone() as Bitmap;
                icon.RotateFlip(RotateFlipType.Rotate90FlipX);
                this.SetIconOverride(icon);
            }
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            IGH_Component component = null;
            DA.GetData(this.Params.Input.Count - 1, ref component);

            if (component != null)
                component.GetType().GetMethod("SolveInstance", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(component, new object[] { DA });
        }
        protected override Bitmap Icon => null;
        public override Guid ComponentGuid => new Guid("A44C410E-0D57-456E-B45A-9E057FA6DABE");
    }
}