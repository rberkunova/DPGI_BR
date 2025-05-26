using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace TextEditorApp
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand Save = new RoutedUICommand(
            "Save", "Save", typeof(CustomCommands),
            new InputGestureCollection() { new KeyGesture(Key.S, ModifierKeys.Control) });

        public static readonly RoutedUICommand Open = new RoutedUICommand(
            "Open", "Open", typeof(CustomCommands),
            new InputGestureCollection() { new KeyGesture(Key.O, ModifierKeys.Control) });

        public static readonly RoutedUICommand Clear = new RoutedUICommand(
            "Clear", "Clear", typeof(CustomCommands));
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CommandBinding saveBinding = new CommandBinding(CustomCommands.Save, Executed_Save, CanExecute_Save);
            this.CommandBindings.Add(saveBinding);

            CommandBinding openBinding = new CommandBinding(CustomCommands.Open, Executed_Open, CanExecute_Open);
            this.CommandBindings.Add(openBinding);

            CommandBinding clearBinding = new CommandBinding(CustomCommands.Clear, Executed_Clear, CanExecute_Clear);
            this.CommandBindings.Add(clearBinding);
        }

        private void CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mainTextBox != null && mainTextBox.Text.Length > 0) e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void Executed_Save(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, mainTextBox.Text);
            }
        }

        private void CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mainTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void CanExecute_Clear(object sender, CanExecuteRoutedEventArgs e)
        {
            if (mainTextBox != null && mainTextBox.Text.Length > 0) e.CanExecute = true;
            else e.CanExecute = false;
        }

        private void Executed_Clear(object sender, ExecutedRoutedEventArgs e)
        {
            mainTextBox.Clear();
        }
    }
}