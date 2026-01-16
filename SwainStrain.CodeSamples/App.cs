using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using SwainStrain.CodeSamples.WPFThemeSwitcher;
using SwainStrain.CodeSamples.TaskDialogMultipleOptions;
using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;
using SwainStrain.CodeSamples.DockablePane;
using SwainStrain.CodeSamples.PostableCommands;
using SwainStrain.CodeSamples.FamilyTreeView;
using SwainStrain.CodeSamples.MVVMExample;

namespace SwainStrain.CodeSamples
{
    public class App : IExternalApplication
    {
        public static Guid DockablePanelGuid => new Guid("{65B4FB52-EF30-4031-8D6D-CE08618336E7}");

        public Result OnStartup(UIControlledApplication application)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Assembly executingAssembly = Assembly.GetExecutingAssembly();            

            string assemblyPath = executingAssembly.Location;

            application.CreateRibbonTab("SwainStrainCodeSamples");
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("SwainStrainCodeSamples", "SwainStrainCodeSamples");

            AddPushButton(ribbonPanel, "WPFThemeSwitcher", "WPF Theme \nSwitcher", assemblyPath,
                typeof(WPFThemeSwitcher_Command).FullName,
                "WPF Theme Switcher",
                "SwainStrain.CodeSamples.Resources.WPFThemeSwitcher_Icon.png", 
                typeof(WPFThemeSwitcher_Availability).FullName);
            AddPushButton(ribbonPanel, "DockablePane", "Dockable\nPane", assemblyPath,
                typeof(DockablePane_Command).FullName,
                "Dockable Pane",
                "SwainStrain.CodeSamples.Resources.DockablePane_Icon.png",
                typeof(DockablePane_Availability).FullName);
            AddPushButton(ribbonPanel, "TaskDialogMultipleOptions", "Task Dialog\nMultiple Options", assemblyPath,
                typeof(TaskDialogMultipleOptions_Command).FullName,
                "Task Dialog Multiple Options",
                "SwainStrain.CodeSamples.Resources.TaskDialogMultipleOptions_Icon.png", 
                typeof(TaskDialogMultipleOptions_Availability).FullName);
            AddPushButton(ribbonPanel, "PostableCommands", "Postable\nCommands", assemblyPath,
                typeof(PostableCommands_Command).FullName,
                "Postable Commands",
                "SwainStrain.CodeSamples.Resources.PostableCommands_Icon.png", 
                typeof(PostableCommands_Availability).FullName);
            AddPushButton(ribbonPanel, "FamilyTreeView", "Family\nTreeView", assemblyPath,
                typeof(FamilyTreeView_Command).FullName,
                "Family Tree View",
                "SwainStrain.CodeSamples.Resources.FamilyTreeView_Icon.png", 
                typeof(FamilyTreeView_Availability).FullName);
            AddPushButton(ribbonPanel, "MVVMExample", "MVVM\nExample", assemblyPath,
                typeof(MVVMExample_Command).FullName,
                "MVVM Example",
                "SwainStrain.CodeSamples.Resources.MVVMExample_Icon.png", 
                typeof(MVVMExample_Availability).FullName);

            Assembly.LoadFrom(Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
    "MaterialDesignThemes.Wpf.dll"));
            Assembly.LoadFrom(Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
    "MaterialDesignColors.dll"));
            Assembly.LoadFrom(Path.Combine(
    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
    "Microsoft.Xaml.Behaviors.dll"));

            application.ControlledApplication.ApplicationInitialized += ControlledApplication_ApplicationInitialized;

            return Result.Succeeded;
        }

        private void ControlledApplication_ApplicationInitialized(object sender, ApplicationInitializedEventArgs e)
        {
            var application = sender as Application;
            var uiApplication = new UIApplication(application);

            DockablePane_Page dock = new DockablePane_Page();

            DockablePaneId id = new DockablePaneId(DockablePanelGuid);

            try
            {
                uiApplication.RegisterDockablePane(id, "SwainStrain Dockable Pane",
                        dock as IDockablePaneProvider);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Info Message", ex.Message);
            }
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        private void AddPushButton(RibbonPanel panel, string name, string text, string assemblyName, string className, string ToolTip, string imagePath, string availabilityClassName)
        {
            PushButtonData button = new PushButtonData(name, text, assemblyName, className);
            button.ToolTip = ToolTip;
            BitmapSource bitmap4 = GetEmbeddedImage(imagePath);
            button.Image = bitmap4;
            button.LargeImage = bitmap4;
            button.AvailabilityClassName = availabilityClassName;
            panel.AddItem(button);
        }

        public BitmapSource GetEmbeddedImage(string name)
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Stream stream = assembly.GetManifestResourceStream(name);
                return BitmapFrame.Create(stream);
            }
            catch
            {
                return null;
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("resources"))
                return null;

            try
            {
                Assembly executingAssembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyName = new AssemblyName(args.Name);

                string path = Path.Combine(Path.GetDirectoryName(executingAssembly.Location), assemblyName.Name + ".dll");

                if (!File.Exists(path))
                {
                    string path2 = assemblyName.Name + ".dll";
                    if (assemblyName.CultureInfo?.Equals(CultureInfo.InvariantCulture) == false)
                    {
                        path2 = String.Format(@"{0}\{1}", assemblyName.CultureInfo, path2);
                    }

                    using (Stream stream = executingAssembly.GetManifestResourceStream(path2))
                    {
                        if (stream == null)
                            return null;

                        byte[] assemblyRawBytes = new byte[stream.Length];
                        stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
                        return Assembly.Load(assemblyRawBytes);
                    }
                }

                var assembly = Assembly.LoadFrom(path);
                return assembly;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}