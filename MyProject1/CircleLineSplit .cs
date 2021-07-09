using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace MyProject1
{
    public class CircleLineSplit : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the CircleLineSplit class.
        /// </summary>
        public CircleLineSplit()
          : base("CircleLineSplit", "CLS",
              "Description",
              "Extra", "Geometry")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCircleParameter("Circle", "C", "The circle to slice", GH_ParamAccess.item);
            pManager.AddLineParameter("Line", "L", "Slicing line", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddArcParameter("Arc A", "A", "First Split result.", GH_ParamAccess.item);
            pManager.AddArcParameter("Arc B", "B", "Second Split result.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // 1. Declare placeholder variables and assign initial invalid data.
            //    This way, if the input parameters fail to supply valid data, we know when to abort.
            Rhino.Geometry.Circle circle = Rhino.Geometry.Circle.Unset;
            Rhino.Geometry.Line line = Rhino.Geometry.Line.Unset;

            // 2. Retrieve input data.
            if (!DA.GetData(0, ref circle)) { return; }
            if (!DA.GetData(1, ref line)) { return; }

            // 3. Abort on invalid inputs.
            if (!circle.IsValid) { return; }
            if (!line.IsValid) { return; }

            // 4. Project line segment onto circle plane.
            line.Transform(Rhino.Geometry.Transform.PlanarProjection(circle.Plane));

            // 5. Test projected segment for validity.
            if (line.Length < Rhino.RhinoMath.ZeroTolerance)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Line could not be projected onto the Circle plane");
                return;
            }

            // 6. Solve intersections and 7. Abort if there are less than two intersections.
            double t1;
            double t2;
            Rhino.Geometry.Point3d p1;
            Rhino.Geometry.Point3d p2;

            switch (Rhino.Geometry.Intersect.Intersection.LineCircle(line, circle, out t1, out p1, out t2, out p2))
            {
                case Rhino.Geometry.Intersect.LineCircleIntersection.None:
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "No intersections were found");
                    return;

                case Rhino.Geometry.Intersect.LineCircleIntersection.Single:
                    AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "Only a single intersection was found");
                    return;
            }

            // 8. Create slicing arcs.
            double ct;
            circle.ClosestParameter(p1, out ct);

            Rhino.Geometry.Vector3d tan = circle.TangentAt(ct);

            // 9. Assign output arcs.
            DA.SetData(0, new Rhino.Geometry.Arc(p1, tan, p2));
            DA.SetData(1, new Rhino.Geometry.Arc(p1, -tan, p2));
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
            get { return new Guid("f6cf0ee8-5151-4728-9fa9-293c3fe8cb61"); }
        }
    }
}