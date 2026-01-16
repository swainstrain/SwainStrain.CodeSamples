using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SwainStrain.CodeSamples.MVVMExample
{
    public class MVVMExample_ViewModel : INotifyPropertyChanged
    {
        private readonly UIDocument _uiDoc;

        public ObservableCollection<string> Elements { get; } = new ObservableCollection<string>();

        public MVVMExample_ViewModel(UIDocument uiDoc)
        {
            _uiDoc = uiDoc;
        }

        public void LoadElements()
        {
            Elements.Clear();

            var doc = _uiDoc.Document;
            var viewId = doc.ActiveView.Id;

            var collector = new FilteredElementCollector(doc, viewId)
                .WhereElementIsNotElementType();

            foreach (var elem in collector)
            {
                Elements.Add(elem.Name ?? elem.Id.ToString());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
