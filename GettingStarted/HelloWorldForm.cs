using System;
using System.Windows.Forms;

namespace GettingStarted
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class HelloWorldForm : Form
    {
        private TextBox Name1TextBox;
        private Label Name1Label;
        private Label Name2Label;
        private TextBox Name2TextBox;
        private Button ConverseButton;
        private TextBox ConversationTextBox;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public HelloWorldForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Name1TextBox = new System.Windows.Forms.TextBox();
            this.Name1Label = new System.Windows.Forms.Label();
            this.Name2Label = new System.Windows.Forms.Label();
            this.Name2TextBox = new System.Windows.Forms.TextBox();
            this.ConverseButton = new System.Windows.Forms.Button();
            this.ConversationTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Name1TextBox
            // 
            this.Name1TextBox.Location = new System.Drawing.Point(88, 40);
            this.Name1TextBox.Name = "Name1TextBox";
            this.Name1TextBox.Size = new System.Drawing.Size(128, 20);
            this.Name1TextBox.TabIndex = 0;
            // 
            // Name1Label
            // 
            this.Name1Label.Location = new System.Drawing.Point(40, 40);
            this.Name1Label.Name = "Name1Label";
            this.Name1Label.Size = new System.Drawing.Size(48, 23);
            this.Name1Label.TabIndex = 1;
            this.Name1Label.Text = "Name1:";
            this.Name1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Name2Label
            // 
            this.Name2Label.Location = new System.Drawing.Point(40, 80);
            this.Name2Label.Name = "Name2Label";
            this.Name2Label.Size = new System.Drawing.Size(48, 23);
            this.Name2Label.TabIndex = 3;
            this.Name2Label.Text = "Name2:";
            this.Name2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Name2TextBox
            // 
            this.Name2TextBox.Location = new System.Drawing.Point(88, 80);
            this.Name2TextBox.Name = "Name2TextBox";
            this.Name2TextBox.Size = new System.Drawing.Size(128, 20);
            this.Name2TextBox.TabIndex = 2;
            // 
            // ConverseButton
            // 
            this.ConverseButton.Location = new System.Drawing.Point(107, 126);
            this.ConverseButton.Name = "ConverseButton";
            this.ConverseButton.Size = new System.Drawing.Size(75, 23);
            this.ConverseButton.TabIndex = 4;
            this.ConverseButton.Text = "Converse";
            this.ConverseButton.Click += new System.EventHandler(this.ConverseButton_Click);
            // 
            // ConversationTextBox
            // 
            this.ConversationTextBox.Location = new System.Drawing.Point(16, 176);
            this.ConversationTextBox.Multiline = true;
            this.ConversationTextBox.Name = "ConversationTextBox";
            this.ConversationTextBox.Size = new System.Drawing.Size(264, 100);
            this.ConversationTextBox.TabIndex = 5;
            // 
            // HelloWorldForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(292, 285);
            this.Controls.Add(this.ConversationTextBox);
            this.Controls.Add(this.ConverseButton);
            this.Controls.Add(this.Name2Label);
            this.Controls.Add(this.Name2TextBox);
            this.Controls.Add(this.Name1Label);
            this.Controls.Add(this.Name1TextBox);
            this.Name = "HelloWorldForm";
            this.Text = "HelloWorldForm";
            this.Load += new System.EventHandler(this.HelloWorldForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new HelloWorldForm());
        }

        private void HelloWorldForm_Load(object sender, EventArgs e)
        {
        }

        private void Converse(string name1, string name2)
        {
            Friendly friend1 = new Friendly(name1);
            Friendly friend2 = new Friendly(name2);

            ConversationTextBox.AppendText(friend1.SayHello());
            ConversationTextBox.AppendText(friend2.SayHello());

            ConversationTextBox.AppendText(friend1.Count(20));

            ConversationTextBox.AppendText(friend1.SayGoodbye(friend2.Name));
            ConversationTextBox.AppendText(friend2.SayGoodbye(friend1.Name));
        }

        private void ConverseButton_Click(object sender, EventArgs e)
        {
            Converse(Name1TextBox.Text, Name2TextBox.Text);
        }
    }

    class Friendly
    {
        private string myName;

        public Friendly(string name)
        {
            myName = name;
        }

        public String SayHello()
        {
            return String.Concat("Hello, my name is ", myName, "\r\n");
        }

        public String SaySomething()
        {
            return "You look nice today\r\n";
        }

        public String Count(int max)
        { // Control Flow Example
            String numbers = "";
            if (max < 1) return numbers;
            for (int loop = 1; loop <= max; loop++)
            {
                numbers = numbers + loop + " ";
                if ((loop % 10) == 0)
                    numbers = numbers + "\r\n";
            }
            return numbers;
        }

        public String SayGoodbye(string othername)
        {
            return String.Concat("Goodbye ", othername, "\r\n");
        }

        public string Name
        {
            get
            {
                return myName;
            }
            set
            {
                value = myName;
            }
        }
    }
}
