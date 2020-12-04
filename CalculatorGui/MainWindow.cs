using System;
using CalculatorGui;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    private TextBuffer buffer;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();

        // Formatar fontes de texto
        txtConsole.ModifyFont(Pango.FontDescription.FromString("Arial 20"));
        lblResult.ModifyFont(Pango.FontDescription.FromString("Arial 16"));
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void Clear(object sender, EventArgs e)
    {
        buffer = txtConsole.Buffer;
        buffer.Text = "";
        lblMessage.Text = "Above is the last result ...";
    }

    protected void Calc(object sender, EventArgs e)
    {
        buffer = txtConsole.Buffer;
        var calc = new Calc(buffer.Text);

        if (calc.Result.Equals("ERROR"))
        {
            lblResult.ModifyFg(StateType.Normal, new Gdk.Color(255, 0, 0));
        }
        else
        {
            lblResult.ModifyFg(StateType.Normal, new Gdk.Color(255, 255, 255));
            buffer.Text = calc.Formula;
        }

        lblResult.Text = "R: " + calc.Result;
        lblMessage.Text = calc.Message;
    }
}
