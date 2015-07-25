namespace WindowsFormsApplication1
{
    partial class TPTestForm
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
            this.btnGetAuth = new System.Windows.Forms.Button();
            this.btnSetAuth = new System.Windows.Forms.Button();
            this.btnGetStat = new System.Windows.Forms.Button();
            this.lResult = new System.Windows.Forms.Label();
            this.lDetail = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tbGCAuth = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbGCEp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tbSARMail = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbSARErr = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbSAROk = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbSARAm = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbSARCurr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbSAROp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbSARMerc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSAREnc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSARSec = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSARSes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbGAAAnsw = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tbGAAReq = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbGAAMerc = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbGAASec = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tbGSOp = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tbGSMerc = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnGetAuth
            // 
            this.btnGetAuth.Location = new System.Drawing.Point(245, 238);
            this.btnGetAuth.Name = "btnGetAuth";
            this.btnGetAuth.Size = new System.Drawing.Size(131, 27);
            this.btnGetAuth.TabIndex = 1;
            this.btnGetAuth.Text = "GetAuthorizeAnswer";
            this.btnGetAuth.UseVisualStyleBackColor = true;
            this.btnGetAuth.Click += new System.EventHandler(this.btnGetAuth_Click);
            // 
            // btnSetAuth
            // 
            this.btnSetAuth.Location = new System.Drawing.Point(22, 238);
            this.btnSetAuth.Name = "btnSetAuth";
            this.btnSetAuth.Size = new System.Drawing.Size(131, 27);
            this.btnSetAuth.TabIndex = 2;
            this.btnSetAuth.Text = "SendAuthorizeRequest";
            this.btnSetAuth.UseVisualStyleBackColor = true;
            this.btnSetAuth.Click += new System.EventHandler(this.btnSetAuth_Click);
            // 
            // btnGetStat
            // 
            this.btnGetStat.Location = new System.Drawing.Point(452, 238);
            this.btnGetStat.Name = "btnGetStat";
            this.btnGetStat.Size = new System.Drawing.Size(131, 27);
            this.btnGetStat.TabIndex = 3;
            this.btnGetStat.Text = "GetStatus";
            this.btnGetStat.UseVisualStyleBackColor = true;
            this.btnGetStat.Click += new System.EventHandler(this.btnGetStat_Click);
            // 
            // lResult
            // 
            this.lResult.AutoSize = true;
            this.lResult.Location = new System.Drawing.Point(15, 274);
            this.lResult.Name = "lResult";
            this.lResult.Size = new System.Drawing.Size(32, 13);
            this.lResult.TabIndex = 4;
            this.lResult.Text = "result";
            // 
            // lDetail
            // 
            this.lDetail.Location = new System.Drawing.Point(12, 294);
            this.lDetail.Multiline = true;
            this.lDetail.Name = "lDetail";
            this.lDetail.ReadOnly = true;
            this.lDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lDetail.Size = new System.Drawing.Size(892, 112);
            this.lDetail.TabIndex = 9;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(859, 220);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.tbGCAuth);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.tbGCEp);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(851, 194);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "General Config";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tbGCAuth
            // 
            this.tbGCAuth.Location = new System.Drawing.Point(87, 36);
            this.tbGCAuth.Name = "tbGCAuth";
            this.tbGCAuth.Size = new System.Drawing.Size(480, 20);
            this.tbGCAuth.TabIndex = 3;
            this.tbGCAuth.Text = "PRISMA 912EC803B2CE49E4A541068D495AB570";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Authorization";
            // 
            // tbGCEp
            // 
            this.tbGCEp.Location = new System.Drawing.Point(87, 10);
            this.tbGCEp.Name = "tbGCEp";
            this.tbGCEp.Size = new System.Drawing.Size(480, 20);
            this.tbGCEp.TabIndex = 1;
            this.tbGCEp.Text = "https://50.19.97.101:8243";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Endpoint";
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.tbSARMail);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.tbSARErr);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.tbSAROk);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.tbSARAm);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.tbSARCurr);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.tbSAROp);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.tbSARMerc);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tbSAREnc);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.tbSARSec);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.tbSARSes);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(851, 194);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SendAuthorizeRequest configuration";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tbSARMail
            // 
            this.tbSARMail.Location = new System.Drawing.Point(87, 244);
            this.tbSARMail.Name = "tbSARMail";
            this.tbSARMail.Size = new System.Drawing.Size(480, 20);
            this.tbSARMail.TabIndex = 21;
            this.tbSARMail.Text = "email_cliente@dominio.com";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 244);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "EMAILCLIENTE";
            // 
            // tbSARErr
            // 
            this.tbSARErr.Location = new System.Drawing.Point(87, 218);
            this.tbSARErr.Name = "tbSARErr";
            this.tbSARErr.Size = new System.Drawing.Size(480, 20);
            this.tbSARErr.TabIndex = 19;
            this.tbSARErr.Text = "http://yahoo.com";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 218);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "URL_ERROR";
            // 
            // tbSAROk
            // 
            this.tbSAROk.Location = new System.Drawing.Point(87, 192);
            this.tbSAROk.Name = "tbSAROk";
            this.tbSAROk.Size = new System.Drawing.Size(480, 20);
            this.tbSAROk.TabIndex = 17;
            this.tbSAROk.Text = "http://google.com";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 192);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "URL_OK";
            // 
            // tbSARAm
            // 
            this.tbSARAm.Location = new System.Drawing.Point(87, 166);
            this.tbSARAm.Name = "tbSARAm";
            this.tbSARAm.Size = new System.Drawing.Size(480, 20);
            this.tbSARAm.TabIndex = 15;
            this.tbSARAm.Text = "54";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Ammount";
            // 
            // tbSARCurr
            // 
            this.tbSARCurr.Location = new System.Drawing.Point(87, 140);
            this.tbSARCurr.Name = "tbSARCurr";
            this.tbSARCurr.Size = new System.Drawing.Size(480, 20);
            this.tbSARCurr.TabIndex = 13;
            this.tbSARCurr.Text = "032";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 140);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Currency";
            // 
            // tbSAROp
            // 
            this.tbSAROp.Location = new System.Drawing.Point(87, 114);
            this.tbSAROp.Name = "tbSAROp";
            this.tbSAROp.Size = new System.Drawing.Size(480, 20);
            this.tbSAROp.TabIndex = 11;
            this.tbSAROp.Text = "01";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "OperationID";
            // 
            // tbSARMerc
            // 
            this.tbSARMerc.Location = new System.Drawing.Point(87, 88);
            this.tbSARMerc.Name = "tbSARMerc";
            this.tbSARMerc.Size = new System.Drawing.Size(480, 20);
            this.tbSARMerc.TabIndex = 9;
            this.tbSARMerc.Text = "538";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 88);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Merchant";
            // 
            // tbSAREnc
            // 
            this.tbSAREnc.Location = new System.Drawing.Point(87, 62);
            this.tbSAREnc.Name = "tbSAREnc";
            this.tbSAREnc.Size = new System.Drawing.Size(480, 20);
            this.tbSAREnc.TabIndex = 7;
            this.tbSAREnc.Text = "XML";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Encoding";
            // 
            // tbSARSec
            // 
            this.tbSARSec.Location = new System.Drawing.Point(87, 36);
            this.tbSARSec.Name = "tbSARSec";
            this.tbSARSec.Size = new System.Drawing.Size(480, 20);
            this.tbSARSec.TabIndex = 5;
            this.tbSARSec.Text = "912EC803B2CE49E4A541068D495AB570";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Security";
            // 
            // tbSARSes
            // 
            this.tbSARSes.Location = new System.Drawing.Point(87, 10);
            this.tbSARSes.Name = "tbSARSes";
            this.tbSARSes.Size = new System.Drawing.Size(480, 20);
            this.tbSARSes.TabIndex = 3;
            this.tbSARSes.Text = "ABCDEF-1234-12221-FDE1-00000200";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Session";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbGAAAnsw);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.tbGAAReq);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.tbGAAMerc);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.tbGAASec);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(851, 194);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "GetAuthorizeAnswer configuration";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbGAAAnsw
            // 
            this.tbGAAAnsw.Location = new System.Drawing.Point(87, 88);
            this.tbGAAAnsw.Name = "tbGAAAnsw";
            this.tbGAAAnsw.Size = new System.Drawing.Size(480, 20);
            this.tbGAAAnsw.TabIndex = 9;
            this.tbGAAAnsw.Text = "8496472a-8c87-e35b-dcf2-94d5e31eb12f";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 88);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 13);
            this.label16.TabIndex = 8;
            this.label16.Text = "AnswerKey";
            // 
            // tbGAAReq
            // 
            this.tbGAAReq.Location = new System.Drawing.Point(87, 62);
            this.tbGAAReq.Name = "tbGAAReq";
            this.tbGAAReq.Size = new System.Drawing.Size(480, 20);
            this.tbGAAReq.TabIndex = 7;
            this.tbGAAReq.Text = "8496472a-8c87-e35b-dcf2-94d5e31eb12f";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 62);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "RequestKey";
            // 
            // tbGAAMerc
            // 
            this.tbGAAMerc.Location = new System.Drawing.Point(87, 36);
            this.tbGAAMerc.Name = "tbGAAMerc";
            this.tbGAAMerc.Size = new System.Drawing.Size(480, 20);
            this.tbGAAMerc.TabIndex = 5;
            this.tbGAAMerc.Text = "305";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 36);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Merchant";
            // 
            // tbGAASec
            // 
            this.tbGAASec.Location = new System.Drawing.Point(87, 10);
            this.tbGAASec.Name = "tbGAASec";
            this.tbGAASec.Size = new System.Drawing.Size(480, 20);
            this.tbGAASec.TabIndex = 3;
            this.tbGAASec.Text = "1234567890ABCDEF1234567890ABCDEF";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 13);
            this.label13.TabIndex = 2;
            this.label13.Text = "Security";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tbGSOp);
            this.tabPage3.Controls.Add(this.label18);
            this.tabPage3.Controls.Add(this.tbGSMerc);
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(851, 194);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "GetStatus configuration";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tbGSOp
            // 
            this.tbGSOp.Location = new System.Drawing.Point(87, 36);
            this.tbGSOp.Name = "tbGSOp";
            this.tbGSOp.Size = new System.Drawing.Size(480, 20);
            this.tbGSOp.TabIndex = 7;
            this.tbGSOp.Text = "01";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 36);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(64, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "OperationID";
            // 
            // tbGSMerc
            // 
            this.tbGSMerc.Location = new System.Drawing.Point(87, 10);
            this.tbGSMerc.Name = "tbGSMerc";
            this.tbGSMerc.Size = new System.Drawing.Size(480, 20);
            this.tbGSMerc.TabIndex = 5;
            this.tbGSMerc.Text = "305";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 10);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Merchant";
            // 
            // TPTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 418);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lDetail);
            this.Controls.Add(this.lResult);
            this.Controls.Add(this.btnGetStat);
            this.Controls.Add(this.btnSetAuth);
            this.Controls.Add(this.btnGetAuth);
            this.Name = "TPTestForm";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGetAuth;
        private System.Windows.Forms.Button btnSetAuth;
        private System.Windows.Forms.Button btnGetStat;
        private System.Windows.Forms.Label lResult;
        private System.Windows.Forms.TextBox lDetail;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox tbGCAuth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbGCEp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox tbSARAm;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbSARCurr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbSAROp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbSARMerc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSAREnc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSARSec;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSARSes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSARMail;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbSARErr;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbSAROk;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbGAAAnsw;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbGAAReq;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbGAAMerc;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbGAASec;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbGSOp;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tbGSMerc;
        private System.Windows.Forms.Label label17;
    }
}

