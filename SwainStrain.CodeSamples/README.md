# SwainStrain Code Samples

A collection of Revit API code samples demonstrating best practices for building Revit add-ins, with focus on WPF integration, MVVM architecture, and Revit API workflows.

## Overview

This solution contains practical examples of implementing modern patterns and techniques for Revit development, including:

- **MVVM Architecture** - Clean separation of concerns using the Model-View-ViewModel pattern
- **WPF Integration** - Theming support and proper Revit window integration
- **Revit API Workflows** - Real-world examples of interacting with the Revit document

## Project Structure

```
SwainStrain.CodeSamples/
?
??? MVVMExample/                           # Basic MVVM pattern example
?   ??? MVVMExample_Command.cs             # IExternalCommand entry point for Revit
?   ??? MVVMExample_View.xaml              # XAML UI definition for the main window
?   ??? MVVMExample_View.xaml.cs           # Code-behind with Revit integration & theming
?   ??? MVVMExample_ViewModel.cs           # ViewModel with data binding & element loading
?
??? FamilyTreeView/                        # Advanced example with hierarchical tree view
?   ??? FamilyTreeView_Command.cs          # IExternalCommand entry point
?   ??? FamilyTreeView_View.xaml           # TreeView UI for family hierarchies
?   ??? FamilyTreeView_View.xaml.cs        # Code-behind with tree population logic
?
??? Resources/                             # Shared WPF resources & theming
?   ??? ResourceDictionary_Common.xaml     # Common styles, brushes, and templates
?   ??? ResourceDictionary_Light.xaml      # Light theme color palette & overrides
?   ??? ResourceDictionary_Dark.xaml       # Dark theme color palette & overrides
?   ??? FamilyTreeView_Icon.png            # Icon asset for FamilyTreeView add-in
?
??? SwainStrain.CodeSamples.csproj         # Project file (.NET 8, WPF)
??? README.md                              # This file
??? .gitignore                             # Git ignore rules
```

### File Descriptions

#### MVVMExample
The foundational MVVM implementation demonstrating core patterns:

- **MVVMExample_Command.cs** - Implements `IExternalCommand`. Instantiates the ViewModel and View, sets up data context, and displays the window as a modal dialog.
- **MVVMExample_View.xaml** - Simple 2-row layout with a "Load Elements" button and ListBox displaying bound ObservableCollection.
- **MVVMExample_View.xaml.cs** - Handles window initialization, Revit theme detection, ResourceDictionary loading, and DWM title bar customization.
- **MVVMExample_ViewModel.cs** - Implements `INotifyPropertyChanged`. Exposes `ObservableCollection<string>` and `LoadElements()` method that uses Revit's `FilteredElementCollector`.

#### FamilyTreeView
An advanced example extending the basic pattern with hierarchical data visualization:

- **FamilyTreeView_Command.cs** - Similar entry point to MVVMExample_Command, but passes the Revit Document to the View.
- **FamilyTreeView_View.xaml** - Defines a TreeView control for displaying hierarchical family relationships.
- **FamilyTreeView_View.xaml.cs** - Inherits theming logic from MVVMExample pattern, adds tree population logic in `LoadTree()` method.

#### Resources
Centralized theming and styling resources for consistent UI:

- **ResourceDictionary_Common.xaml** - Defines button styles, colors, brushes, and control templates shared across all windows.
- **ResourceDictionary_Light.xaml** - Light theme overrides (foreground: black/dark gray, background: white/light gray).
- **ResourceDictionary_Dark.xaml** - Dark theme overrides (foreground: white/light gray, background: dark gray/black).
- **FamilyTreeView_Icon.png** - 16×16 or 32×32 PNG icon displayed in Revit's add-in ribbon/menu.

## Key Examples

### MVVM Example

Demonstrates a minimal MVVM implementation for a Revit add-in dialog:

