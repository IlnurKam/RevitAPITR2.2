using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITR2._2
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        UIApplication uiapp = commandData.Application;
        UIDocument uidoc = uiapp.ActiveUIDocument;
        Document doc = uidoc.Document;

        IList<Reference> selectedElementRefList = uidoc.Selection.PickObjects(ObjectType.Element, new PipesFilter(), "Выберите трубы");
        var pipeList = new List<Pipe>();

        string info = string.Empty;
        foreach (var selectedElement in selectedElementRefList)
        {
            Pipe oPipe = doc.GetElement(selectedElement) as Pipe;
            pipeList.Add(oPipe);
            var diameter = UnitUtils.ConvertFromInternalUnits(oPipe.Diameter, UnitTypeId.Millimeters);
            info += $"Name:{oPipe.Name}, diameter: {diameter}{Environment.NewLine}";

        }
        info += $"Количество: {pipeList.Count}";

        TaskDialog.Show("Selection", info);

        return Result.Succeeded;

    }
}
