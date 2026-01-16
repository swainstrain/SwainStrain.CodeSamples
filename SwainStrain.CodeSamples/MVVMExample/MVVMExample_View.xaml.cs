using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SwainStrain.CodeSamples.MVVMExample
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MVVMExample_View : Window
    {
        public MVVMExample_View()
        {
            InitializeComponent();

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
        }

        // P/Invoke declaration for modifying DWM window attributes
        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        // Attribute ID for caption/title bar color
        const int DWWMA_CAPTION_COLOR = 35;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is MVVMExample_ViewModel vm)
            {
                vm.LoadElements();
            }
        }
    }
}
