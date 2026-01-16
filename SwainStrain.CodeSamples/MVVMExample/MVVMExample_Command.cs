using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace SwainStrain.CodeSamples.MVVMExample
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class MVVMExample_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;

            var viewModel = new MVVMExample_ViewModel(uidoc);
            var window = new MVVMExample_View
            {
                DataContext = viewModel
            };

            window.ShowDialog();
            return Result.Succeeded;
        }
    }

    public class MVVMExample_Availability : IExternalCommandAvailability
    {
        public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
        {
            return true;
        }
    }
}
