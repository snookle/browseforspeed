// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 1.1.4322.2032
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Stetic {
    
    class Gui {
        
        public static void Build(object obj, System.Type type) {
            Stetic.Gui.Build(obj, type.FullName);
        }
        
        public static void Build(object obj, string id) {
            System.Collections.Hashtable widgets = new System.Collections.Hashtable();
            if ((id == "MainWindow")) {
                Gtk.Window cobj = ((Gtk.Window)(obj));
                // Widget MainWindow
                cobj.Title = "gtkbrowseforspeed";
                cobj.WindowPosition = ((Gtk.WindowPosition)(4));
                cobj.Events = ((Gdk.EventMask)(0));
                cobj.Name = "MainWindow";
                // Container child MainWindow.Gtk.Container+ContainerChild
                Gtk.VBox w1 = new Gtk.VBox();
                w1.Events = ((Gdk.EventMask)(0));
                w1.Name = "mainVBox";
                widgets["mainVBox"] = w1;
                cobj.Add(w1);
                cobj.DefaultWidth = 675;
                cobj.DefaultHeight = 524;
                widgets["MainWindow"] = cobj;
                w1.Show();
                cobj.Show();
                cobj.DeleteEvent += ((Gtk.DeleteEventHandler)(System.Delegate.CreateDelegate(typeof(Gtk.DeleteEventHandler), cobj, "OnDeleteEvent")));
            }
            System.Reflection.FieldInfo[] fields = obj.GetType().GetFields(((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic) | System.Reflection.BindingFlags.Instance));
            for (int n = 0; (n < fields.Length); n = (n + 1)) {
                System.Reflection.FieldInfo field = fields[n];
                object widget = widgets[field.Name];
                if (((widget != null) && field.FieldType.IsInstanceOfType(widget))) {
                    field.SetValue(obj, widget);
                }
            }
        }
    }
    
}