**Components:**
- **View** (`MVVMExample_View`): WPF window with automatic Revit theming support
- **ViewModel** (`MVVMExample_ViewModel`): Handles data binding and element collection logic
- **Command** (`MVVMExample_Command`): IExternalCommand implementation that launches the window

**Features:**
- Loads elements from the active view using FilteredElementCollector
- Data binding through ObservableCollection for real-time UI updates
- Automatic Dark/Light theme detection and application
- Proper window ownership with Revit (ensures proper z-ordering)

**Usage in Revit:**
1. Click the add-in button to invoke the command
2. The window displays with matching Revit theme
3. Click "Load Elements" to populate the list with elements from the active view

### Family Tree View Example

Demonstrates a more advanced implementation for visualizing hierarchical data (family relationships).

**Features:**
- TreeView control for hierarchical data representation
- Same theming and window integration as MVVMExample
- Structured navigation of family relationships in Revit

## Technical Highlights

### Revit Theme Integration

All windows automatically adapt to Revit's Dark/Light theme:

```csharp
UITheme theme = UIThemeManager.CurrentTheme;
// Load appropriate ResourceDictionary based on theme
```

### Window Title Bar Customization

Uses Desktop Window Manager (DWM) API to customize title bar colors:

```csharp
DwmSetWindowAttribute(hWnd, DWWMA_CAPTION_COLOR, colorstr, 4);
```

### Data Binding with INotifyPropertyChanged

ViewModel implements property change notifications for seamless UI updates:

```csharp
public class MVVMExample_ViewModel : INotifyPropertyChanged
{
    public ObservableCollection<string> Elements { get; } = new();
}
```

## Dependencies

- **Autodesk Revit API** - Revit 2024+ (compatible with .NET 8)
- **.NET 8** - Target framework
- **WPF** - Windows Presentation Foundation for UI
- **C# 12** - Latest language features

## Building and Testing

### Prerequisites

1. Autodesk Revit 2024 or later installed
2. Visual Studio 2022 or later
3. .NET 8 SDK

### Build Steps

```
# Clone the repository
git clone https://github.com/swainstrain/BIMDev.CodeSamples.git

# Build the solution
dotnet build

# The compiled DLL should be placed in the Revit Add-Ins folder
```

### Installation in Revit

1. Build the solution
2. Create an `.addin` manifest file in the Revit Add-Ins directory
3. Reference the compiled DLL
4. Restart Revit and activate the add-in from the External Tools menu

## Code Patterns Used

### MVVM Pattern

- **Model**: Revit API objects (Document, Elements, etc.)
- **View**: XAML windows with minimal code-behind
- **ViewModel**: Business logic, data binding, and command handling

### Command Pattern

`MVVMExample_Command` implements `IExternalCommand` as the Revit entry point, instantiating the ViewModel and View with proper data context binding.

### Resource Dictionary Strategy

Uses merged ResourceDictionaries for theming:
- `ResourceDictionary_Common.xaml` - Shared styles and templates
- `ResourceDictionary_Light.xaml` - Light theme specific colors
- `ResourceDictionary_Dark.xaml` - Dark theme specific colors

## Best Practices Demonstrated

? **Clean Separation of Concerns** - View, ViewModel, and Command are separate  
? **Data Binding** - Uses WPF binding instead of direct UI manipulation  
? **Proper Window Ownership** - Ensures correct z-ordering in Revit  
? **Theme Support** - Adapts to Revit's theme automatically  
? **Observable Collections** - Enables reactive UI updates  
? **Transaction Management** - Uses required Revit transaction attributes  

## Resources

- [Revit API Documentation](https://www.autodesk.com/developer/revit)
- [WPF Documentation](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
- [MVVM Pattern Overview](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm)

## License

This code is provided as-is for educational purposes.

## Contributing

Contributions are welcome! Please ensure code follows the patterns established in this project.

---

**Author:** SwainStrain  
**Repository:** https://github.com/swainstrain/BIMDev.CodeSamples
