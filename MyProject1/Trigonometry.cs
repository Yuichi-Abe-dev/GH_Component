using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace MyProject1
{
    public class Trigonometry : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MyComponent1 class.
        /// </summary>
        public Trigonometry()
          : base("Trigonometry", "Tri",
              "Description",
              "Extra", "Maths")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddNumberParameter("Angle", "A", "The angle to measure", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Radians", "R", "Work in Radians", GH_ParamAccess.item, true); // The default value is 'true'
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Sin", "sin", "The sine of the Angle.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Cos", "cos", "The cosine of the Angle.", GH_ParamAccess.item);
            pManager.AddNumberParameter("Tan", "tan", "The tangent of the Angle.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare variables to contain all inputs.
            // We can assign some initial values that are either sensible or indicative.
            double angle = double.NaN;
            bool radians = false;

            // Use the DA object to retrieve the data inside the input parameters.
            // If the retrieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref angle)) { return; }
            if (!DA.GetData(1, ref radians)) { return; }

            // If the angle value is not a valid number, we should abort.
            if (!Rhino.RhinoMath.IsValidDouble(angle)) { return; }

            // If the user wants to work in degrees rather than radians,
            // we assume that angle is defined in degrees.
            // We need to convert it into Radians again.
            if (!radians)
            {
                angle = Rhino.RhinoMath.ToRadians(angle);
            }

            // Now we are ready to assign the outputs via the DA object.
            // Since the Sin(), Cos() and Tan() never fail, we might as well
            // combine them with the assignment.
            DA.SetData(0, Math.Sin(angle));
            DA.SetData(1, Math.Cos(angle));
            DA.SetData(2, Math.Tan(angle));
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
            get { return new Guid("295ab476-a8a1-4bb1-9700-430b8beff5d5"); }
        }
    }
}