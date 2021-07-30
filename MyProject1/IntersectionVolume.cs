using Grasshopper.Kernel;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using System;
using System.Collections.Generic;

namespace MyProject1
{
    public class IntersectionVolume : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the IntersectionVolume class.
        /// </summary>
        public IntersectionVolume()
          : base("IntersectionVolume", "IntersectVol",
              "Calculate intersect volume",
              "Extra", "Geometry")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBrepParameter("Brep A", "A", "First Brep", GH_ParamAccess.list);
            pManager.AddBrepParameter("Brep B", "B", "Second Brep", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddBooleanParameter("Collision", "C", "Collision detection", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Brep> brep_a = new List<Brep>();
            List<Brep> brep_b = new List<Brep>();
            List<bool> collision = new List<bool>();
            int count_a = brep_a.Count;
            int count_b = brep_b.Count;
            int index = 0;
            List<int> index_list = new List<int>();
            //Conditional branching based on number of breps
            if (count_a >= count_b)
            { //If there is more A than B
                for (int i = 0; i <= count_a; i++)
                {
                    index_list.Add(index);
                    if (count_a > i) {
                        index++;
                    }
                }
            }
            else //If there is more B than A
            {
                for (int i = 0; i <= count_b; i++)
                {
                    index_list.Add(index);
                    if (count_b > i)
                    {
                        index++;
                    }
                }
            }
            foreach (int j in index_list)
            {
                collision.Add(Intersection.BrepBrep(brep_a[j], brep_b[j], Rhino.RhinoMath.ZeroTolerance, out Curve[] intersectionCurves,out Point3d[] intersectionPoints));
            }

                DA.SetDataList(0, collision);
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
            get { return new Guid("9a0f0ea6-d2aa-43a3-acb7-dbc482712803"); }
        }
    }
}