namespace Client
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "Form1";

            this.btnSendMessage = new System.Windows.Forms.Button();
            this.textBoxChat = new System.Windows.Forms.TextBox();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.btnOfferGame = new System.Windows.Forms.Button();
            this.lbGames = new System.Windows.Forms.ListBox();
            this.btnJoinGame = new System.Windows.Forms.Button();
            this.btnCreateGame = new System.Windows.Forms.Button();
            this.btnCancelGame = new System.Windows.Forms.Button();
            this.lblUser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMessage.Location = new System.Drawing.Point(8, 549);
            this.btnSendMessage.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(265, 57);
            this.btnSendMessage.TabIndex = 1;
            this.btnSendMessage.Text = "Послать сообщение";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // textBoxChat
            // 
            this.textBoxChat.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxChat.Location = new System.Drawing.Point(8, 106);
            this.textBoxChat.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxChat.Multiline = true;
            this.textBoxChat.Name = "textBoxChat";
            this.textBoxChat.ReadOnly = true;
            this.textBoxChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxChat.Size = new System.Drawing.Size(580, 331);
            this.textBoxChat.TabIndex = 3;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxMessage.Location = new System.Drawing.Point(8, 445);
            this.textBoxMessage.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessage.Size = new System.Drawing.Size(580, 97);
            this.textBoxMessage.TabIndex = 4;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Location = new System.Drawing.Point(12, 50);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(331, 40);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Сменить пользователя";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lbUsers
            // 
            this.lbUsers.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.ItemHeight = 25;
            this.lbUsers.Location = new System.Drawing.Point(595, 38);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(246, 504);
            this.lbUsers.TabIndex = 7;
            this.lbUsers.SelectedIndexChanged += new System.EventHandler(this.lbUsers_SelectedIndexChanged);
            // 
            // btnOfferGame
            // 
            this.btnOfferGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOfferGame.Location = new System.Drawing.Point(593, 549);
            this.btnOfferGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnOfferGame.Name = "btnOfferGame";
            this.btnOfferGame.Size = new System.Drawing.Size(246, 57);
            this.btnOfferGame.TabIndex = 8;
            this.btnOfferGame.Text = "Предложить игру";
            this.btnOfferGame.UseVisualStyleBackColor = true;
            this.btnOfferGame.Click += new System.EventHandler(this.btnOfferGame_Click);
            // 
            // lbGames
            // 
            this.lbGames.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbGames.FormattingEnabled = true;
            this.lbGames.ItemHeight = 25;
            this.lbGames.Location = new System.Drawing.Point(847, 38);
            this.lbGames.Name = "lbGames";
            this.lbGames.Size = new System.Drawing.Size(246, 379);
            this.lbGames.TabIndex = 9;
            this.lbGames.SelectedIndexChanged += new System.EventHandler(this.lbGames_SelectedIndexChanged);
            // 
            // btnJoinGame
            // 
            this.btnJoinGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJoinGame.Location = new System.Drawing.Point(848, 424);
            this.btnJoinGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnJoinGame.Name = "btnJoinGame";
            this.btnJoinGame.Size = new System.Drawing.Size(246, 57);
            this.btnJoinGame.TabIndex = 10;
            this.btnJoinGame.Text = "Присоединится";
            this.btnJoinGame.UseVisualStyleBackColor = true;
            this.btnJoinGame.Click += new System.EventHandler(this.btnJoinGame_Click);
            // 
            // btnCreateGame
            // 
            this.btnCreateGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateGame.Location = new System.Drawing.Point(847, 487);
            this.btnCreateGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnCreateGame.Name = "btnCreateGame";
            this.btnCreateGame.Size = new System.Drawing.Size(245, 57);
            this.btnCreateGame.TabIndex = 11;
            this.btnCreateGame.Text = "Создать игру";
            this.btnCreateGame.UseVisualStyleBackColor = true;
            this.btnCreateGame.Click += new System.EventHandler(this.btnCreateGame_Click);
            // 
            // btnCancelGame
            // 
            this.btnCancelGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelGame.Location = new System.Drawing.Point(847, 550);
            this.btnCancelGame.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelGame.Name = "btnCancelGame";
            this.btnCancelGame.Size = new System.Drawing.Size(245, 57);
            this.btnCancelGame.TabIndex = 12;
            this.btnCancelGame.Text = "Отменить игру";
            this.btnCancelGame.UseVisualStyleBackColor = true;
            this.btnCancelGame.Click += new System.EventHandler(this.btnCancelGame_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(12, 12);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(72, 25);
            this.lblUser.TabIndex = 13;
            this.lblUser.Text = "Игрок:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(590, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "Игроки онлайн:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(842, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 25);
            this.label2.TabIndex = 15;
            this.label2.Text = "Окрытые игры:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(1101, 615);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnCancelGame);
            this.Controls.Add(this.btnCreateGame);
            this.Controls.Add(this.btnJoinGame);
            this.Controls.Add(this.lbGames);
            this.Controls.Add(this.btnOfferGame);
            this.Controls.Add(this.lbUsers);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.textBoxChat);
            this.Controls.Add(this.btnSendMessage);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Общий чат";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.TextBox textBoxChat;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.Button btnOfferGame;
        private System.Windows.Forms.ListBox lbGames;
        private System.Windows.Forms.Button btnJoinGame;
        private System.Windows.Forms.Button btnCreateGame;
        private System.Windows.Forms.Button btnCancelGame;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

