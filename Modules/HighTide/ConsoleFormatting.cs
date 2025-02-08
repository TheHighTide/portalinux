namespace PortaLinuxVM.Modules.HighTide
{
    internal class ConsoleFormatting
    {
        public static string FormatString(string text)
        {
            string output = text;

            output = output.Replace("{DEFAULT}", ForeColor.RESET);
            output = output.Replace("{RESET}", ForeColor.RESET);
            output = output.Replace("{BLACK}", ForeColor.BLACK);
            output = output.Replace("{LBLACK}", ForeColor.LBLACK);
            output = output.Replace("{RED}", ForeColor.RED);
            output = output.Replace("{LRED}", ForeColor.LRED);
            output = output.Replace("{GREEN}", ForeColor.GREEN);
            output = output.Replace("{LGREEN}", ForeColor.LGREEN);
            output = output.Replace("{YELLOW}", ForeColor.YELLOW);
            output = output.Replace("{LYELLOW}", ForeColor.LYELLOW);
            output = output.Replace("{BLUE}", ForeColor.BLUE);
            output = output.Replace("{LBLUE}", ForeColor.LBLUE);
            output = output.Replace("{MAGENTA}", ForeColor.MAGENTA);
            output = output.Replace("{LMAGENTA}", ForeColor.LMAGENTA);
            output = output.Replace("{CYAN}", ForeColor.CYAN);
            output = output.Replace("{LCYAN}", ForeColor.LCYAN);
            output = output.Replace("{WHITE}", ForeColor.WHITE);
            output = output.Replace("{LWHITE}", ForeColor.LWHITE);

            return output; // Return the calculated and formatted string back to the main program
        }
    }
}
