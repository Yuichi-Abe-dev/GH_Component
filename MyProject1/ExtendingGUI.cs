using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyProject1
{
    public class ExtendingGUI : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the ExtendingGUI class.
        /// </summary>
        public ExtendingGUI()
          : base("ExtendingGUI", "ExtGUI",
              "Description",
              "Extra", "Util")
        {
        }
        public override bool AppendMenuItems(ToolStripDropDown menu)
        {
            Menu_AppendGenericMenuItem(menu, "First item");
            Menu_AppendGenericMenuItem(menu, "Second item");
            Menu_AppendGenericMenuItem(menu, "Third item");
            Menu_AppendSeparator(menu);
            Menu_AppendGenericMenuItem(menu, "Fourth item");
            Menu_AppendGenericMenuItem(menu, "Fifth item");
            Menu_AppendGenericMenuItem(menu, "Sixth item");

            // Return true, otherwise the menu won't be shown.
            return true;
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
            get { return new Guid("00c78caa-d1e1-422b-99c1-7476e78ba01e"); }
        }
    }
}