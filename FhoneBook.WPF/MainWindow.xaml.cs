
using System;
using System.Linq;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using PhoneBook.Lib;

namespace PhoneBook.WPF
{
    public partial class MainWindow : Window
    {
        private readonly DbContext _db;

        public MainWindow()
        {
            InitializeComponent();

            _db = new DbContext();
            ReloadData();
        }

        private void ReloadData()
        {
            var list = _db.GetAll().Select(p => $"{p.LastName} {p.FirstName}");
            ListPhoneBookItems.ItemsSource = list;
        }

        private void ButtonCreate_OnClick(object sender, RoutedEventArgs e)
        {
            var item = new PhoneBookItem()
            {
                LastName = InputLastName.Text,
                FirstName = InputFirstName.Text,
                PhoneNumber = InputPhoneNumber.Text
            };
            var result = _db.Insert(item);

            ReloadData();

            if (result)
            {
                MessageBox.Show("Данные успешно добавлены", "Добавление данных", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ошибка добавления данных", "Добавление данных", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            var item = new PhoneBookItem()
            {
                Id = Convert.ToInt32(InputId.Text),
                LastName = InputLastName.Text,
                FirstName = InputFirstName.Text,
                PhoneNumber = InputPhoneNumber.Text
            };
            var result = _db.Update(item);

            ReloadData();

            if (result)
            {
                MessageBox.Show("Данные успешно обновлены", "Обновление данных", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ошибка обновления данных", "Обновление данных", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListPhoneBookItems.SelectedItem as PhoneBookItem;  // Предположим, что ListPhoneBookItems содержит объекты типа PhoneBookItem
            if (selectedItem != null)
            {
                var result = _db.Delete(selectedItem);  // Предполагается, что метод Delete принимает объект PhoneBookItem
                ReloadData();

                if (result)
                {
                    MessageBox.Show("Данные успешно удалены", "Удаление данных", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Ошибка удаления данных", "Удаление данных", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void ListPhoneBookItems_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = ListPhoneBookItems.SelectedIndex;
            var item = _db.GetById(id + 1);
            if (item != null)
            {
                InputId.Text = item.Id.ToString();
                InputLastName.Text = item.LastName;
                InputFirstName.Text = item.FirstName;
                InputPhoneNumber.Text = item.PhoneNumber;
            }
        }

        private void ButtonClear_OnClick(object sender, RoutedEventArgs e)
        {
            InputId.Clear();
            InputLastName.Clear();
            InputFirstName.Clear();
            InputPhoneNumber.Clear();
        }
    }
}
