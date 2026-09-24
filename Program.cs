namespace StoperApp;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        try
        {
            ApplicationConfiguration.Initialize();
            var form = new Form1();
            form.Load += (s, e) => {
                File.AppendAllText("debug.log", "Form loaded successfully at " + DateTime.Now + "\n");
            };
            form.FormClosed += (s, e) => {
                File.AppendAllText("debug.log", "Form closed: " + e.CloseReason + "\n");
            };
            File.AppendAllText("debug.log", "Starting Application.Run at " + DateTime.Now + "\n");
            Application.Run(form);
            File.AppendAllText("debug.log", "Application.Run finished.\n");
        }
        catch (Exception ex)
        {
            File.WriteAllText("error.log", ex.ToString());
        }
    }    
}