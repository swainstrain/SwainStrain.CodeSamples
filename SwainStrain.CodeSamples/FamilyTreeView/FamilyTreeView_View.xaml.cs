using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;

namespace SwainStrain.CodeSamples.FamilyTreeView
{
    /// <summary>
    /// Interaction logic for FamilyTreeView_View.xaml
    /// </summary>
    public partial class FamilyTreeView_View : Window
    {
        private readonly Document _doc;
        public FamilyTreeView_View(Document doc)
        {
            InitializeComponent();
            _doc = doc;

            var helper = new WindowInteropHelper(this);
            helper.Owner = Process.GetCurrentProcess().MainWindowHandle;

            UITheme theme = UIThemeManager.CurrentTheme;

            var dictionary = Resources.MergedDictionaries.First();

            var source = theme == UITheme.Dark ?
                "pack://application:,,,/SwainStrain.CodeSamples;component/Resources/ResourceDictionary_Dark.xaml" :
                "pack://application:,,,/SwainStrain.CodeSamples;component/Resources/ResourceDictionary_Light.xaml";

            dictionary.Source = new Uri(source);

            IntPtr hWnd = new WindowInteropHelper(this).EnsureHandle();
            var backGround = theme == UITheme.Dark ? 0x212121 : 0xF5F5F5;
            int[] colorstr = new int[] { backGround };
            DwmSetWindowAttribute(hWnd, DWWMA_CAPTION_COLOR, colorstr, 4);

            LoadTree();
        }

        // P/Invoke declaration for modifying DWM window attributes
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        // Attribute ID for caption/title bar color
        const int DWWMA_CAPTION_COLOR = 35;

        private void LoadTree()
        {
            var families = new FilteredElementCollector(_doc)
                .OfClass(typeof(Family))
                .Cast<Family>()
                .GroupBy(f => f.FamilyCategory?.Name)
                .OrderBy(g => g.Key);

            foreach (var categoryGroup in families)
            {
                var categoryNode = new TreeViewItem
                {
                    Header = categoryGroup.Key ?? "<No Category>",
                    IsExpanded = false
                };

                foreach (var family in categoryGroup.OrderBy(f => f.Name))
                {
                    var familyNode = new TreeViewItem
                    {
                        Header = family.Name,
                        IsExpanded = false
                    };

                    // Family Types (Symbols)
                    foreach (ElementId symbolId in family.GetFamilySymbolIds())
                    {
                        var symbol = _doc.GetElement(symbolId) as FamilySymbol;
                        if (symbol == null) continue;

                        familyNode.Items.Add(new TreeViewItem
                        {
                            Header = symbol.Name
                        });
                    }

                    categoryNode.Items.Add(familyNode);
                }

                Tree.Items.Add(categoryNode);
            }
        }
    }
}
