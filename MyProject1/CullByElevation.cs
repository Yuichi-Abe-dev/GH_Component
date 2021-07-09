using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino.Geometry;

namespace MyProject1
{
    public class CullByElevation : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CullByElevation class.
        /// </summary>
        public CullByElevation()
          : base("Cull Elevation", "CullZ", "Cull objects by relative elevation", "Extra", "Sequence")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometry", "G", "Geometry to cull", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Count", "C", "Number of objects to cull", GH_ParamAccess.item, 1);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGeometryParameter("Geometry", "G", "Culled geometry", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //Declare a new List(Of T) to hold your data.
            //This list must exist and should probably be empty.
            List<IGH_GeometricGoo> geometry = new List<IGH_GeometricGoo>();
            Int32 count = 0;

            //Retrieve the whole list using Da.GetDataList().
            if ((!DA.GetDataList(0, geometry)))
                return;
            if (!DA.GetData(1, ref count))
                return;

            //Validate inputs.
            if ((count < 0))
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Count must be a positive integer");
                return;
            }

            //The number of objects to cull is larger than or
            //equal to the total number of objects. I.e. cull them all.
            if ((geometry.Count <= count))
                return;

            //Iteratively remove the lowest object from the list.
            for (Int32 N = 1; N <= count; N++)
            {
                double lowestElevation = double.MaxValue;
                Int32 lowestIndex = -1;

                //Iterate over all remaining geometry and find the lowest one.
                for (Int32 i = 0; i <= geometry.Count - 1; i++)
                {
                    if ((geometry[i] == null))
                        continue;
                    BoundingBox bbox = geometry[i].Boundingbox;
                    if ((!bbox.IsValid))
                        continue;

                    double localElevation = bbox.Min.Z;
                    if ((localElevation < lowestElevation))
                    {
                        lowestElevation = localElevation;
                        lowestIndex = i;
                    }
                }

                //Delete the lowest object.
                geometry.RemoveAt(lowestIndex);
            }

            //Assign the remaining geometry
            //(even if it is only a single item!)
            //using the DA.SetDataList() method.
            DA.SetDataList(0, geometry);
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
            get { return new Guid("92491eae-11e6-49d6-82ae-581c7824004e"); }
        }
    }
}