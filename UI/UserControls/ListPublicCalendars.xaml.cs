using System;
using System.Collections.Generic;
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

namespace UI.UserControls;
/// <summary>
/// Логика взаимодействия для ListPublicCalendars.xaml
/// </summary>
public partial class ListPublicCalendars : UserControl, IViewCalendar
{
    private List<MonthView> monthViewsCalendars;
    private List<WeekView> weekViewsCalendars;
    private readonly List<ContentControl> contentContolersCalendar = new();
    private List<IViewCalendar> currentCalendars;
    public DateTime CurrentDate { get; set; } = DateTime.Now;

    public ListPublicCalendars(int column, int row)
    {
        InitializeComponent();
        monthViewsCalendars = CreateCalendars<MonthView>(column * row);
        weekViewsCalendars = CreateCalendars<WeekView>(column * row);
        CreateAreaCalendars(column, row);
        FillFramesView(monthViewsCalendars);
    }

    private void CreateAreaCalendars(int column, int row)
    {
        for (int i = 0; i < column; i++)
        {
            var columnDefinition = new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            };
            AreaGrid.ColumnDefinitions.Add(columnDefinition);
        }

        for (int i = 0; i < row; i++)
        {
            var rowDefinition = new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            };
            AreaGrid.RowDefinitions.Add(rowDefinition);
        }

        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < row; j++)
            {
                var contentControl = new ContentControl();
                contentControl.Style = (Style)Application.Current.FindResource("ViewContentControlStyle");
                Grid.SetColumn(contentControl, i);
                Grid.SetRow(contentControl, j);
                AreaGrid.Children.Add(contentControl);
                contentContolersCalendar.Add(contentControl);
            }
        }
    }

    private void FillFramesView<T>(List<T> currentCalendars) where T : UserControl, IViewCalendar
    {
        this.currentCalendars = new List<IViewCalendar>(currentCalendars);
        for (int i = 0; i < contentContolersCalendar.Count; i++)
        {
            contentContolersCalendar[i].Content = currentCalendars[i];
        }
        UpdateView();
    }

    private List<T> CreateCalendars<T>(int count) where T : IViewCalendar
    {
        var result = new List<T>();
        var constructor = typeof(T).GetConstructor(new[] { typeof(TextBlock) }) 
            ?? throw new InvalidOperationException($"Type {typeof(T)} must have a constructor with a TextBlock parameter.");
        for (int i = 0; i < count; i++)
        {
            result.Add((T)constructor.Invoke([DateTextBlock]));
        }
        return result;
    }

    private void Next_Click(object sender, RoutedEventArgs e)
    {
        NextView();
    }

    private void Previous_Click(object sender, RoutedEventArgs e)
    {
        PrevView();
    }

    private void NextCalendar_Click(object sender, RoutedEventArgs e)
    {
        //TO-DO
    }

    private void PreviousCalendar_Click(object sender, RoutedEventArgs e)
    {
        //TO-DO
    }

    private void MonthButton_Click(object sender, RoutedEventArgs e)
    {
        FillFramesView(monthViewsCalendars);
        UpdateView();
    }

    private void WeekButton_Click(object sender, RoutedEventArgs e)
    {
        FillFramesView(weekViewsCalendars);
        UpdateView();
    }

    public void NextView()
    {
        foreach (var view in currentCalendars)
        {
            view.NextView();
        }
    }

    public void PrevView()
    {
        foreach (var view in currentCalendars)
        {
            view.PrevView();
        }
        UpdateView();
    }

    public void UpdateView()
    {
        foreach (var view in currentCalendars)
        {
            view.UpdateView();
        }
    }
}