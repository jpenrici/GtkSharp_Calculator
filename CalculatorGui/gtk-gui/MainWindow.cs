
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.VBox vbox1;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TextView txtConsole;

	private global::Gtk.Label lblResult;

	private global::Gtk.Label lblMessage;

	private global::Gtk.HBox hbox3;

	private global::Gtk.Button btnClear;

	private global::Gtk.Button btnCalc;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("Expression Calculator");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.txtConsole = new global::Gtk.TextView();
		this.txtConsole.HeightRequest = 25;
		this.txtConsole.CanFocus = true;
		this.txtConsole.Name = "txtConsole";
		this.txtConsole.AcceptsTab = false;
		this.txtConsole.WrapMode = ((global::Gtk.WrapMode)(1));
		this.txtConsole.PixelsAboveLines = 5;
		this.txtConsole.PixelsBelowLines = 5;
		this.txtConsole.LeftMargin = 5;
		this.txtConsole.RightMargin = 5;
		this.txtConsole.Indent = 5;
		this.GtkScrolledWindow.Add(this.txtConsole);
		this.vbox1.Add(this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
		w2.Position = 0;
		// Container child vbox1.Gtk.Box+BoxChild
		this.lblResult = new global::Gtk.Label();
		this.lblResult.Name = "lblResult";
		this.lblResult.Xpad = 10;
		this.lblResult.Xalign = 0F;
		this.lblResult.LabelProp = global::Mono.Unix.Catalog.GetString("=");
		this.lblResult.Selectable = true;
		this.vbox1.Add(this.lblResult);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.lblResult]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.lblMessage = new global::Gtk.Label();
		this.lblMessage.Name = "lblMessage";
		this.lblMessage.Xpad = 5;
		this.vbox1.Add(this.lblMessage);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.lblMessage]));
		w4.Position = 2;
		w4.Expand = false;
		w4.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hbox3 = new global::Gtk.HBox();
		this.hbox3.Name = "hbox3";
		this.hbox3.Spacing = 6;
		// Container child hbox3.Gtk.Box+BoxChild
		this.btnClear = new global::Gtk.Button();
		this.btnClear.CanFocus = true;
		this.btnClear.Name = "btnClear";
		this.btnClear.UseUnderline = true;
		this.btnClear.Label = global::Mono.Unix.Catalog.GetString("Clear");
		this.hbox3.Add(this.btnClear);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.btnClear]));
		w5.Position = 0;
		// Container child hbox3.Gtk.Box+BoxChild
		this.btnCalc = new global::Gtk.Button();
		this.btnCalc.CanFocus = true;
		this.btnCalc.Name = "btnCalc";
		this.btnCalc.UseUnderline = true;
		this.btnCalc.Label = global::Mono.Unix.Catalog.GetString("Calc");
		this.hbox3.Add(this.btnCalc);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.btnCalc]));
		w6.Position = 1;
		this.vbox1.Add(this.hbox3);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox3]));
		w7.Position = 3;
		w7.Expand = false;
		w7.Fill = false;
		this.Add(this.vbox1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 522;
		this.DefaultHeight = 237;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.btnClear.Clicked += new global::System.EventHandler(this.Clear);
		this.btnCalc.Clicked += new global::System.EventHandler(this.Calc);
	}
}
