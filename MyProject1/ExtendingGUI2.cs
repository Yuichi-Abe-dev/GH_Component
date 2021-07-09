using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyProject1
{
    public class ExtendingGUI2 : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ExtendingGUI2 class.
        /// </summary>
        public ExtendingGUI2()
          : base("ExtendingGUI2", "ExtGUI2",
              "Description",
              "Extra", "Util")
        {
        }
        public override void AppendAdditionalMenuItems(ToolStripDropDown menu)
        {
            base.AppendAdditionalMenuItems(menu);

            Menu_AppendGenericMenuItem(menu, "First item");
            Menu_AppendGenericMenuItem(menu, "Second item");
            Menu_AppendGenericMenuItem(menu, "Third item");
            Menu_AppendSeparator(menu);
            Menu_AppendGenericMenuItem(menu, "Fourth item");
            Menu_AppendGenericMenuItem(menu, "Fifth item");
            Menu_AppendGenericMenuItem(menu, "Sixth item");
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5d11daea-9a4c-45c8-9cd5-0f2d0fa2d1c4"); }
        }
    }
}