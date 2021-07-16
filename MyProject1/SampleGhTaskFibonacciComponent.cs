using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace MyProject1
{
    public class SampleGhTaskFibonacciComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SampleGhTaskFibonacciComponent class.
        /// </summary>
        public SampleGhTaskFibonacciComponent()
          : base("Task Fibonacci", "TFib", "Task computes a Fibonacci number.",
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
        public class SolveResults
        {
            public int Value { get; set; }
        }

        private static SolveResults ComputeFibonacci(int n)
        {
            SolveResults result = new SolveResults();
            if (n == 0)
                result.Value = 0;
            else if (n == 1)
                result.Value = 1;
            else
            {
                int x = 0, y = 1, rc = 0;
                for (int i = 2; i <= n; i++)
                {
                    rc = x + y;
                    x = y;
                    y = rc;
                }
                result.Value = rc;
            }
            return result;
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            const int max_steps = 46;

            if (InPreSolve)
            {
                // First pass; collect data and construct tasks
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

                // Run the task
                Task<SolveResults> task = Task.Run(() => ComputeFibonacci(steps), CancelToken);
                TaskList.Add(task);
                return;
            }

            if (!GetSolveResults(DA, out SolveResults result))
            {
                // Compute right here, right now.
                // 1. Collect
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

                // 2. Compute
                result = ComputeFibonacci(steps);
            }

            // 3. Set
            if (result != null)
            {
                DA.SetData(0, result.Value);
            }
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
            get { return new Guid("8d24f94d-ea69-4646-88f2-6d33ce0e185e"); }
        }
    }
}