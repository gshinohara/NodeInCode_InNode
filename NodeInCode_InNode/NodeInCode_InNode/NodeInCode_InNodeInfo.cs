using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace NodeInCode_InNode
{
    public class NodeInCode_InNodeInfo : GH_AssemblyInfo
    {
        public override string Name => "NodeInCode_InNode";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("48893b13-84d7-4732-8a2e-749f13189aff");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}