using System;
using System.Collections.Generic;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.Geometry;

// In order to load the result of this wizard, you will also need to
// add the output bin/ folder of this project to the list of loaded
// folder in Grasshopper.
// You can use the _GrasshopperDeveloperSettings Rhino command for that.

namespace MyProject1
{
    public class FourRulesOperation : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public FourRulesOperation()
          : base("FourRulesOperation", "4RulOpe",
              "Culc arithmetic operations",
              "Extra", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("A", "A", "The first number", GH_ParamAccess.item);
            pManager.AddNumberParameter("B", "B", "The second number", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Key", "K", "The key to sort points", GH_ParamAccess.item);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Results", "R", "The calculation results", GH_ParamAccess.item);
            pManager.AddTextParameter("Formula", "F", "The formula", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> inPts = new List<Point3d>();
            double A = new double();
            double B = new double();
            double R = new double();
            string S = ""; //symbol
            int key = 0;

            if (!DA.GetData(0, ref A))
                return;
            if (!DA.GetData(1, ref B))
                return;
            if (!DA.GetData(2, ref key))
                return;

            switch (key)
            {
                case 0:
                    R = A + B;
                    S = " + ";
                    break;
                case 1:
                    R = A - B;
                    S = " - ";
                    break;
                case 2:
                    R = A * B;
                    S = " * ";
                    break;
                case 3:
                    R = A / B;
                    S = " / ";
                    break;
            }
            string F = A.ToString() + S + B.ToString();
            DA.SetData(0, R);
            DA.SetData(1, F);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b30eec69-0cb1-4b05-8bd3-049be572de3d"); }
        }
    }
}