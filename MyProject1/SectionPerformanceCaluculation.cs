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
    public class SectionPerformanceCaluculation : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public SectionPerformanceCaluculation()
          : base("SectionPerformanceCaluculation", "SecPerfCalc",
              "Culc section performance",
              "Extra", "Simple")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "The points to sort", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Key", "K", "The key to sort points", GH_ParamAccess.item);

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "P", "The sorted points", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Indices", "I", "The index of sorted points", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> inPts = new List<Point3d>();
            int key = new int();

            if ((!DA.GetDataList(0, inPts)))
                return;
            if ((!DA.GetData(1, ref key)))
                return;

            List<Point3d> pt = new List<Point3d>();
            List<int> index = new List<int>();
            for (int i = 0; i < inPts.Count; i++) index.Add(i);

            switch (key)
            {
                case 0:
                    pt = inPts.OrderBy(p => p.X).ThenBy(p => p.Y).ThenBy(p => p.Z).ToList();
                    break;
                case 1:
                    pt = inPts.OrderBy(p => p.X).ThenBy(p => p.Z).ThenBy(p => p.Y).ToList();
                    break;
                case 2:
                    pt = inPts.OrderBy(p => p.Y).ThenBy(p => p.X).ThenBy(p => p.Z).ToList();
                    break;
                case 3:
                    pt = inPts.OrderBy(p => p.Y).ThenBy(p => p.Z).ThenBy(p => p.X).ToList();
                    break;
                case 4:
                    pt = inPts.OrderBy(p => p.Z).ThenBy(p => p.X).ThenBy(p => p.Y).ToList();
                    break;
                case 5:
                    pt = inPts.OrderBy(p => p.Z).ThenBy(p => p.Y).ThenBy(p => p.X).ToList();
                    break;
            }

            List<int> sortedIndices = new List<int>();
            for (int i = 0; i < inPts.Count; i++) sortedIndices.Add(inPts.IndexOf(pt[i]));

            DA.SetDataList(0, pt);
            DA.SetDataList(1, sortedIndices);
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
                return MyProject1.Properties.Resources.sortpoints;
                //return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("d068228a-27a9-4afc-a883-76c318c3f791"); }
        }
    }
}
