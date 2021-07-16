using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace MyProject1
{
    public class SampleGhFibonacciComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SampleGhFibonacciComponent class.
        /// </summary>
        public SampleGhFibonacciComponent()
          : base("Fibonacci", "Fib", "Computes a Fibonacci number.",
              "Extra", "Util")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddIntegerParameter("Steps", "S", "Number of steps to compute.", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddIntegerParameter("Fibonacci number", "F", "The Fibonacci number", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
                const int max_steps = 46;

                int steps = 0;
                DA.GetData(0, ref steps);
                if (steps < 0)
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Steps must be >= 0.");
                    return;
                }
                if (steps > max_steps) // Prevent overflow...
                {
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, $"Steps must be <= {max_steps}.");
                    return;
                }

                int result;
                if (steps == 0)
                    result = 0;
                else if (steps == 1)
                    result = 1;
                else
                {
                    int x = 0, y = 1, rc = 0;
                    for (int i = 2; i <= steps; i++)
                    {
                        rc = x + y;
                        x = y;
                        y = rc;
                    }
                    result = rc;
                }

                DA.SetData(0, result);
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
            get { return new Guid("8a756200-ab7b-41ea-bfe5-5b85f182c4fb"); }
        }
    }
}