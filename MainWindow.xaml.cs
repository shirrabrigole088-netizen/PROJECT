using System.Security.Policy;
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

namespace BrigoleWpsApplicationProject
{
    public partial class MainWindow : Window
    {
        // declare the array storage
        string[] titles = new string[100];
        string[] genres = new string[100];
        string[] statusList = new string[100];
        string[] ratings = new string[100];

        char status = 'A';

        int index = 0;
        int updatedIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string rating = "";
            string title = txtTitle.Text;
            string genre = txtGenre.Text;
            string watchStatus = comboBoxStatus.Text;

            if (rb1.IsChecked == true) rating = "1";
            else if (rb2.IsChecked == true) rating = "2";
            else if (rb3.IsChecked == true) rating = "3";
            else if (rb4.IsChecked == true) rating = "4";
            else if (rb5.IsChecked == true) rating = "5";

            if (title == "" || genre == "" || watchStatus == "" || rating == "")
            {
                MessageBox.Show("Please fill all fields", "Watchlist", MessageBoxButton.OK);
                return;
            }

            SaveData(title, genre, watchStatus, rating);
            ClearData();
        }

        private void ClearData()
        {
            txtTitle.Clear();
            txtGenre.Clear();
            comboBoxStatus.SelectedIndex = -1;

            rb1.IsChecked = false;
            rb2.IsChecked = false;
            rb3.IsChecked = false;
            rb4.IsChecked = false;
            rb5.IsChecked = false;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;

            if (index >= 0)
            {
                txtTitle.Text = titles[index];
                txtGenre.Text = genres[index];
                comboBoxStatus.Text = statusList[index];

                if (ratings[index] == "1") rb1.IsChecked = true;
                else if (ratings[index] == "2") rb2.IsChecked = true;
                else if (ratings[index] == "3") rb3.IsChecked = true;
                else if (ratings[index] == "4") rb4.IsChecked = true;
                else if (ratings[index] == "5") rb5.IsChecked = true;

                btnDeleteData.IsEnabled = true;

                status = 'E';
                updatedIndex = index;
            }
        }

        private void SaveData(string t, string g, string s, string r)
        {
            if (status == 'A')
            {
                titles[index] = t;
                genres[index] = g;
                statusList[index] = s;
                ratings[index] = r;

                dataGrid.Items.Add(new
                {
                    Title = titles[index],
                    Genre = genres[index],
                    Status = statusList[index],
                    Rating = ratings[index]
                });

                index++;

                MessageBox.Show("New item added!", "Watchlist", MessageBoxButton.OK);
            }
            else if (status == 'E' && updatedIndex >= 0)
            {
                titles[updatedIndex] = t;
                genres[updatedIndex] = g;
                statusList[updatedIndex] = s;
                ratings[updatedIndex] = r;

                RefreshGrid();

                status = 'A';
                updatedIndex = -1;

                MessageBox.Show("Item updated!", "Watchlist", MessageBoxButton.OK);
            }
        }

        private void btnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            int deleteIndex = dataGrid.SelectedIndex;

            if (deleteIndex == -1)
            {
                MessageBox.Show("Please select a row to delete.");
                return;
            }

            ShiftElements(deleteIndex);
            index--;

            RefreshGrid();

            btnDeleteData.IsEnabled = false;
            ClearData();

            MessageBox.Show("Item deleted!", "Watchlist", MessageBoxButton.OK);
        }

        private void RefreshGrid()
        {
            dataGrid.Items.Clear();

            for (int i = 0; i < index; i++)
            {
                dataGrid.Items.Add(new
                {
                    Title = titles[i],
                    Genre = genres[i],
                    Status = statusList[i],
                    Rating = ratings[i]
                });
            }
        }

        private void ShiftElements(int deletedIndex)
        {
            for (int i = deletedIndex; i < index; i++)
            {
                titles[i] = titles[i + 1];
                genres[i] = genres[i + 1];
                statusList[i] = statusList[i + 1];
                ratings[i] = ratings[i + 1];
            }
        }
    }
}