using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO_P12
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection connection;
        public ObservableCollection<String> columns { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            connection = null!;
            this.DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new(App.ConnectionString);
                connection.Open();
                LoadGroups();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void LoadGroups()
        {
            using SqlCommand command = new();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM ProductGroups";
            try
            {
                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())  // get result's one row
                {
                    columns.Add(
                        $"Id: {reader.GetGuid(0).ToString()[..4]}..., Name: {reader.GetString(1)}"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Query error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            connection?.Dispose();
        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            using SqlCommand command = new();
            command.Connection = connection;
            command.CommandText = 
                @"CREATE TABLE ProductGroups (
	                Id			UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	                Name		NVARCHAR(50)     NOT NULL,
	                Description NTEXT            NOT NULL,
                    Picture     NVARCHAR(50)     NULL
                )";
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Table Created");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Create error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InsertGroup_Click(object sender, RoutedEventArgs e)
        {
            using SqlCommand command = new();
            command.Connection = connection;
            command.CommandText = 
                @"INSERT INTO ProductGroups
	                ( Id, Name,	Description, Picture )
                VALUES
                ( '089015F4-31B5-4F2B-BA05-A813B5419285', N'Інструменти',     N'Ручний інструмент для побутового використання', N'tools.png' ),
                ( 'A6D7858F-6B75-4C75-8A3D-C0B373828558', N'Офісні товари',   N'Декоративні товари для офісного облаштування', N'office.jpg' ),
                ( 'DEF24080-00AA-440A-9690-3C9267243C43', N'Вироби зі скла',  N'Творчі вироби зі скла', N'glass.jpg' ),
                ( '2F9A22BC-43F4-4F73-BAB1-9801052D85A9', N'Вироби з дерева', N'Композиції та декоративні твори з деревини', N'wood.jpg' ),
                ( 'D6D9783F-2182-469A-BD08-A24068BC2A23', N'Вироби з каменя', N'Корисні та декоративні вироби з натурального каменю', N'stone.jpg' )";
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show("Data inserted");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insertation error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GroupCount_Click(object sender, RoutedEventArgs e)
        {
            using SqlCommand command = new();
            command.Connection = connection;
            command.CommandText = "SELECT COUNT(*) FROM ProductGroups";
            try
            {
                int cnt = Convert.ToInt32(
                    command.ExecuteScalar());
                MessageBox.Show($"Table has {cnt} rows");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Query error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
/* Д.З. Визначитись з фінальним (підсумковим) проєктом
 * Спроєктувати принаймні одну таблицю, скласти для неї 
 * SQL запит створення та заповнення тестовими даними
 * Повторити завдання, виконані на занятті - 
 * DDL запит створення таблиці
 * DML запит заповнення таблиці
 * Агрегуючий запит на кількість рядків у таблиці.
 */
/*

CREATE TABLE ProductGroups (
	Id			UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	Name		NVARCHAR(50)     NOT NULL,
	Description NTEXT            NOT NULL,
    Picture     NVARCHAR(50)     NULL
) ;

INSERT INTO ProductGroups
	( Id, Name,	Description, Picture )
VALUES
( '089015F4-31B5-4F2B-BA05-A813B5419285', N'Інструменти',     N'Ручний інструмент для побутового використання', N'tools.png' ),
( 'A6D7858F-6B75-4C75-8A3D-C0B373828558', N'Офісні товари',   N'Декоративні товари для офісного облаштування', N'office.jpg' ),
( 'DEF24080-00AA-440A-9690-3C9267243C43', N'Вироби зі скла',  N'Творчі вироби зі скла', N'glass.jpg' ),
( '2F9A22BC-43F4-4F73-BAB1-9801052D85A9', N'Вироби з дерева', N'Композиції та декоративні твори з деревини', N'wood.jpg' ),
( 'D6D9783F-2182-469A-BD08-A24068BC2A23', N'Вироби з каменя', N'Корисні та декоративні вироби з натурального каменю', N'stone.jpg' );
 */