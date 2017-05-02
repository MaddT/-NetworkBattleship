namespace Client
{
    partial class GameForm
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
            this.Text = "GameForm";
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnCancelStart = new System.Windows.Forms.Button();
            this.btnGenerateNewField = new System.Windows.Forms.Button();
            this.tbGameChat = new System.Windows.Forms.TextBox();
            this.tbGameMessage = new System.Windows.Forms.TextBox();
            this.lblOpp = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendMessage.Location = new System.Drawing.Point(413, 560);
            this.btnSendMessage.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(202, 58);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Послать сообщение";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartGame.Location = new System.Drawing.Point(414, 477);
            this.btnStartGame.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(214, 56);
            this.btnStartGame.TabIndex = 3;
            this.btnStartGame.Text = "Начать игру";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnCancelStart
            // 
            this.btnCancelStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelStart.Location = new System.Drawing.Point(622, 560);
            this.btnCancelStart.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnCancelStart.Name = "btnCancelStart";
            this.btnCancelStart.Size = new System.Drawing.Size(201, 58);
            this.btnCancelStart.TabIndex = 4;
            this.btnCancelStart.Text = "Покинуть игру";
            this.btnCancelStart.UseVisualStyleBackColor = true;
            this.btnCancelStart.Click += new System.EventHandler(this.btnCancelStart_Click);
            // 
            // btnGenerateNewField
            // 
            this.btnGenerateNewField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateNewField.Location = new System.Drawing.Point(414, 383);
            this.btnGenerateNewField.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnGenerateNewField.Name = "btnGenerateNewField";
            this.btnGenerateNewField.Size = new System.Drawing.Size(214, 82);
            this.btnGenerateNewField.TabIndex = 5;
            this.btnGenerateNewField.Text = "Сгенерировать новое расположение кораблей";
            this.btnGenerateNewField.UseVisualStyleBackColor = true;
            this.btnGenerateNewField.Click += new System.EventHandler(this.btnGenerateNewField_Click);
            // 
            // tbGameChat
            // 
            this.tbGameChat.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbGameChat.Location = new System.Drawing.Point(7, 368);
            this.tbGameChat.Multiline = true;
            this.tbGameChat.Name = "tbGameChat";
            this.tbGameChat.ReadOnly = true;
            this.tbGameChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGameChat.Size = new System.Drawing.Size(395, 192);
            this.tbGameChat.TabIndex = 6;
            // 
            // tbGameMessage
            // 
            this.tbGameMessage.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbGameMessage.Location = new System.Drawing.Point(7, 565);
            this.tbGameMessage.Multiline = true;
            this.tbGameMessage.Name = "tbGameMessage";
            this.tbGameMessage.Size = new System.Drawing.Size(395, 58);
            this.tbGameMessage.TabIndex = 7;
            // 
            // lblOpp
            // 
            this.lblOpp.AutoSize = true;
            this.lblOpp.Location = new System.Drawing.Point(636, 396);
            this.lblOpp.Name = "lblOpp";
            this.lblOpp.Size = new System.Drawing.Size(113, 24);
            this.lblOpp.TabIndex = 8;
            this.lblOpp.Text = "Противник:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(636, 455);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 24);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.Text = "Статус:";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(828, 633);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblOpp);
            this.Controls.Add(this.tbGameMessage);
            this.Controls.Add(this.tbGameChat);
            this.Controls.Add(this.btnGenerateNewField);
            this.Controls.Add(this.btnCancelStart);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.btnSendMessage);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Морской Бой";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnCancelStart;
        private System.Windows.Forms.Button btnGenerateNewField;
        private System.Windows.Forms.TextBox tbGameChat;
        private System.Windows.Forms.TextBox tbGameMessage;
        private System.Windows.Forms.Label lblOpp;
        private System.Windows.Forms.Label lblStatus;
    }
}