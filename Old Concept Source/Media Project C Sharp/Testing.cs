using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication5
{
    class Program
    {
        static void CommandOutput(string[] args)
        {
            getOldBox a = new getOldBox();
            a.Refresh();   // re-paint a form
            do
            {
                output = Console.StandardOutput.ReadLine();
                a.MyText = output;
                a.Refresh();  // re-paint a form
            } while (output.Length != 0);
        }
        static void show()
        {

        }
    }
}

class getOldBox : System.Windows.Forms.Form
{
    System.Windows.Forms.TextBox t = new System.Windows.Forms.TextBox();
    public getOldBox()
    {
        t.Multiline = true;
        t.Size = new System.Drawing.Size(100, 100);
        Controls.Add(t);
        Show();
    }
    public string MyText
    {
        get
        {
            return t.Text;
        }
        set
        {
            t.Text = t.Text + "\r" + "\n" + value;
        }
    }

}