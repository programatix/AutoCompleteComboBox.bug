using AutoCompleteComboBox.bug.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoCompleteComboBox.bug
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ViewModel();
        }

        private sealed class ViewModel : INotifyPropertyChanged
        {
            public ViewModel()
            {
                TestItems = new ObservableCollection<TestItem>();

                for (int i = 1; i <= 14; i++)
                {
                    TestItems.Add(new TestItem()
                    {
                        BaseName = "Base" + i.ToString(),
                        PersonId = 10,
                    });
                }
            }

            #region INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;

            private void SetField<X>(ref X field, X value, [CallerMemberName] string propertyName = null)
            {
                if (EqualityComparer<X>.Default.Equals(field, value)) return;

                field = value;

                var h = PropertyChanged;
                if (h != null) h(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion INotifyPropertyChanged

            public IReadOnlyList<Person> Items
            {
                get { return PersonModule.All; }
            }

            private Person selectedItem;

            public Person SelectedItem
            {
                get { return selectedItem; }
                set { SetField(ref selectedItem, value); }
            }

            private long? selectedValue;

            public long? SelectedValue
            {
                get { return selectedValue; }
                set { SetField(ref selectedValue, value); }
            }

            private ObservableCollection<TestItem> testItems;

            public ObservableCollection<TestItem> TestItems
            {
                get { return testItems; }
                set { SetField(ref testItems, value); }
            }
        }
    }
}