namespace MauiSample_App;

public partial class TabbedPageDemo : TabbedPage
{
	public TabbedPageDemo()
	{
		InitializeComponent();
        BindLabel();

    }

    private void BindLabel()
    {
        string strTab1 = "Demo Tabbed Page Demo1";
        string strTab2 = "Demo Tabbed Page Demo2";
        string strTab3 = "Demo Tabbed Page Demo3";
     
  
        var binding = new Binding
        {
            Source = strTab1.ToString()
        };

        lblT1.SetBinding(Label.TextProperty, binding);

        var binding1 = new Binding
        {
            Source = strTab2.ToString()
        };

        lblT2.SetBinding(Label.TextProperty, binding1);

        var binding2 = new Binding
        {
            Source = strTab3.ToString()
        };

        lblT3.SetBinding(Label.TextProperty, binding2);
    }
}