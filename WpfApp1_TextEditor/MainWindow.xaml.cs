using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
namespace WpfApp1_TextEditor;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private string copy="";
    private string cut="";
    private int count = 0;

    private void open_click(object sender, RoutedEventArgs e)
    {
       OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "Text Files (*.txt)|*.txt";
        dialog.Title = "choose a file";

        if (dialog.ShowDialog() == true)
        {
            string file = dialog.FileName;
            try
            {
                string metn = File.ReadAllText(file);
                file_name.Content = file;
                text.Text = metn;            
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Not Opened: " + ex.Message);
            }
        }

    }

    private void all_click(object sender, RoutedEventArgs e)
    {
        int firstindx = 0; 
        int selectedLength = text.Text.Length;
      
        text.Select(firstindx, selectedLength);
        text.Focus();

    }

    
    
    private void paste_click(object sender, RoutedEventArgs e)
    {
        if (count == 1)
        {
            int cari1 = text.CaretIndex;
            string a = "";
            string b = "";
            for (int i = 0; i < cari1; i++)
            {
                a += text.Text[i];
            }
            for (int i = cari1; i < text.Text.Length; i++)
            {
                b += text.Text[i];
            }
            text.Text = a + cut + b;
            count = 0;
        }
        else if(count ==0 && copy.Length!=0)
        {
            int cari = text.CaretIndex;
            string a = "";
            string b = "";
            for (int i = 0; i < cari; i++)
            {
                a += text.Text[i];
            }
            for (int i = cari; i < text.Text.Length; i++)
            {
                b += text.Text[i];
            }
            text.Text = a + copy + b;
        }
    }

    private void copy_click(object sender, RoutedEventArgs e)
    {
        int bas = text.SelectionStart;
        int son = text.SelectionLength;

        if (bas > 0)
        {
           
            copy = text.Text.Substring(bas, son);
           
        }
        if (count == 1) count = 0;
    }

    private void cut_click(object sender, RoutedEventArgs e)
    {          
        int header = text.SelectionStart;
        int footer = text.SelectionLength;

        if (header > 0)
        {
            
            cut = text.Text.Substring(header, footer);
            text.Text = text.Text.Remove(header, footer);
            count = 1;
           
        }
     
    }

    private void change(object sender, TextChangedEventArgs e)
    {
        if (auto.IsChecked == true) { File.WriteAllText(file_name.Content.ToString(), text.Text); }
    }

    private void save_click(object sender, RoutedEventArgs e)
    {
        SaveFileDialog save = new SaveFileDialog();
        if (save.ShowDialog() == true)
        {
            string file = save.FileName;
            try
            {
                File.WriteAllText(file, text.Text);
                text.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("File is not found: " + ex.Message);
            }
        }
    }
}
