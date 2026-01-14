using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using SwainStrain.CodeSamples.FamilyTreeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwainStrain.CodeSamples.DockablePane
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class DockablePane_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiApplication = commandData.Application;
            var document = uiApplication.ActiveUIDocument.Document;
            UIDocument uiDoc = uiApplication.ActiveUIDocument;

            DockablePaneId id = new DockablePaneId(App.DockablePanelGuid);

            var dockableWindow = uiApplication.GetDockablePane(id);
            dockableWindow.Show();


            return Result.Succeeded;
        }
    }
    public class DockablePane_Availability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
