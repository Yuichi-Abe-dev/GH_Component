using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace MyProject1
{
    public class RangeByCount : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the RangeByCount class.
        /// </summary>
        public RangeByCount()
          : base("RangeByCount", "RangeCount",
              "Creating a sequence of numbers by first term, last term, and number of terms",
              "Extra", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Start", "S", "The first number", GH_ParamAccess.item);
            pManager.AddNumberParameter("End", "E", "The last number", GH_ParamAccess.item);
            pManager.AddNumberParameter("Count", "C", "The count of numbers", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Results", "R", "The calculation results", GH_ParamAccess.item);
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
            get { return new Guid("b9ce09d5-8b04-465d-8094-d55315524ad7"); }
        }
    }
}