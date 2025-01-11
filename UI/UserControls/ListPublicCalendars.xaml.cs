using System.Windows;
using System.Windows.Controls;

namespace UI.UserControls;
/// <summary>
/// Логика взаимодействия для ListPublicCalendars.xaml
/// </summary>
public partial class ListPublicCalendars : UserControl, IViewCalendar
{
    private List<MonthView> monthViewsCalendars;
    private List<WeekView> weekViewsCalendars;
    private readonly List<ContentControl> contentContolersCalendar = new();
    private readonly List<ButtonShowPublicCalendar> buttons = new();
    private List<IViewCalendar> currentCalendars;
    public DateTime CurrentDate { get; set; } = DateTime.Now;

    public ListPublicCalendars(int column, int row)
    {
        InitializeComponent();
        monthViewsCalendars = CreateCalendars<MonthView>(column * row);
        weekViewsCalendars = CreateCalendars<WeekView>(column * row);
        CreateAreaCalendars(column, row);
        FillContentContolersView(monthViewsCalendars);
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

                var button = new ButtonShowPublicCalendar();
                Grid.SetColumn(button, i);
                Grid.SetRow(button, j);
                AreaGrid.Children.Add(button);

                contentContolersCalendar.Add(contentControl);
                buttons.Add(button);
            }
        }
    }

    private void FillContentContolersView<T>(List<T> currentCalendars) where T : UserControl, IViewCalendar
    {
        this.currentCalendars = new List<IViewCalendar>(currentCalendars);
        for (int i = 0; i < contentContolersCalendar.Count; i++)
        {
            contentContolersCalendar[i].Content = currentCalendars[i];
            buttons[i].Calendar = currentCalendars[i];
        }
        UpdateView();
    }

    //TO-DO
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
        FillContentContolersView(monthViewsCalendars);
        UpdateView();
    }

    private void WeekButton_Click(object sender, RoutedEventArgs e)
    {
        FillContentContolersView(weekViewsCalendars);
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