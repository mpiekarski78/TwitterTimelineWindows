using System;
using System.Drawing;
using System.Windows.Forms;
using tip2tail.WinFormAppBarLib;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {



        string prevTweet = "";
        int pos = 10;
        int marginT = 0;

        Label[] fullnameTLabel = new Label[100];
        Label[] usernameTLabel = new Label[100];
        Label[] tweetTLabel = new Label[100];

        int n = 1;
        

        public Form1()
        {
            InitializeComponent();

                   
        }

        
        private void timer_Tick(object sender, EventArgs e)
        {
            browser.Navigate("https://mobile.twitter.com/home");

            


            try {
                //  Console.WriteLine(browser.Document.Body.OuterHtml);
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(browser.Document.Body.OuterHtml);

                var document = htmlDoc.DocumentNode;

                var fullnameT = htmlDoc.DocumentNode.SelectSingleNode("//strong[@class='fullname'] ");
                var usernameT = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='username'] ");
                var tweetT = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='dir-ltr'] ");


                if (prevTweet != tweetT.InnerText)
                {

                    prevTweet = tweetT.InnerText;

                    if (n == 5)
                    {
                        this.streamTimeline.Clear();
                    }

                    string s = fullnameT.InnerText + " " + usernameT.InnerText + "\r\n" + tweetT.InnerText + "\r\n\r\n -- \r\n";
                    this.streamTimeline.SelectionStart = 0;
                    this.streamTimeline.SelectionLength = 0;
                    this.streamTimeline.SelectedText = s;

                    SelectRichText(streamTimeline, fullnameT.InnerText);
                    streamTimeline.SelectionColor = Color.LightGreen;

                    SelectRichText(streamTimeline, usernameT.InnerText);
                    streamTimeline.SelectionColor = Color.Yellow;

                    SelectRichText(streamTimeline, tweetT.InnerText);
                    streamTimeline.SelectionColor = Color.LightSkyBlue;

                   

                    pos += 100;
                    n += 1;
                

                }


            }
            catch (Exception err)
            {
                Console.WriteLine("{0} Exception caught.", err);

            }
        }


        private void SelectRichText(RichTextBox rch, string target)
        {
            int pos = rch.Text.IndexOf(target);
            if (pos < 0)
            {
                // Not found. Select nothing.
                rch.Select(0, 0);
            }
            else
            {
                // Found the text. Select it.
                rch.Select(pos, target.Length);
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {


            Timer timer = new Timer();
            timer.Interval = (10 * 600); // 10 secs
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            // przekierowanie okienka na prawą stronię
            AppBarHelper.AppBarMessage = "TestAppBarApplication";
            AppBarHelper.SetAppBar(this, AppBarEdge.Right);
            
            //pominięcie błędów JS
            browser.ScriptErrorsSuppressed = true;
                        
            

        }

          

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void streamTimeline_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void browser_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}


